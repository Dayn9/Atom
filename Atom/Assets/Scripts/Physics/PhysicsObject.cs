using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Physics
{
    public class PhysicsObject : MonoBehaviour
    {
        //TODO make get porpertys and method for apply forces so that these are hidden
        public Vector3 position;
        public Vector3 velocity;

        [SerializeField] [Range(0, 1)] private float drag;

        private void Start()
        {
            position = transform.position;
        }

        private void FixedUpdate()
        {
            position = transform.position;

            position += velocity * Time.deltaTime;

            transform.position = position;

            velocity *= 1 - drag;
        }
    }
}

