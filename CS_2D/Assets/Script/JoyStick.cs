using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Image Joy;   
    private Image Handle;    
    private Vector2 touchPosition;  
     

    private void Awake()
    {
        Joy = GetComponent<Image>();
        Handle = transform.GetChild(0).GetComponent<Image>();

    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPosition =  Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Joy.rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
        {
            touchPosition.x = (touchPosition.x / Joy.rectTransform.sizeDelta.x);
            touchPosition.y = (touchPosition.y / Joy.rectTransform.sizeDelta.y);

            touchPosition = new Vector2(touchPosition.x * 2, touchPosition.y * 2 );

            touchPosition = (touchPosition.magnitude > 1 ? touchPosition.normalized : touchPosition);

            Handle.rectTransform.anchoredPosition = new Vector2(touchPosition.x * Joy.rectTransform.sizeDelta.x / 2, touchPosition.y * Joy.rectTransform.sizeDelta.y / 2);
        }
    }

    public void OnPointerDown(PointerEventData eventData) //터치 상태일 때 매 프레임
    {
        Debug.Log("Touch Began : "+ eventData);
    }

    public void OnPointerUp(PointerEventData eventData) //터치 종료 시 1회
    {
        Handle.rectTransform.anchoredPosition = Vector2.zero;
        touchPosition = Vector2.zero;
    }
    public float Horizontal()
    {       
        return touchPosition.x;
    }
    public float Vertical()
    {
        return touchPosition.y;
    }
}