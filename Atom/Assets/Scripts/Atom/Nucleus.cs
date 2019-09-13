using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DUI;
using Physics;

namespace Atom
{
    [RequireComponent(typeof(DUIAnchor))]
    public class Nucleus : MonoBehaviour
    {
        [SerializeField] private float particleSpeed;
        [SerializeField] private float rotationSpeed;

        private List<Particle> particles;

        private DUIAnchor anchor;

        private void Awake()
        {
            particles = new List<Particle>();

            anchor = GetComponent<DUIAnchor>();
        }

        public bool AddParticle(Particle particle)
        {
            if(anchor.Bounds.Contains(DUI.DUI.mousePos) && 
              (particle.GetType().Equals(typeof(Proton)) || particle.GetType().Equals(typeof(Neutron))))
            {
                particles.Add(particle);
                particle.transform.SetParent(transform);
                return true;
            }
            return false;
        }

        void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            float maxDiff = 0;

            foreach (Particle particle in particles)
            {
                Vector3 diffOrgin = transform.position - particle.PhysicsObj.position;
                if (diffOrgin.sqrMagnitude > maxDiff) { maxDiff = diffOrgin.sqrMagnitude; }

                Vector3 forceToCenter = Vector3.ClampMagnitude(diffOrgin.normalized * particleSpeed, diffOrgin.magnitude);

                Vector3 forceToSeperate = Vector3.zero;
                foreach (Particle other in particles)
                {
                    if (!particle.Equals(other))
                    {
                        Vector3 diffOther = particle.PhysicsObj.position - other.PhysicsObj.position;
                        float overlap = diffOther.magnitude - particle.Radius - other.Radius;
                        if (overlap < 0)
                        {
                            forceToSeperate -= diffOther.normalized * overlap;
                        }
                    }
                }
                particle.PhysicsObj.velocity += forceToCenter + forceToSeperate;
            }
        }
    }
}
