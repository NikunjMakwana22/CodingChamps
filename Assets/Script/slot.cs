using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class slot : MonoBehaviour,IDropHandler
{
   
    public void OnDrop(PointerEventData eventData)
    {
     //  Debug.Log("DropInSlot");
       // Debug.Log(this.transform.parent.parent.name);
        if (eventData.pointerDrag.transform.GetComponent<BlockManager>().type != "I")
        {
            eventData.pointerDrag.transform.SetParent(this.transform.GetChild(0).transform);
            Debug.Log(eventData.pointerDrag.transform.name);
        }
        else
        {
            eventData.pointerDrag.transform.SetParent(this.transform);
            Debug.Log(eventData.pointerDrag.transform.name);
        }
    }
}
