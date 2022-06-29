using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public Animator ButtonAnim;
    public bool Show = true;
    public bool Ques = false;
    public GameObject Quess;




        public void ExitApp()
    {
        Application.Quit();
    }

    public void Login()
    {

    }

    public void Logout()
    {

    }







    public void ShowEn()
    {
        if(Show)
        {
            Show = false;
            ButtonAnim.SetInteger("Show", 1);
        }
        else
        {
            Show = true;
            ButtonAnim.SetInteger("Show", 2);
        }
    }


    public void ChangeScene(int index)
    {
            SceneManager.LoadScene(index);
    }

    public void QuestionEn()
    {
        if(Ques)
        {
            Ques = false;
            Quess.SetActive(false);
        }
        else
        {
            Ques = true;
            Quess.SetActive(true);
        }
    }
}
