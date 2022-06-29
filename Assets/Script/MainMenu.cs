using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public bool IsMobile = false;

    public GameObject MobileMainMenu;
    public GameObject WinMainMenu;
    public GameObject MobileLevels;
    public GameObject WinLevels;

    private void Start()
    {

#if UNITY_EDITOR
       IsMobile = false;
        // IsMobile = true;
#elif UNITY_IOS || UNITY_ANDROID
       IsMobile = true;
#else
       
        IsMobile = false;
#endif



        if (IsMobile)
        {
            MobileMainMenu.SetActive(true);
            MobileLevels.SetActive(false);
            WinMainMenu.SetActive(false);
            WinLevels.SetActive(false);
        }
        else
        {
            MobileMainMenu.SetActive(false);
            MobileLevels.SetActive(false);
            WinMainMenu.SetActive(true);
            WinLevels.SetActive(false);
        }
    }
    public void ShowLevels()
    {
        if (IsMobile)
        {
            MobileLevels.SetActive(true);
        }
        else
        {
            WinLevels.SetActive(true);

        }
    }
}
