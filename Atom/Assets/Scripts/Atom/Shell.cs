using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Shell : MonoBehaviour
    {
        private List<Particle> particles;

        private int maxParticles;

        public float radius;

        [SerializeField] private float particleSpeed;
        [SerializeField] private float orbitSpeed;

        private float seperationDistance;

        public int MaxParticles { set { maxParticles = value; } }
        public bool Full
        {
            get
            {
                if (particles == null)
                    particles = new List<Particle>();
                return particles.Count == maxParticles;
            }
        }

        private void Awake()
        {
            particles = new List<Particle>();
        }

        public bool AddParticle(Particle particle)
        {
            if (particle.GetType().Equals(typeof(Electron)))
            {
                particles.Add(particle);
                particle.transform.SetParent(transform);

                //calculate the new seperation distance
                seperationDistance = SeperationDistance(particles.Count);
                return true;
            }
            return false;
        }

        private void FixedUpdate()
        {
            foreach (Particle particle in particles)
            {
                Vector3 diffRadius = transform.position - particle.PhysicsObj.position;
                
                Vector3 forceToRadius = diffRadius.normalized * (diffRadius.sqrMagnitude - radius * radius) * particleSpeed;

                Vector3 forceToOrbit = new Vector2(-diffRadius.y, diffRadius.x).normalized * orbitSpeed;

                //calculate the force to seperate
                Vector2 forceToSeperate = Vector3.zero;
                foreach (Particle other in particles)
                {
                    //don't seperate from self
                    if (!particle.Equals(other))
                    {
                        //find the distance between particles
                        Vector2 diffOther = particle.PhysicsObj.position - other.PhysicsObj.position;
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
                particle.PhysicsObj.velocity += forceToRadius + forceToOrbit + (Vector3) forceToSeperate;
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
