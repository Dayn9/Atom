using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DUIAnchor : MonoBehaviour
{
    public enum Anchor { topLeft, topCenter, topRight, middleLeft, middleCenter, middleRight, bottomLeft, bottomCenter, bottomRight, custom }

    //[SerializeField] private Anchor anchor;
    [SerializeField] private Vector2 min;
    [SerializeField] private Vector2 max;

    private Bounds bounds;

    public Bounds Bounds { get { return bounds; } }

    private void Start()
    {
        if(transform.parent.GetComponent<DUIAnchor>() == null)
        {
            SetPosition(new Bounds(Vector2.zero, new Vector2 (DUI.cameraWidth , DUI.cameraHeight) * 2));
            foreach (DUIAnchor duia in GetComponentsInChildren<DUIAnchor>(true))
            {
                if(duia != this) 
                    duia.SetPosition(bounds);
            }
        }
    }

    public void SetPosition(Bounds r) {
        
        bounds = new Bounds(new Vector2 (r.center.x + ((max.x + min.x - 1) / 2) * r.size.x,
                                         r.center.y + ((max.y + min.y - 1) / 2) * r.size.y),
                            new Vector2 (r.size.x * (max.x - min.x),
                                         r.size.y * (max.y - min.y)));
        transform.position = bounds.center;
    }

    /*
    public void SetPosition(float width, float height)
    {
        transform.localPosition = Vector3.zero;
        //adjust x position of UI element
        switch (anchor)
        {
            //left
            case Anchor.topLeft:
            case Anchor.middleLeft:
            case Anchor.bottomLeft:
                transform.localPosition += Vector3.right * (-width + bounds.x);
                break;
            //right
            case Anchor.topRight:
            case Anchor.middleRight:
            case Anchor.bottomRight:
                transform.localPosition += Vector3.right * (width + bounds.x);
                break;
            //center
            default:
                transform.localPosition += Vector3.right * bounds.x;
                break;
        }

        //adjust y position of UI element
        switch (anchor)
        {
            //up
            case Anchor.topLeft:
            case Anchor.topCenter:
            case Anchor.topRight:
                transform.localPosition += Vector3.up * (height + bounds.y);
                break;
            //down
            case Anchor.bottomLeft:
            case Anchor.bottomCenter:
            case Anchor.bottomRight:
                transform.localPosition  += Vector3.up * (-height + bounds.y);
                break;
            //middle
            default:
                transform.localPosition += Vector3.up * bounds.y;
                break;
        }
    }
    */
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
