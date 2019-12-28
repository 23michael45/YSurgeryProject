using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderEndDragEvent : UnityEvent<Slider>
{
    public SliderEndDragEvent() { }
}
[RequireComponent(typeof(Slider))]
public class SliderDrag : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public SliderEndDragEvent onEndDrag = new SliderEndDragEvent();
    public SliderEndDragEvent onStartDrag = new SliderEndDragEvent();

    public void OnPointerDown(PointerEventData data)
    {
        if (onStartDrag != null)
        {
            onStartDrag.Invoke(gameObject.GetComponent<Slider>());
        }
    }
    public void OnPointerUp(PointerEventData data)
    {
        if (onEndDrag != null)
        {
            onEndDrag.Invoke(gameObject.GetComponent<Slider>());
        }
    }
}
