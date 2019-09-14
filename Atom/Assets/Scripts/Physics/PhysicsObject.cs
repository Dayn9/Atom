using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Physics
{
    public class PhysicsObject : MonoBehaviour
    {
        /// <summary>
        /// Handles basic physics calculations
        /// </summary>

        //TODO make get porpertys and method for apply forces so that these are hidden
        public Vector3 position; //current postion of the object
        public Vector3 velocity; //current velocity of the object

        [SerializeField] [Range(0, 1)] private float drag; //amout to slow velocity by every update

        private void Start()
        {
            position = transform.position;
        }

        private void FixedUpdate()
        {
            //get the current position
            position = transform.position;

            //move by velocity
            position += velocity * Time.deltaTime;

            //apply to transform
            transform.position = position;

            //apply drag
            velocity *= 1 - drag;
        }
    }
}

