using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManger : MonoBehaviour
{
    public GameObject LevelEnd_Mobile;
    public GameObject LevelEnd_Win;
    public Sprite Cross;
    public Sprite Check;
    public Color Red;
    public Color Green;


    [SerializeField]
    private int temp, index;
    public int currentlevel;
    [SerializeField]
    private string si;

    [Header("Level1")]
    public GameObject Lift;
    public float[] PositionsForLevel1;
    public bool Execute = false;
    private float Endpoint;
  
    private int FloorAmount;
    private bool GoUp;

    [Header("Level2")]
    [SerializeField]
    private GameObject ufo;
    [SerializeField]
    private GameObject[] Levels;
    [SerializeField]
    private GameObject[] PutLocation;
    private Vector3 Endpoint2;
    private int LevelAmount;
    private bool HasBlock;
    [SerializeField]
    private GameObject Hook;
    [SerializeField]
    private GameObject EndPos;

    [Header("Level3")]
    [SerializeField]
    private GameObject[] Positions;
    [SerializeField]
    private bool HasBall=false;
    [SerializeField]
    private GameObject[] balls;
    [SerializeField]
    private GameObject Target;
    [SerializeField]
    private Vector3 EndPoint3;
    [SerializeField]
    private int MoveAmount;
    [SerializeField]
    private bool Mid = true;




    [SerializeField]
    private string TestCaseCondition;



    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if(Execute)
        {
            if(currentlevel==1 || currentlevel==4)
            {

                if (GoUp)
                {
                    if (Lift.transform.position.y <= Endpoint)
                    {
                        Lift.transform.Translate(0, 0, 0.2f);
                    }
                    else
                    {
                        if (index == FloorAmount-1)
                        {
                            Debug.Log("End");
                            CheckForTestCases();
                            Execute = false;
                        }
                        else
                        {
                            StartCoroutine(LoadNextPosition());
                        }
                    }
                }
                else
                {
                    if (Lift.transform.position.y >= Endpoint)
                    {
                        Lift.transform.Translate(0, 0, -0.2f);
                    }
                    else
                    {
                        if (index == FloorAmount-1)
                        {
                            Debug.Log("End");
                            CheckForTestCases();
                            Execute = false;

                        }
                        else 
                        {
                            StartCoroutine(LoadNextPosition());
                           
                        }
                    }
                }
              
            }
            if(currentlevel==2)
            {
                if(HasBlock)
                {
              
                    Levels[si[temp] - 48].transform.parent.transform.position = Hook.transform.position;
                }
                ufo.gameObject.SetActive(true);
                if (ufo.transform.position != Endpoint2)
                {
                    ufo.transform.position = Vector3.MoveTowards(ufo.transform.position, Endpoint2, 60 * Time.deltaTime);
                }
                else if(ufo.transform.position==EndPos.transform.position)
                {
                    Execute = false;
                    ufo.gameObject.SetActive(false);
                }
                else
                {                  
                        if (temp == LevelAmount-1 && HasBlock==true)
                        {
                            Debug.Log("End");
                             Endpoint2 = EndPos.transform.position;
                            CheckForTestCases();
                             HasBlock = false;
                           
                          
                        }
                        else
                        {
                            StartCoroutine(LoadNextPosition2());
                            
                        }
                  
                }
            }
            if(currentlevel==3)
            {
                if(HasBall)
                {
                    balls[index].transform.position = Target.transform.position;
                }
                if (Target.transform.position != EndPoint3)
                {
                    Target.transform.position = Vector3.MoveTowards(Target.transform.position, EndPoint3, 90 * Time.deltaTime);
                }
                else
                {
                    if (temp == MoveAmount)
                    {
                        Debug.Log("End");
                        Endpoint2 = Positions[0].transform.position;
                        CheckForTestCases();
                        Execute = false;
                    }
                    else
                    {
                        StartCoroutine(LoadNextPosiion3());
                    }
                    
                }
            }
        }
    }

    public void ExecuteLevel2(string s)
    {
        Execute = true;
        si = s;
        LevelAmount = si.Length;
        temp = 0;
        Debug.Log(s[temp]);
            Endpoint2 = Levels[si[temp] - 48].transform.position;
        
    }

    public void CheckForTestCases()
    {
        bool Mobile = false;
#if UNITY_EDITOR
        Mobile = false;
       //Mobile = true;
#elif UNITY_IOS || UNITY_ANDROID
          Mobile = true;
#else
        Mobile = false;
#endif

        if (si.Contains(TestCaseCondition))
        {
            EnLevelendCanvas(true, Mobile);
            Debug.Log("LevelPass");

        }
        else
        {
            EnLevelendCanvas(false, Mobile);
            Debug.Log("LevelFailed");

        }
    }
    public void ExecuetLevel1(string s)
    {
        Execute = true;
        si = s;
        FloorAmount = si.Length;
        index = 0;
        if (si[index] - 48 > temp)
            GoUp = true;
        else
            GoUp = false;
        temp = si[index] - 48;
        Debug.Log(temp);
        Endpoint = PositionsForLevel1[temp];
    
    }
    public void ExecuteLevel3(string s)
    {
        index = -1;
        Execute = true;
        si = s;
        MoveAmount = si.Length;
        temp = 0;
        EndPoint3 = Positions[si[temp] - 48].transform.position;
    }
    public IEnumerator LoadNextPosition()
    {
        
        Execute = false;
        index++;
        Debug.Log("Waiting");
        yield return new WaitForSeconds(1f);
        Debug.Log("Waiting OVER");
        if (si[index] - 48 > temp)
            GoUp = true;
        else
            GoUp = false;
        temp = si[index] - 48;
        Endpoint = PositionsForLevel1[temp];
        Execute = true;
    }
    public IEnumerator LoadNextPosition2()
    {

        Execute = false;
       
        yield return new WaitForSeconds(0.5f);
        if(HasBlock)
        {
            temp++;
            Endpoint2 = Levels[si[temp] - 48].transform.position;
            HasBlock = false;
        }
        else
        {
            Endpoint2 = PutLocation[si[temp] - 48].transform.position;
            HasBlock = true;
        }
     
        Execute = true;
    }
    public IEnumerator LoadNextPosiion3()
    {
        HasBall = false;
        Execute = false;
        if (si[temp] - 48 == 0)
        {
            if (Mid)
            {
                EndPoint3 = Positions[1].transform.position;
                Mid = false;
                temp++;
                index++;
            }
            else
            {
                EndPoint3 = Positions[0].transform.position;
                Mid = true;
            }
           
        

        }
        else if (si[temp] - 48 == 1)
        {
            if (Mid)
            {
                EndPoint3 = Positions[3].transform.position;
                Mid = false;
                temp++;
            }
            else
            {
                EndPoint3 = Positions[2].transform.position;
                Mid = true;
            }
           
            HasBall = true;
        }
        else if (si[temp] - 48 == 2)
        {
            if (Mid)
            {
                EndPoint3 = Positions[5].transform.position;
                Mid = false;
                temp++;
            }
            else
            {
                EndPoint3 = Positions[4].transform.position;
                Mid = true;
            }
          
            HasBall = true;
        }
        else if (si[temp] - 48 == 3)
        {
            if (Mid)
            {
                EndPoint3 = Positions[7].transform.position;
                Mid = false;
                temp++;
            }
            else
            {
                EndPoint3 = Positions[6].transform.position;
                Mid = true;
            }
     
            HasBall = true;
        }
        yield return new WaitForSeconds(0.1f);
        Execute = true;
    }

    public  void SetNextPos()
    {
        Debug.Log("setpos");
        if(si[temp] -48 ==0)
        {
            if(Mid)
            {
                EndPoint3 = Positions[1].transform.position;
                Mid = false;
                temp++;
            }
            else
            {
                EndPoint3 = Positions[0].transform.position;
                Mid = true;
            }
        }
        else if(si[temp] -48 ==1)
        {
            if (Mid)
            {
                EndPoint3 = Positions[3].transform.position;
                Mid = false;
                temp++;
            }
            else
            {
                EndPoint3 = Positions[2].transform.position;
                Mid = true;
            }
        }
        else if (si[temp] - 48 == 2)
        {
            if (Mid)
            {
                EndPoint3 = Positions[5].transform.position;
                Mid = false;
                temp++;
            }
            else
            {
                EndPoint3 = Positions[4].transform.position;
                Mid = true;
            }
        }
        else if (si[temp] - 48 == 3)
        {
            if (Mid)
            {
                EndPoint3 = Positions[7].transform.position;
                Mid = false;
                temp++;
            }
            else
            {
                EndPoint3 = Positions[6].transform.position;
                Mid = true;
            }
        }
    }




    public void EnLevelendCanvas(bool LevelStatus,bool Mobile)
    {
        if(Mobile)
       {
            if (LevelStatus)
            {
                LevelEnd_Mobile.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Level Pass";
            }
            else
            {
                LevelEnd_Mobile.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Level Failed";
            }
            if (currentlevel == 1)
            {
                if (si.Contains("0123"))
                {
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Green;

                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Green;
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Green;
                    LevelEnd_Mobile.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Green;

                }
                else
                {
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Red;
                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Red;
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Red;
                    LevelEnd_Mobile.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Red;
                }
                LevelEnd_Mobile.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Test case 1";
                LevelEnd_Mobile.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Test case 2";
                LevelEnd_Mobile.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Test case 3";
                LevelEnd_Mobile.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Test case 4";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to Ground Floor";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to First Floor From Ground Floor";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to Second Floor From First Floor";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to Third Floor From Second Floor";





                LevelEnd_Mobile.transform.GetChild(2).gameObject.SetActive(true);
                LevelEnd_Mobile.transform.GetChild(3).gameObject.SetActive(true);
                LevelEnd_Mobile.transform.GetChild(4).gameObject.SetActive(true);
                LevelEnd_Mobile.transform.GetChild(5).gameObject.SetActive(true);
            }
            else if (currentlevel == 2)
            {
                if (si.Contains("543210"))
                {
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Green;

                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Green;
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Green;
                    LevelEnd_Mobile.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Green;
                    LevelEnd_Mobile.transform.GetChild(6).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(6).GetChild(0).GetComponent<Image>().color = Green;
                    LevelEnd_Mobile.transform.GetChild(7).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(7).GetChild(0).GetComponent<Image>().color = Green;

                }
                else
                {
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Red;
                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Red;
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Red;
                    LevelEnd_Mobile.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Red;
                    LevelEnd_Mobile.transform.GetChild(6).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(6).GetChild(0).GetComponent<Image>().color = Red;
                    LevelEnd_Mobile.transform.GetChild(7).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(7).GetChild(0).GetComponent<Image>().color = Red;
                }
                LevelEnd_Mobile.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Test case 1";
                LevelEnd_Mobile.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Test case 2";
                LevelEnd_Mobile.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Test case 3";
                LevelEnd_Mobile.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Test case 4";
                LevelEnd_Mobile.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "Test case 5";
                LevelEnd_Mobile.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "Test case 6";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Place Layer 6 On Proper Level";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Place Layer 5 On Proper Level";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Place Layer 4 On Proper Level";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Place Layer 3 On Proper Level";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Place Layer 2 On Proper Level";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Place Layer 1 On Proper Level";
                LevelEnd_Mobile.transform.GetChild(2).gameObject.SetActive(true);
                LevelEnd_Mobile.transform.GetChild(3).gameObject.SetActive(true);
                LevelEnd_Mobile.transform.GetChild(4).gameObject.SetActive(true);
                LevelEnd_Mobile.transform.GetChild(5).gameObject.SetActive(true);
                LevelEnd_Mobile.transform.GetChild(6).gameObject.SetActive(true);
                LevelEnd_Mobile.transform.GetChild(7).gameObject.SetActive(true);
            }
            else if (currentlevel == 3)
            {

                if(si.Split('1').Length==6)
                {
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Green;
                }
                else
                {
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Red;
                }
                if (si.Split('2').Length == 6)
                {
                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Green;
                }
                else
                {
                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Red;
                }
                if (si.Split('3').Length == 6)
                {
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Green;
                }
                else
                {
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Red;
                }

                LevelEnd_Mobile.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Test case 1";
                LevelEnd_Mobile.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Test case 2";
                LevelEnd_Mobile.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Test case 3";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Move all BasketBall in BasketBall Basket";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Move all FootBall in FootBall Basket";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Move all SoccerBall in SoccerBall Basket";
                LevelEnd_Mobile.transform.GetChild(2).gameObject.SetActive(true);
                LevelEnd_Mobile.transform.GetChild(3).gameObject.SetActive(true);
                LevelEnd_Mobile.transform.GetChild(4).gameObject.SetActive(true);

            }
            else if (currentlevel == 4)
            {
                if (si.Contains("3210"))
                {
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Green;

                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Green;
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Green;
                    LevelEnd_Mobile.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Check;
                    LevelEnd_Mobile.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Green;

                }
                else
                {
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Red;
                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Red;
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Red;
                    LevelEnd_Mobile.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Cross;
                    LevelEnd_Mobile.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Red;
                }
                LevelEnd_Mobile.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Test case 1";
                LevelEnd_Mobile.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Test case 2";
                LevelEnd_Mobile.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Test case 3";
                LevelEnd_Mobile.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Test case 4";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to Third Floor";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to Second Floor From Third Floor";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to First Floor From Second Floor";
                LevelEnd_Mobile.transform.GetChild(11).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to Ground Floor From First Floor";
                LevelEnd_Mobile.transform.GetChild(2).gameObject.SetActive(true);
                LevelEnd_Mobile.transform.GetChild(3).gameObject.SetActive(true);
                LevelEnd_Mobile.transform.GetChild(4).gameObject.SetActive(true);
                LevelEnd_Mobile.transform.GetChild(5).gameObject.SetActive(true);
            }
            LevelEnd_Mobile.transform.parent.gameObject.SetActive(true);
            LevelEnd_Win.transform.parent.gameObject.SetActive(false);
        }
        else
        {
                if (LevelStatus)
                {
                    LevelEnd_Win.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Level Pass";
                }
                else
                {
                    LevelEnd_Win.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Level Failed";
                }
                if (currentlevel == 1)
                {
                    if (si.Contains("0123"))
                    {
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Green;

                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Green;
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Green;
                        LevelEnd_Win.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Green;

                    }
                    else
                    {
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Red;
                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Red;
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Red;
                        LevelEnd_Win.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Red;
                    }
                    LevelEnd_Win.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Test case 1";
                    LevelEnd_Win.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Test case 2";
                    LevelEnd_Win.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Test case 3";
                    LevelEnd_Win.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Test case 4";
                LevelEnd_Win.transform.GetChild(11).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to Ground Floor";
                LevelEnd_Win.transform.GetChild(11).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to First Floor From Ground Floor";
                LevelEnd_Win.transform.GetChild(11).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to Second Floor From First Floor";
                LevelEnd_Win.transform.GetChild(11).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to Third Floor From Second Floor";
                LevelEnd_Win.transform.GetChild(2).gameObject.SetActive(true);
                    LevelEnd_Win.transform.GetChild(3).gameObject.SetActive(true);
                    LevelEnd_Win.transform.GetChild(4).gameObject.SetActive(true);
                    LevelEnd_Win.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (currentlevel == 2)
                {
                    if (si.Contains("543210"))
                    {
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Green;

                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Green;
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Green;
                        LevelEnd_Win.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Green;
                        LevelEnd_Win.transform.GetChild(6).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(6).GetChild(0).GetComponent<Image>().color = Green;
                        LevelEnd_Win.transform.GetChild(7).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(7).GetChild(0).GetComponent<Image>().color = Green;

                    }
                    else
                    {
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Red;
                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Red;
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Red;
                        LevelEnd_Win.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Red;
                        LevelEnd_Win.transform.GetChild(6).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(6).GetChild(0).GetComponent<Image>().color = Red;
                        LevelEnd_Win.transform.GetChild(7).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(7).GetChild(0).GetComponent<Image>().color = Red;
                    }
                    LevelEnd_Win.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Test case 1";
                    LevelEnd_Win.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Test case 2";
                    LevelEnd_Win.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Test case 3";
                    LevelEnd_Win.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Test case 4";
                    LevelEnd_Win.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "Test case 5";
                    LevelEnd_Win.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "Test case 6";
                LevelEnd_Win.transform.GetChild(11).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Place Layer 6 On Proper Level";
                LevelEnd_Win.transform.GetChild(11).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Place Layer 5 On Proper Level";
                LevelEnd_Win.transform.GetChild(11).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Place Layer 4 On Proper Level";
                LevelEnd_Win.transform.GetChild(11).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Place Layer 3 On Proper Level";
                LevelEnd_Win.transform.GetChild(11).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Place Layer 2 On Proper Level";
                LevelEnd_Win.transform.GetChild(11).GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Place Layer 1 On Proper Level";
                LevelEnd_Win.transform.GetChild(2).gameObject.SetActive(true);
                    LevelEnd_Win.transform.GetChild(3).gameObject.SetActive(true);
                    LevelEnd_Win.transform.GetChild(4).gameObject.SetActive(true);
                    LevelEnd_Win.transform.GetChild(5).gameObject.SetActive(true);
                    LevelEnd_Win.transform.GetChild(6).gameObject.SetActive(true);
                    LevelEnd_Win.transform.GetChild(7).gameObject.SetActive(true);
                }
                else if (currentlevel == 3)
                {

                    if (si.Split('1').Length == 6)
                    {
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Green;
                    }
                    else
                    {
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Red;
                    }
                    if (si.Split('2').Length == 6)
                    {
                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Green;
                    }
                    else
                    {
                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Red;
                    }
                    if (si.Split('3').Length == 6)
                    {
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Green;
                    }
                    else
                    {
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Red;
                    }

                    LevelEnd_Win.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Test case 1";
                    LevelEnd_Win.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Test case 2";
                    LevelEnd_Win.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Test case 3";
                LevelEnd_Win.transform.GetChild(11).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Move all BasketBall in BasketBall Basket";
                LevelEnd_Win.transform.GetChild(11).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Move all FootBall in FootBall Basket";
                LevelEnd_Win.transform.GetChild(11).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Move all SoccerBall in SoccerBall Basket";
                LevelEnd_Win.transform.GetChild(2).gameObject.SetActive(true);
                    LevelEnd_Win.transform.GetChild(3).gameObject.SetActive(true);
                    LevelEnd_Win.transform.GetChild(4).gameObject.SetActive(true);

                }
                else if (currentlevel == 4)
                {
                    if (si.Contains("3210"))
                    {
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Green;

                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Green;
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Green;
                        LevelEnd_Win.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Check;
                        LevelEnd_Win.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Green;

                    }
                    else
                    {
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Red;
                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Red;
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Red;
                        LevelEnd_Win.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Cross;
                        LevelEnd_Win.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Red;
                    }
                    LevelEnd_Win.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Test case 1";
                    LevelEnd_Win.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Test case 2";
                    LevelEnd_Win.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Test case 3";
                    LevelEnd_Win.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Test case 4";
                LevelEnd_Win.transform.GetChild(11).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to Third Floor";
                LevelEnd_Win.transform.GetChild(11).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to Second Floor From Third Floor";
                LevelEnd_Win.transform.GetChild(11).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to First Floor From Second Floor";
                LevelEnd_Win.transform.GetChild(11).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go to Ground Floor From First Floor";
                LevelEnd_Win.transform.GetChild(2).gameObject.SetActive(true);
                    LevelEnd_Win.transform.GetChild(3).gameObject.SetActive(true);
                    LevelEnd_Win.transform.GetChild(4).gameObject.SetActive(true);
                    LevelEnd_Win.transform.GetChild(5).gameObject.SetActive(true);
                }

                LevelEnd_Mobile.transform.parent.gameObject.SetActive(false);
            LevelEnd_Win.transform.parent.gameObject.SetActive(true);
        }
    }
}
