using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlanetBehaviorJB : ScriptableObject {
    public abstract void ApplyBehavior(Transform planetTransform);
}