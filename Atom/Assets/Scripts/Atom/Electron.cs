using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Electron : Particle
    {
        protected override void Awake()
        {
            base.Awake();
            mass = 0.01f;
            charge = -1;
            Radius = 0.25f;
        }

        protected override void PickUpParticle()
        {
            if (inAtom && atom.RemoveElectron(this))
            {
                base.PickUpParticle();
                Debug.Log("Electron Removed");
            }
        }

        protected override void DropParticle()
        {
            if (!inAtom && atom.Contains(transform.position) && atom.OuterShell.AddParticle(this))
            {
                base.DropParticle();
                Debug.Log("Electron Added");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
