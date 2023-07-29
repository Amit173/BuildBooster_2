using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IScrollHandler
{
    [HideInInspector]
    public Vector2 touchDist;
    [HideInInspector]
    public Vector2 pointerOld;
    [HideInInspector]
    public int pointerId;
    [HideInInspector]
    public bool pressed;
    [HideInInspector]
    public float scroll;
   
    void Update()
    {
        if (pressed)
        {
            if (pointerId >= 0 && pointerId < Input.touches.Length)
            {
                touchDist = Input.touches[pointerId].position - pointerOld;
                pointerOld = Input.touches[pointerId].position;
            }
            else
            {
                touchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - pointerOld;
                pointerOld = Input.mousePosition;
            }
        }
        else
        {
            touchDist = new Vector2();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        pointerId = eventData.pointerId;
        pointerOld = eventData.position;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }

    public void OnScroll(PointerEventData eventData)
    {
        scroll = eventData.scrollDelta.y;
    }
}