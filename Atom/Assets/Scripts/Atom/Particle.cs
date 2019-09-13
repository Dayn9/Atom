using UnityEngine;
using UnityEngine.Events;
using Physics;

namespace Atom
{
    [RequireComponent(typeof(PhysicsObject))]
    public abstract class Particle : MonoBehaviour
    {
        /// <summary>
        /// Parent class for Protons, Neutrons, Electorns
        /// Contains particle data and helper methods
        /// </summary>

        protected float mass;
        protected byte charge = 0;

        protected bool selected = false;
        public UnityEvent OnSelect;
        public UnityEvent OnDeselect;

        protected static Atom atom;

        private PhysicsObject physicsObj;

        public float Radius {
            get { return transform.localScale.x / 2; }
            set { transform.localScale = Vector3.one * 2 * value; }
        }

        public PhysicsObject PhysicsObj { get { return physicsObj; } }

        protected virtual void Awake()
        {
            physicsObj = GetComponent<PhysicsObject>();

            if(atom == null)
            {
                atom = FindObjectOfType<Atom>();
                if (atom == null)
                {
                    throw new System.Exception("An Atom class is needed");
                }
            }

            OnSelect.AddListener(Select);
            OnDeselect.AddListener(Deselect);
        }

        protected virtual void Update()
        {
            if (selected)
            {
                transform.position = (Vector3)DUI.DUI.mousePos + Vector3.back;

                if (Input.GetMouseButtonUp(0))
                {
                    OnDeselect?.Invoke();
                }
            }
        }

        protected void Select()
        {
            selected = true;
        }

        protected void Deselect()
        {
            selected = false;
        }

        protected virtual void DropParticle() { }

    }
}
