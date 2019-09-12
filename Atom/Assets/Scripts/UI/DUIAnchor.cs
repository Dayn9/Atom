using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DUIAnchor : MonoBehaviour
{
    public enum Anchor { topLeft, topCenter, topRight, middleLeft, middleCenter, middleRight, bottomLeft, bottomCenter, bottomRight }

    [SerializeField] private Anchor anchor;
    [SerializeField] private Rect bounds;

    private void Start()
    {
        if(transform.parent.GetComponent<DUIAnchor>() == null)
        {
            SetPosition();
            foreach (DUIAnchor duia in GetComponentsInChildren<DUIAnchor>(true))
            {
                if(duia != this) 
                    duia.SetPosition(bounds.width, bounds.height);
            }
        }
    }

    public void SetPosition()
    {
        SetPosition(DUI.cameraWidth, DUI.cameraHeight);
    }

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

    /// <summary>
    /// draw a green collider based on area
    /// </summary>
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Vector2 center = new Vector2(transform.position.x + bounds.x, transform.position.y + bounds.y);

        Vector2 topLeft = new Vector2(center.x - bounds.width, center.y + bounds.height);
        Vector2 topRight = new Vector2(center.x + bounds.width, center.y + bounds.height);
        Vector2 bottomLeft = new Vector2(center.x - bounds.width, center.y - bounds.height);
        Vector2 bottomRight = new Vector2(center.x + bounds.width, center.y - bounds.height);

        //draw lines between corners
        Gizmos.DrawLine(topLeft, topRight); //top
        Gizmos.DrawLine(topRight, bottomRight); //right
        Gizmos.DrawLine(bottomRight, bottomLeft); //bottom
        Gizmos.DrawLine(bottomLeft, topLeft); //left
    }
}
