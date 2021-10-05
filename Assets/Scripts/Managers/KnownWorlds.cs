using System;
using System.Collections.Generic;

[Serializable]
public class KnownWorlds
{
    public List<WorldInformation> Worlds = new List<WorldInformation>();

    public void RemoveWorld(WorldInformation worldInformation)
    {
        Worlds.Remove(worldInformation);
    }

    public void AddWorld(WorldInformation worldInformation)
    {
        if (Worlds.Contains(worldInformation))
        {
            Worlds.Remove(worldInformation);
        }

        Worlds.Add(worldInformation);
    }
}