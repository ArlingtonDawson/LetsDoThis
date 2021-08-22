using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;

[Serializable]
[GenerateAuthoringComponent]
public struct MoveableComponent : IComponentData
{
    [GhostField]
    public int ExampleValue;
}
