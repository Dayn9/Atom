using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DUI
{
    public class DUIAnchor : MonoBehaviour
    {
        [SerializeField] private Vector2 min;
        [SerializeField] private Vector2 max;

        private Bounds bounds;

        public Bounds Bounds { get { return bounds; } }
        public Rect Rect { get { return new Rect(bounds.center, bounds.size); } }

        private void Start()
        {
            if (transform.parent.GetComponent<DUIAnchor>() == null)
            {
                SetPosition(new Bounds(Vector2.zero, new Vector2(DUI.cameraWidth, DUI.cameraHeight) * 2));
                foreach (DUIAnchor duia in GetComponentsInChildren<DUIAnchor>(true))
                {
                    if (duia != this)
                        duia.SetPosition(bounds);
                }
            }
        }

        public void SetPosition(Bounds r)
        {

            bounds = new Bounds(new Vector2(r.center.x + ((max.x + min.x - 1) / 2) * r.size.x,
                                             r.center.y + ((max.y + min.y - 1) / 2) * r.size.y),
                                new Vector2(r.size.x * Mathf.Abs(max.x - min.x),
                                             r.size.y * Mathf.Abs(max.y - min.y)));
            transform.position = bounds.center;

            SpriteRenderer render = GetComponent<SpriteRenderer>();
            if (render != null)
            {
                render.size = bounds.size;
            }
        }

        public void SetMinMax(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// draw a green collider based on area
        /// </summary>
        protected void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;

            Vector2 center = bounds.center;

            Vector2 topLeft = new Vector2(center.x - bounds.extents.x, center.y + bounds.extents.y);
            Vector2 topRight = new Vector2(center.x + bounds.extents.x, center.y + bounds.extents.y);
            Vector2 bottomLeft = new Vector2(center.x - bounds.extents.x, center.y - bounds.extents.y);
            Vector2 bottomRight = new Vector2(center.x + bounds.extents.x, center.y - bounds.extents.y);

            //draw lines between corners
            Gizmos.DrawLine(topLeft, topRight); //top
            Gizmos.DrawLine(topRight, bottomRight); //right
            Gizmos.DrawLine(bottomRight, bottomLeft); //bottom
            Gizmos.DrawLine(bottomLeft, topLeft); //left
        }
    }


    [CustomEditor(typeof(DUIAnchor))]
    [CanEditMultipleObjects]
    public class DUIAnchorEditor : Editor
    {
        SerializedProperty min;
        SerializedProperty max;

        private void OnEnable()
        {
            min = serializedObject.FindProperty("min");
            max = serializedObject.FindProperty("max");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            min.vector2Value = EditorGUILayout.Vector2Field("min", min.vector2Value);
            max.vector2Value = EditorGUILayout.Vector2Field("max", max.vector2Value);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
