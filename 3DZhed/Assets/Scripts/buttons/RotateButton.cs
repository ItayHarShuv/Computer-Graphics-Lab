using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RotateButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] char dir = 'n';
    public void OnPointerDown(PointerEventData eventData)
    {
        Rotator.Instance.dir = dir;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Rotator.Instance.dir = 'n';
    }
}
