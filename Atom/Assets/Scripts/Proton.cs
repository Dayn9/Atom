using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proton : Particle
{
    protected override void Awake()
    {
        base.Awake();
        mass = 1;
        charge = 1;
    }
}
