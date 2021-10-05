using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
using System;

namespace LetsDoThis.UI
{

    public class Login : MonoBehaviour
    {
        // Debug Flag to simulate a reset
        public bool ClearPlayerPrefs;

        // Meta fields for objects in the UI

        public TMP_InputField LoginEmail;
        public TMP_InputField LoginPassword;
        public TextMeshProUGUI LoginStatusText;

        public TMP_InputField Email;
        public TMP_InputField ConfirmEmail;
        public TMP_InputField Password;
        public TMP_InputField ConfirmPassword;
        public TextMeshProUGUI StatusText;
        
        public Toggle RememberMe;

        public Button LoginButton;
        public Button RegisterButton;
        public Button CreateNewAccountButton;
        public Button CancelRegisterButton;

        // Meta references to panels we need to show / hide
        public GameObject RegisterPanel;
        public GameObject SigninPanel;

        public Text UserName;

        // Settings for what data to get from playfab on login.
        public GetPlayerCombinedInfoRequestParams InfoRequestParams;

        // Reference to our Authentication service
        private PlayFabAuthService _AuthService = PlayFabAuthService.Instance;

        public void Awake()
        {
            if (ClearPlayerPrefs)
            {
                _AuthService.UnlinkSilentAuth();
                _AuthService.ClearRememberMe();
                _AuthService.AuthType = Authtypes.None;
            }

            //Set our remember me button to our remembered state.
            RememberMe.isOn = _AuthService.RememberMe;

            //Subscribe to our Remember Me toggle
            RememberMe.onValueChanged.AddListener(
                (toggle) =>
                {
                    _AuthService.RememberMe = toggle;
                });
        }

        public void Start()
        {
            // Hide all our panels until we know what UI to display
            RegisterPanel.SetActive(false);
            SigninPanel.SetActive(true);

            // Subscribe to events that happen after we authenticate
            PlayFabAuthService.OnDisplayAuthentication += OnDisplayAuthentication;
            PlayFabAuthService.OnLoginSuccess += OnLoginSuccess;
            PlayFabAuthService.OnPlayFabError += OnPlayFaberror;

            // Bind to UI buttons to perform actions when user interacts with the UI.
            LoginButton.onClick.AddListener(OnLoginClicked);
            CreateNewAccountButton.onClick.AddListener(CreateNewAccountClicked);
            RegisterButton.onClick.AddListener(OnRegisterButtonClicked);
            CancelRegisterButton.onClick.AddListener(OnCancelRegisterButtonClicked);

            // Set the data we want at login from what we chose in our meta data.
            _AuthService.InfoRequestParams = InfoRequestParams;

            // Start the authentication process.
            _AuthService.Authenticate();
        }

        private void CreateNewAccountClicked()
        {
            Email.text = LoginEmail.text;
            RegisterPanel.SetActive(true);
            SigninPanel.SetActive(false);
        }


        /// <summary>
        /// Login Successfully - Goes to next screen.
        /// </summary>
        /// <param name="result"></param>
        private void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
        {
            Debug.LogFormat("Logged In as: {0}", result.PlayFabId);
            LoginStatusText.text = "";
        }

        /// <summary>
        /// Error handling for when Login returns errors.
        /// </summary>
        /// <param name="error"></param>
        private void OnPlayFaberror(PlayFabError error)
        {
            //There are more cases which can be caught, below are some
            //of the basic ones.
            switch (error.Error)
            {
                case PlayFabErrorCode.InvalidEmailAddress:
                case PlayFabErrorCode.InvalidPassword:
                case PlayFabErrorCode.InvalidEmailOrPassword:
                    StatusText.text = "Invalid Email or Password";
                    break;

                case PlayFabErrorCode.AccountNotFound:
                    Email.text = LoginEmail.text;
                    RegisterPanel.SetActive(true);
                    SigninPanel.SetActive(false);
                    return;
                default:
                    StatusText.text = error.GenerateErrorReport();
                    break;
            }

            //Also report to debug console, this is optional.
            Debug.Log(error.Error);
            Debug.LogError(error.GenerateErrorReport());
        }

        /// <summary>
        /// Choose to display the Auth UI or any other action.
        /// </summary>
        private void OnDisplayAuthentication()
        {
            //Here we have choses what to do when AuthType is None.
            StatusText.text = "";

            /*
             * Optionally we could Not do the above and force login silently
             * 
             * _AuthService.Authenticate(Authtypes.Silent);
             * 
             * This example, would auto log them in by device ID and they would
             * never see any UI for Authentication.
             * 
             */
        }

        /// <summary>
        /// Login Button means they've selected to submit a username (email) / password combo
        /// Note: in this flow if no account is found, it will ask them to register.
        /// </summary>
        private void OnLoginClicked()
        {
            LoginStatusText.text = string.Format("Logging In As {0} ...", Email.text);

            _AuthService.Email = LoginEmail.text;
            _AuthService.Password = LoginPassword.text;
            _AuthService.Authenticate(Authtypes.EmailAndPassword);
        }

        /// <summary>
        /// No account was found, and they have selected to register a username (email) / password combo.
        /// </summary>
        private void OnRegisterButtonClicked()
        {
            if (Email.text != ConfirmEmail.text)
            {
                StatusText.text = "Emails do not Match.";
                return;
            }

            if (Password.text != ConfirmPassword.text)
            {
                StatusText.text = "Passwords do not Match.";
                return;
            }

            StatusText.text = string.Format("Registering User {0} ...", Email.text);

            _AuthService.Email = Email.text;
            _AuthService.Password = Password.text;
            _AuthService.Authenticate(Authtypes.RegisterPlayFabAccount);
        }

        /// <summary>
        /// They have opted to cancel the Registration process.
        /// Possibly they typed the email address incorrectly.
        /// </summary>
        private void OnCancelRegisterButtonClicked()
        {
            // Reset all forms
            Email.text = string.Empty;
            ConfirmEmail.text = string.Empty;
            Password.text = string.Empty;
            ConfirmPassword.text = string.Empty;
            StatusText.text = "";
            LoginStatusText.text = "";
            // Show panels
            RegisterPanel.SetActive(false);
            SigninPanel.SetActive(true);
        }

        private void OnClearSigninButtonClicked()
        {
            _AuthService.ClearRememberMe();
            StatusText.text = "Signin info cleared";
        }

        private void OnResetSampleButtonClicked()
        {
            PlayFabClientAPI.ForgetAllCredentials();
            _AuthService.Email = string.Empty;
            _AuthService.Password = string.Empty;
            _AuthService.AuthTicket = string.Empty;
            _AuthService.Authenticate();
        }
    }
}

