using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public GameObject Object;
    GameObject temp;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    
        temp = Instantiate(Object,canvas.transform);
        temp.GetComponent<DragDrop>().canvas = canvas;
        temp.SetActive(true);
    }
    public void OnPointerUp(PointerEventData eventData)
    {

    }
    //public void OnDrag(PointerEventData eventData)
    //{
    //    rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    //}
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    canvasGroup.blocksRaycasts = false;
    //}
    //public void OnEndDrag(PointerEventData eventData)
    //{

    //    canvasGroup.blocksRaycasts = true;
    //}

}
