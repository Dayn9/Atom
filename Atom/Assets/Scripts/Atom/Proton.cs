using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Proton : Particle
    {
        protected override void Awake()
        {
            mass = 1;
            charge = 1;
            Radius = 0.5f;

            base.Awake();
        }

        protected override void PickUpParticle()
        {
            if (atom.Nucleus.RemoveParticle(this))
            {
                Debug.Log("Proton Removed");
            }
            else
            {
                
            }
            
        }

        protected override void DropParticle()
        {
            if (atom.Nucleus.AddParticle(this))
            {
                Debug.Log("Proton Added");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
