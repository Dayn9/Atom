using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Electron : Particle
    {
        /// <summary>
        /// Handles the behavior of electron particles in the atom
        /// </summary>

        protected override void Awake()
        {
            base.Awake();
            Radius = 0.25f;
        }

        protected override void PickUpParticle()
        {
            //check the the electron is part of the atom and can be removed
            if (inAtom && atom.RemoveElectron(this))
            {
                base.PickUpParticle();
                Debug.Log("Electron Removed");
            }
            else
            {
                atom.RemoveExcessParticle(this);
            }
        }

        protected override void DropParticle()
        {
            //electron must be added when Next Shell exists and isn't Full
            bool mustAdd = atom.OuterShell != null && atom.OuterShell.NextShell != null && !atom.OuterShell.NextShell.Full;
            
            //check not already part of atom, within atom bounds, and can actually be added
            if (!inAtom && (atom.Contains(transform.position) || mustAdd ) && atom.OuterShell.AddParticle(this))
            {
                base.DropParticle();
                Debug.Log("Electron Added");
            }
            //electron out of bounds or cound not be added 
            else
            {
                atom.AddExcessParticle(this);
            }
        }
    }
}
