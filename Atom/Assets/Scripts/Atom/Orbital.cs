using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Orbital : MonoBehaviour
    {
        private List<Particle> particles;

        private int maxParticles;

        public float radius;

        public int MaxParticles { set { maxParticles = value; } }
        public bool Full {
            get {
                if(particles == null)
                    particles = new List<Particle>();
                return particles.Count == maxParticles;
            }
        }

        public bool AddParticle(Particle particle)
        {
            if (particle.GetType().Equals(typeof(Proton)) || particle.GetType().Equals(typeof(Neutron)))
            {
                particles.Add(particle);
                particle.transform.SetParent(transform);
                return true;
            }
            return false;
        }
    }
}

