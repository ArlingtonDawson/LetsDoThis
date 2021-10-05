using MLAPI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class WorldSelectionManager : MonoBehaviour
{
    public KnownWorlds KnownWorlds = new KnownWorlds();

    private WorldInformation newWorld = new WorldInformation();

    // Start is called before the first frame update
    void Start()
    {
        loadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNewWorldAddressChange(string WorldAddress)
    {
        newWorld.ServerAddress = WorldAddress;
    }

    public void OnConnectToWorld()
    {

        //Query the Server if it exist
        newWorld.Name = "Test";
        KnownWorlds.AddWorld(newWorld);
        saveData();
    }

    private void saveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/worlds.kwnwld";
        using FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, KnownWorlds);
        stream.Close();
    }

    private void loadData()
    {
        string path = Application.persistentDataPath + "/worlds.kwnwld";
        if(!File.Exists(path))
        {
            Debug.Log("No known worlds file found.");
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        using FileStream stream = new FileStream(path, FileMode.Open);

        KnownWorlds = formatter.Deserialize(stream) as KnownWorlds;
        stream.Close();
    }
}
