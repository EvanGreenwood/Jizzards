using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
//
public partial class FluidType : ScriptableEnum
{
    public Color color;
    public float repulsionRange = 1;
    public float repulsionForce = 1;
    public float attractionRange = 3;
    public float attractionForce = 1;
    public float drag = 0.5f;
    public bool playerAttraction = false;
    public float playerAttractionForce = 0;
    public bool aimForTargetSpeed = false;
    public float targetSpeed = 2;
    public float targetSpeedLerp = 0.4f;
}
