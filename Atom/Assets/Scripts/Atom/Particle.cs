﻿using UnityEngine;
using UnityEngine.Events;
using Physics;
using DUI;

namespace Atom
{
    [RequireComponent(typeof(PhysicsObject))]
    [RequireComponent(typeof(DUISphereButton))]
    public abstract class Particle : MonoBehaviour
    {
        /// <summary>
        /// Parent class for Protons, Neutrons, Electorns
        /// </summary>

        protected float mass;
        protected sbyte charge = 0;

        protected bool selected = false; //true when the particle is currently selected

        public UnityEvent OnSelect; //called when the particle is first selected 
        public UnityEvent OnDeselect; //called when the particle is released from selection

        protected static Atom atom; //static ref to the Atom

        private PhysicsObject physicsObj;
        private DUISphereButton sphereButton;

        protected bool inAtom = false;

        //get and set the radius in Unity Units
        public float Radius {
            get { return transform.localScale.x / 2; }
            set { transform.localScale = Vector3.one * 2 * value; }
        }

        public PhysicsObject PhysicsObj { get { return physicsObj; } }

        protected virtual void Awake()
        {
            physicsObj = GetComponent<PhysicsObject>();
            sphereButton = GetComponent<DUISphereButton>();

            sphereButton.Radius = Radius;

            if(atom == null)
            {
                atom = FindObjectOfType<Atom>();
                if (atom == null)
                {
                    throw new System.Exception("An Atom class is needed");
                }
            }

            sphereButton.OnClick.AddListener(Select);
            OnSelect.AddListener(Select);
            OnDeselect.AddListener(Deselect);
        }

        protected virtual void Update()
        {
            if (selected)
            {
                //move to mouse position when selected
                transform.position = (Vector3)DUI.DUI.inputPos + Vector3.back;

                //call deselect when mouse released
                if (Input.GetMouseButtonUp(0))
                {
                    OnDeselect?.Invoke();
                }
            }
        }

        protected void Select()
        {
            PickUpParticle();

            selected = true;
        }

        protected void Deselect()
        {
            DropParticle();

            //physicsObj.velocity = (DUI.DUI.inputPos - DUI.DUI.inputPosPrev) * 2;
            selected = false;
        }

        /// <summary>
        /// Behavior for when the particle is dropped into the atom
        /// </summary>
        protected virtual void DropParticle()
        {
            inAtom = true;
        }

        protected virtual void PickUpParticle()
        {
            inAtom = false;
        }

    }
}
