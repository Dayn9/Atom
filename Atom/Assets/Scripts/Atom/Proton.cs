﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Proton : Particle
    {
        /// <summary>
        /// Handles the bahavior of proton particles in atom
        /// </summary>
        
        protected override void Awake()
        {
            Radius = 0.5f;

            base.Awake();
        }

        protected override void PickUpParticle()
        {
            //check the proton is part of the atom and can be removed
            if (inAtom && atom.Nucleus.RemoveParticle(this))
            {
                base.PickUpParticle();
                Debug.Log("Proton Removed");
            }
            else
            {
                
            }
            
        }

        protected override void DropParticle()
        {
            //check not already part of atom, within atom bounds, and can actually be added
            if (!inAtom && atom.Contains(transform.position) && atom.Nucleus.AddParticle(this))
            {
                base.DropParticle();
                Debug.Log("Proton Added");
            }
            //proton out of bounds or could not be added
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
