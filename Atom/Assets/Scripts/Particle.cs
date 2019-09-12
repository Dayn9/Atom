using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Particle : MonoBehaviour
{
    /// <summary>
    /// Parent class for Protons, Neutrons, Electorns
    /// Contains particle data and helper methods
    /// </summary>

    protected float mass;
    protected byte charge = 0;

}
