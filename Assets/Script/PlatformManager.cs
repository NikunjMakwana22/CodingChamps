using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{

    public GameObject Mobile_Canvas;
    public GameObject Win_Canvas;

    public CodeGenerator CG;

    public GameObject MainCamera;
    public GameObject CamForMobile;
    public GameObject CamForWin;

    public GameObject FirstForMobile;
    public GameObject FirstForWin;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        Mobile_Canvas.SetActive(false);
        Win_Canvas.SetActive(true);
        CG.FirstBlockParent = FirstForWin;
        MainCamera.transform.position = CamForWin.transform.position;
        //Mobile_Canvas.SetActive(true);
        //Win_Canvas.SetActive(false);
        //CG.FirstBlockParent = FirstForMobile;
        //MainCamera.transform.position = CamForMobile.transform.position;
        Debug.Log("Unity Editor");
#elif UNITY_IOS || UNITY_ANDROID
         Mobile_Canvas.SetActive(true);
        Win_Canvas.SetActive(false);
        CG.FirstBlockParent = FirstForMobile;
        MainCamera.transform.position = CamForMobile.transform.position;
         Debug.Log("Unity iOS");
#else
         Mobile_Canvas.SetActive(false);
        Win_Canvas.SetActive(true);
        CG.FirstBlockParent = FirstForWin;
        MainCamera.transform.position = CamForWin.transform.position;
          Debug.Log("Any other platform");

#endif

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
