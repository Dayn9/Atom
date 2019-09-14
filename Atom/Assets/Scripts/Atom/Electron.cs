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

            OnDeselect.AddListener(DropParticle);
        }

        protected override void DropParticle()
        {
            if (atom.OuterShell.AddParticle(this))
            {
                Debug.Log("Electron Added");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
