using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Nucleus : MonoBehaviour
    {
        /// <summary>
        /// Handles the behavior of Atom's nucleus
        /// </summary>

        [SerializeField] private float particleSpeed; //magnitude of force to center
        [SerializeField] private float rotationSpeed; //degees to spin 

        private List<Particle> particles; //list of all particles in nucleus

        private int protonCount = 0; //number of protons in nucleus
        private int neutronCount = 0; //number of neutrons in nucleus

        public int ProtonCount { get { return protonCount; } }
        public int NeutronCount { get { return neutronCount; } }

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
            if (particle.GetType().Equals(typeof(Proton)) && protonCount < 18)
            {
                protonCount++;

                //add the particle and set the parent
                particles.Add(particle);
                particle.transform.SetParent(transform);
                return true;
            }
            else if (particle.GetType().Equals(typeof(Neutron)) && neutronCount < 20)
            {
                neutronCount++;

                //add the particle and set the parent
                particles.Add(particle);
                particle.transform.SetParent(transform);
                return true;
            }
            return false;
        }

        /// <summary>
        /// try and remove a particle from the nucleus
        /// </summary>
        /// <param name="particle">particle to remove</param>
        /// <returns>removal suceess</returns>
        public bool RemoveParticle(Particle particle)
        {
            //check type of particle
            if (particle.GetType().Equals(typeof(Proton)) && particles.Contains(particle))
            {
                protonCount--;

                //add the particle and set the parent
                particles.Remove(particle);
                particle.transform.SetParent(null);
                return true;
            }
            else if (particle.GetType().Equals(typeof(Neutron)) && particles.Contains(particle))
            {
                neutronCount--;

                //add the particle and set the parent
                particles.Remove(particle);
                particle.transform.SetParent(null);
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
