using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Nucleus : MonoBehaviour
    {
        [SerializeField] private float particleSpeed;
        [SerializeField] private float rotationSpeed;

        private List<Particle> particles;

        private void Awake()
        {
            particles = new List<Particle>();

        }

        /// <summary>
        /// Adds a particle to the nucleus
        /// </summary>
        /// <param name="particle">Particle to add</param>
        /// <returns>true when particle successfully added</returns>
        public bool AddParticle(Particle particle)
        {
            //check type of particle
            if(particle.GetType().Equals(typeof(Proton)) || particle.GetType().Equals(typeof(Neutron)))
            {
                //add the particle and set the parent
                particles.Add(particle);
                particle.transform.SetParent(transform);
                return true;
            }
            return false;
        }

        void Update()
        {
            //slowly spin the nucleus
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        private void FixedUpdate()
        {        
            foreach (Particle particle in particles)
            {
                //find the distance from origin
                Vector3 diffOrgin = transform.position - particle.PhysicsObj.position;
                //calculate the force to center ( clamp is used so particles slow near center
                Vector3 forceToCenter = Vector3.ClampMagnitude(diffOrgin.normalized * particleSpeed, diffOrgin.magnitude);

                //calculate the force to seperate
                Vector3 forceToSeperate = Vector3.zero;
                foreach (Particle other in particles)
                {
                    //don't seperate from self
                    if (!particle.Equals(other))
                    {
                        //find the distance between particles
                        Vector3 diffOther = particle.PhysicsObj.position - other.PhysicsObj.position;
                        //calculate the amount of overlap
                        float overlap = diffOther.magnitude - particle.Radius - other.Radius;
                        //check if actually overlapping
                        if (overlap < 0)
                        {
                            //add force to seperate
                            forceToSeperate -= diffOther.normalized * overlap;
                        }
                    }
                }
                //apply forces to the particles
                particle.PhysicsObj.velocity += forceToCenter + forceToSeperate;
            }
        }
    }
}
