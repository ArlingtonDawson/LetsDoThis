using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Loadout
{
    public Exosuit Exosuit;
    public DropPod DropPod;
}

public class Exosuit
{
    public NativeArray<HardPoint> Hardpoints;
}

public class DropPod
{
}

public struct Stucture
{
    public int MaxHitpoints;
    public NativeArray<Resistance> Resistances;
}

public class Structure : ScriptableObject
{
    public string Name;
    public int Hitpoints;
    public GameObject GameObject;
}
