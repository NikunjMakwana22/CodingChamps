using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{   [SerializeField]
    public string Name;
    public TextMeshProUGUI Title;
    public Color BlockColor,Black;
    public string type;
    public int Code;
    public bool exception = true;
    public bool exception2 = false;


    public int NumberofArgs = 0;
    public int arg1, arg2, arg3, arg4, arg5, arg6 = 0;
    public int temp;


    public bool IsExecuted = false;
    public bool IsRootBlock = false;
    public bool HasSlot = false;
    public bool FirstTime = false;
    public bool IsMobile = false;

    private void Start()
    {

#if UNITY_EDITOR
    IsMobile = false;
    //    IsMobile = true;
#elif UNITY_IOS || UNITY_ANDROID
       IsMobile = true;
#else
       
        IsMobile = false;
#endif

        if (type == "S")
        {
            Title.text = Name;
            GetComponent<Image>().color = BlockColor;
            if(!IsMobile)
            {
                transform.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
                transform.GetComponent<RectTransform>().localPosition = new Vector3(376, 150, 0);
            }
            else
            {
                transform.GetComponent<RectTransform>().localScale = new Vector3(1.4f, 1.4f, 1.4f);
                transform.GetComponent<RectTransform>().localPosition = new Vector3(140, -201, 0);
            }
        }
        else if(type=="C")
        {
            Title.text = Name;
            transform.GetChild(0).GetChild(0).GetComponent<Image>().color = BlockColor;
            transform.GetChild(0).GetChild(2).GetComponent<Image>().color = BlockColor;
            transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = BlockColor;
            if (!IsMobile)
            {
                transform.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
                transform.GetComponent<RectTransform>().localPosition = new Vector3(376, 150, 0);
            }
            else
            {
                transform.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
                transform.GetComponent<RectTransform>().localPosition = new Vector3(140, -201, 0);
            }
        }
        else if(type=="I" && exception2)
        {
            exception2 = false;
            Title.text = Name;
            GetComponent<Image>().color = BlockColor;
            if (!IsMobile)
            {
                transform.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
                transform.GetComponent<RectTransform>().localPosition = new Vector3(376, 150, 0);
            }
            else
            {
                transform.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
                transform.GetComponent<RectTransform>().localPosition = new Vector3(140, -201, 0);
            }

        }
        else if(type=="I")
        {
            if (!IsMobile)
            {
                transform.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
                transform.GetComponent<RectTransform>().localPosition = new Vector3(356, 150, 0);
            }
            else
            {
                transform.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
                transform.GetComponent<RectTransform>().localPosition = new Vector3(140, -201, 0);
            }
        }

    }


    public void changeColorToBlack()
    {
        if (type == "S")
        {
            GetComponent<Image>().color = BlockColor;
        }
        else if (type == "C")
        {
            transform.GetChild(0).GetChild(0).GetComponent<Image>().color = Black;
            transform.GetChild(0).GetChild(2).GetComponent<Image>().color = Black;
            transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Black;
        }
        else
        {
        }
    }
    public void changeColorToNormal()
    {
        if (type == "S")
        {
            GetComponent<Image>().color = BlockColor;
        }
        else if (type == "C")
        {
            transform.GetChild(0).GetChild(0).GetComponent<Image>().color = BlockColor;
            transform.GetChild(0).GetChild(2).GetComponent<Image>().color = BlockColor;
            transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = BlockColor;
        }
        else
        {
        }
    }
}
