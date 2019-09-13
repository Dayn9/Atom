﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Neutron : Particle
    {
        protected override void Awake()
        {
            base.Awake();
            mass = 1;
            charge = 0;
            Radius = 0.5f;

            OnDeselect.AddListener(DropParticle);
        }

        protected override void DropParticle()
        {
            if (atom.Nucleus.AddParticle(this))
            {
                Debug.Log("Nutron Added");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    
}

    
