using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReseachItems", menuName = "LetsDoThis/Research Item")]
public class ResearchItem : ScriptableObject
{
    public string DisplayName;
    public int ResearchTimeCost;
    public List<RequiredResource> RequiredResources;
    public List<ResearchItem> RequiredResearch;
    
}
