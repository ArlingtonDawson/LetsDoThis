using Unity.Collections;

public struct HardPoint
{
    public NativeArray<HardpointAcceptedType> AcceptedType;
}

public struct HardpointAcceptedType
{
    public HardpointType Type;
}

public enum HardpointType
{
    Engine = 0,
    RangedWeapon = 1,
    MeleeWeapon = 2,
    Artilliery = 3,
    Missles = 4,
    Armor = 5,
    Hull = 6,
}