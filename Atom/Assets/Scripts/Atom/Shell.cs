using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Shell : MonoBehaviour
    {
        [SerializeField] private float particleSpeed; //magnitude of force to get into orbit
        [SerializeField] private float orbitSpeed; //magnitude of orbital force

        private List<Particle> particles; //list of all the particles in this shell
        private int maxParticles; //the maximum number of particles that can be in this shell
        private float seperationDistance; //how far apart each electron should be
        private Shell nextShell; //ref to the next shell down

        public float radius; //desired orbital radius

        public int ElectronCount { get { return particles.Count; } }
        public Shell NextShell { get { return nextShell; } set { nextShell = value; } }
        public int MaxParticles { set { maxParticles = value; } }
        public bool Full { get { return particles.Count == maxParticles; } }
        public bool Empty { get { return particles.Count == 0; } }

        private void Awake()
        {
            particles = new List<Particle>();
        }

        /// <summary>
        /// Add a particle to this shell
        /// </summary>
        /// <param name="particle">Particle to be added</param>
        /// <returns>true if sucessfully added</returns>
        public bool AddParticle(Particle particle)
        {
            //make sure the particle is an electron and the shell is not full
            if (particle.GetType().Equals(typeof(Electron)) && !Full)
            {
                //add the particle
                particles.Add(particle);
                particle.transform.SetParent(transform);

                //calculate the new seperation distance
                seperationDistance = SeperationDistance(particles.Count);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes a particle from this shell
        /// </summary>
        /// <param name="particle">Particle to remove</param>
        /// <returns></returns>
        public bool RemoveParticle(Particle particle)
        {
            //make sure the particle is an electron and actually in this shell
            if (particle.GetType().Equals(typeof(Electron)) && particles.Contains(particle))
            {
                particles.Remove(particle);
                particle.transform.SetParent(null);

                //calculate the new seperation distance
                seperationDistance = SeperationDistance(particles.Count);
                return true;
            }
            //not in shell, check the next one
            else if (NextShell != null)
            {
                //recursively check if particle in next shell
                if (NextShell.RemoveParticle(particle))
                {
                    //replace the removed partcicle with one from this shell
                    Particle transferParticle = particles[0];
                    particles.Remove(transferParticle);
                    NextShell.AddParticle(transferParticle);

                    seperationDistance = SeperationDistance(particles.Count);
                    return true;
                }
                return false;
            }
            return false;
        }

        private void FixedUpdate()
        {
            foreach (Particle particle in particles)
            {
                //calculate force to get into orbit
                Vector3 diffRadius = transform.position - particle.PhysicsObj.Position;
                Vector2 forceToRadius = diffRadius.normalized * (diffRadius.magnitude - radius) * particleSpeed;

                //calculate force to maintain orbit
                Vector2 forceToOrbit = new Vector2(-diffRadius.y, diffRadius.x).normalized * orbitSpeed;

                //calculate the force to seperate
                Vector2 forceToSeperate = Vector3.zero;
                foreach (Particle other in particles)
                {
                    //don't seperate from self
                    if (!particle.Equals(other))
                    {
                        //find the distance between particles
                        Vector2 diffOther = particle.PhysicsObj.Position - other.PhysicsObj.Position;
                        //calculate the amount of overlap
                        float overlap = diffOther.magnitude - seperationDistance;

                        if(overlap < 0)
                        {
                            //add force to seperate
                            forceToSeperate -= diffOther.normalized * overlap;
                        }
                    }
                }

                //apply forces to the particles
                particle.PhysicsObj.AddForce(forceToRadius + forceToOrbit + forceToSeperate);
            }
        }

        /// <summary>
        /// calculates the distance between points when n points ar equally spaced on a circle
        /// </summary>
        /// <param name="n">number of points on circle</param>
        /// <returns>distance between points</returns>
        private float SeperationDistance(int n)
        {
            return 2 * radius * Mathf.Sin(Mathf.PI / n);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
