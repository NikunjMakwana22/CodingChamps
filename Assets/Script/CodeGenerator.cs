using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeGenerator : MonoBehaviour
{
    public GameObject FirstBlockParent;
    [SerializeField]
    private GameObject CurrentBlock,NextBlock,PrevBlock,CurrentBlockForReset;
    //Initialization variable
    private int Start;
    //End variable
    private int End;
    //conditions 0= (<) ,1 = (>),2 = (<=),3 = (>=)
    private int Condition;
    //Flow of Loop 0 = (++) , 1 = (--)
    private int Flow;
    public string EndResult="";
    public int level=0;
    public int Big, Small;
    public bool ElseCon=false;
    public int temppp = 0;
    public GameObject[] Balls;
    public int ball=0;
    [SerializeField]
    public LevelManger LM;
    


    public ButtonManager bm;

    public int TypeOfBall;
    public int tempppp = 0;



    public void ExecuteLevel()
    {
        StartCoroutine(ExecuteWithAnimation());
    }


    public void TestRun()
    {
        ExecuteLevel();
    }


    public void StartCode()
    {
        ball = 0;
        EndResult = "";
        tempppp = 0;
        CurrentBlock = FirstBlockParent.transform.GetChild(1).gameObject;
        CurrentBlock.GetComponent<BlockManager>().IsRootBlock = true;
        GoThrughAllBlockInside(CheckForCondition());
    }


    private int GetNumberOfBlocksInside()
    {
        if (CurrentBlock.GetComponent<BlockManager>().HasSlot)
        {
            int NumberOfBlocks = 0;
            NumberOfBlocks = CurrentBlock.transform.GetChild(0).GetChild(1).GetChild(0).childCount;
            return NumberOfBlocks;
        }
        else
        {
            return 0;
        }
    }



    public void resetInsideBlocks(GameObject ResetBlock)
    {
        if (ResetBlock.GetComponent<BlockManager>().HasSlot)
        {
          //Debug.Log("reset");
            int k = ResetBlock.transform.GetChild(0).GetChild(1).GetChild(0).childCount;
            for (int num = k; num >= 1; num--)
            {
               
                CurrentBlockForReset = ResetBlock.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(num - 1).gameObject;
                int rcode = CurrentBlockForReset.GetComponent<BlockManager>().Code;
                switch (rcode)
                {
                    case 1:
                        CurrentBlockForReset.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value = CurrentBlockForReset.GetComponent<BlockManager>().arg1;
                        break;
                    case 2:
                        CurrentBlockForReset.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value = CurrentBlockForReset.GetComponent<BlockManager>().arg1;
                        break;
                    case 3:
                        CurrentBlockForReset.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value = CurrentBlockForReset.GetComponent<BlockManager>().arg1;
                        break;
                    case 4:
                        CurrentBlockForReset.GetComponent<BlockManager>().exception2 = false;
                        break;
                    case 5:
                        CurrentBlockForReset.GetComponent<BlockManager>().exception2 = false;
                        break;
                    case 6:
                        Debug.Log("RESET");
                        CurrentBlockForReset.GetComponent<BlockManager>().exception2 = false;
                        Debug.Log(CurrentBlockForReset.GetComponent<BlockManager>().exception2);
                        break;
                    case 7:
                        CurrentBlockForReset.GetComponent<BlockManager>().exception2 = false;
                        break;
                    case 8:
                        CurrentBlockForReset.GetComponent<BlockManager>().exception2 = false;
                        break;
                    case 9:
                        CurrentBlockForReset.GetComponent<BlockManager>().exception2 = false;
                        break;
                }
                CurrentBlock.GetComponent<BlockManager>().IsExecuted = false;
                temppp = -1;
                // Debug.Log("Reset Complete " + CurrentBlockForReset);
            }
        }
    }



    public void GoThrughAllBlockInside(bool CheckForCond)
    {
        if (CheckForCond)
            {
            if(temppp== 2 )
            {
                CurrentBlock = CurrentBlock.transform.GetChild(0).GetChild(5).GetChild(0).GetChild(0).gameObject;
               
            }
            else if(temppp==1)
            {
                CurrentBlock = CurrentBlock.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).gameObject;
               
            }
            else if(ElseCon)
            {
                CurrentBlock = CurrentBlock.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).gameObject;
            }
              //  Debug.Log(CurrentBlock.name);
                int i = GetNumberOfBlocksInside();
              //   Debug.Log(i);
                int j = i;
            repeat:
          
                if (i == 0)
                {
                    if(temppp==1)
                    {
                    Debug.Log("ELSE IF");
                    GoThrughAllBlockInside(CheckForCondition());
                }
                    else if(temppp==2)
                   {
                    Debug.Log("ELSE");
                    GoThrughAllBlockInside(CheckForCondition());
                }
                    if(ElseCon)
                    {
                    GoThrughAllBlockInside(CheckForCondition());
                    ElseCon = false;
                    }
                    CurrentBlock.GetComponent<BlockManager>().IsExecuted = true;
                    Debug.Log("All Inside Block Executed " + CurrentBlock.GetComponent<BlockManager>().Title.text);
                if (!CurrentBlock.GetComponent<BlockManager>().IsRootBlock)
                {
                    CurrentBlock = CurrentBlock.transform.parent.parent.parent.parent.gameObject;
                    resetInsideBlocks(CurrentBlock);
                    GoThrughAllBlockInside(CheckForCondition());
                }
               // Debug.Log(CurrentBlock.name);
                   
                    //if (CurrentBlock.name != "Main")
                    //{
                    //    resetInsideBlocks(CurrentBlock);
                    //} 
                }
                else
                {
                    CurrentBlock = CurrentBlock.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(i - 1).gameObject;
                    GoThrughAllBlockInside(CheckForCondition());
                    i--;
                    goto repeat;
                }
        }
        else
        {
                Debug.Log("Condition Not Satisfied " + CurrentBlock.name);
            }
        
    }



    public bool CheckForCondition()
    {
        temppp = 0;
        int fcode = CurrentBlock.GetComponent<BlockManager>().Code;
        if (!CurrentBlock.GetComponent<BlockManager>().FirstTime)
        {
            CurrentBlock.GetComponent<BlockManager>().FirstTime = true;
            switch (fcode)
            {
                case 1:
                    CurrentBlock.GetComponent<BlockManager>().arg1 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg2 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg3 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg4 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    break;
                case 2:
                    CurrentBlock.GetComponent<BlockManager>().arg1 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg2 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg3 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg4 = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    break;
                case 3:
                    CurrentBlock.GetComponent<BlockManager>().arg1 = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg2 = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg3 = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg4 = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    break;
                case 4:
                    CurrentBlock.GetComponent<BlockManager>().arg1 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg2 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg3 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    break;
                case 5:
                    CurrentBlock.GetComponent<BlockManager>().arg1 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg2 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg3 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    break;
                case 6:
                    CurrentBlock.GetComponent<BlockManager>().arg1 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg2 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg3 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg4 = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg5 = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    CurrentBlock.GetComponent<BlockManager>().arg6 = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    temppp = 0;
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    CurrentBlock.GetComponent<BlockManager>().arg1 = CurrentBlock.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    break;
                case 21:
                    CurrentBlock.GetComponent<BlockManager>().arg1 = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    break;
                case 22:
                    break;
                case 31:

                    break;
                case 32:
                    break;
                case 33:
                    break;
                case 34:
                    break;
            }
        }

        // code 1 = for
        bool temp = false;
        switch (fcode)
        {
            case 1:
                int fstart = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                int frange = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                int fcondition = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                int finc_dec = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;

                Debug.Log(fstart + " " + fcondition + " " + frange + " " + finc_dec);
                if (fcondition == 0)
                {
                    if (fstart < frange)
                    {
                        temp = true;
                        Funcinc_dec(finc_dec, fcode);
                    }
                }
                else if (fcondition == 1)
                {
                    if (fstart > frange)
                    {
                        temp = true;
                        Funcinc_dec(finc_dec, fcode);
                    }
                }
                else if (fcondition == 2)
                {

                    if (fstart <= frange && CurrentBlock.GetComponent<BlockManager>().exception)
                    {
                        if (fstart == frange)
                            CurrentBlock.GetComponent<BlockManager>().exception = false;
                        temp = true;
                        Funcinc_dec(finc_dec, fcode);
                    }

                }
                else if (fcondition == 3)
                {

                    if (fstart >= frange && CurrentBlock.GetComponent<BlockManager>().exception)
                    {
                        if (fstart == frange)
                            CurrentBlock.GetComponent<BlockManager>().exception = false;
                        temp = true;
                        Funcinc_dec(finc_dec, fcode);
                    }


                }
                break;
            case 2:
                int wstart = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                int wrange = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                int wcondition = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                int winc_dec = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;

                Debug.Log(wstart + " " + wcondition + " " + wrange + " " + winc_dec);
                if (wcondition == 0)
                {
                    if (wstart < wrange)
                    {
                        temp = true;
                        Funcinc_dec(winc_dec, fcode);
                    }
                }
                else if (wcondition == 1)
                {
                    if (wstart > wrange)
                    {
                        temp = true;
                        Funcinc_dec(winc_dec, fcode);
                    }
                }
                else if (wcondition == 2)
                {

                    if (wstart <= wrange && CurrentBlock.GetComponent<BlockManager>().exception)
                    {
                        if (wstart == wrange)
                            CurrentBlock.GetComponent<BlockManager>().exception = false;
                        temp = true;
                        Funcinc_dec(winc_dec, fcode);
                    }

                }
                else if (wcondition == 3)
                {

                    if (wstart >= wrange && CurrentBlock.GetComponent<BlockManager>().exception)
                    {
                        if (wstart == wrange)
                            CurrentBlock.GetComponent<BlockManager>().exception = false;
                        temp = true;
                        Funcinc_dec(winc_dec, fcode);
                    }
                }
                break;
            case 3:
                if (CurrentBlock.GetComponent<BlockManager>().exception2)
                {
                    int dstart = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    int drange = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    int dcondition = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    int dinc_dec = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    Debug.Log(dstart + " " + dcondition + " " + drange + " " + dinc_dec);
                    if (dcondition == 0)
                    {
                        if (dstart < drange)
                        {
                            temp = true;
                            Funcinc_dec(dinc_dec, fcode);
                        }
                    }
                    else if (dcondition == 1)
                    {
                        if (dstart > drange)
                        {
                            temp = true;
                            Funcinc_dec(dinc_dec, fcode);
                        }
                    }
                    else if (dcondition == 2)
                    {

                        if (dstart <= drange && CurrentBlock.GetComponent<BlockManager>().exception)
                        {
                            if (dstart == drange)
                                CurrentBlock.GetComponent<BlockManager>().exception = false;
                            temp = true;
                            Funcinc_dec(dinc_dec, fcode);
                        }

                    }
                    else if (dcondition == 3)
                    {

                        if (dstart >= drange && CurrentBlock.GetComponent<BlockManager>().exception)
                        {
                            if (dstart == drange)
                                CurrentBlock.GetComponent<BlockManager>().exception = false;
                            temp = true;
                            Funcinc_dec(dinc_dec, fcode);
                        }
                    }
                }
                else
                {
                    CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                    temp = true;
                }
                break;
            case 4:
                int istart = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                int irange = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                int icondition = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                if (icondition == 0)
                {
                 
                    if (istart < irange && !CurrentBlock.GetComponent<BlockManager>().exception2)
                    {
                        temp = true;
                        CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                    }

                }
                else if (icondition == 1)
                {  
                    if (istart > irange && !CurrentBlock.GetComponent<BlockManager>().exception2)
                    {
                        temp = true;
                        CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                    }
                }
                else if (icondition == 2)
                {
                  
                    if (istart <= irange && !CurrentBlock.GetComponent<BlockManager>().exception2)
                    {
                        temp = true;
                        CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                    }
                }
                else if (icondition == 3)
                {
                   
                    if (istart >= irange && !CurrentBlock.GetComponent<BlockManager>().exception2)
                    {
                        temp = true;
                        CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                    }
                }
                break;
            case 5:
                int iestart = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                int ierange = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                int iecondition = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                if (iecondition == 0)
                {

                    if (iestart < ierange && !CurrentBlock.GetComponent<BlockManager>().exception2)
                    {
                        temp = true;
                        CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                    }
                    else if (!CurrentBlock.GetComponent<BlockManager>().exception2)
                    {
                        temp = true;
                        CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                        ElseCon = true;
                    }

                }
                else if (iecondition == 1)
                {
                    if (iestart > ierange && !CurrentBlock.GetComponent<BlockManager>().exception2)
                    {
                        temp = true;
                        CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                    }
                    else if (!CurrentBlock.GetComponent<BlockManager>().exception2)
                    {
                        temp = true;
                        CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                        ElseCon = true;
                    }
                }
                else if (iecondition == 2)
                {

                    if (iestart <= ierange && !CurrentBlock.GetComponent<BlockManager>().exception2)
                    {
                        temp = true;
                        CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                    }
                    else if (!CurrentBlock.GetComponent<BlockManager>().exception2)
                    {
                        temp = true;
                        CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                        ElseCon = true;
                    }
                }
                else if (iecondition == 3)
                {

                    if (iestart >= ierange && !CurrentBlock.GetComponent<BlockManager>().exception2)
                    {
                        temp = true;
                        CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                    }
                    else if (!CurrentBlock.GetComponent<BlockManager>().exception2)
                    {
                        temp = true;
                        CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                        ElseCon = true;
                    }
                }
                break;
            case 6:
                    int ieestart = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    int ieerange = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    int ieecondition = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    int ieestart1 = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    int ieerange1 = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    int ieecondition1 = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    jump:
                    if (temppp == 0)
                    {
                        if (ieecondition == 0)
                        {
                            if (ieestart < ieerange && !CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temp = true;
                                ElseCon = false;
                            Debug.Log("exc2 = true");
                                CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                            }
                            else if (!CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temppp = 1;
                                goto jump;
                            }
                        }
                        else if (ieecondition == 1)
                        {
                            if (ieestart > ieerange && !CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temp = true;
                                ElseCon = false;
                            Debug.Log("exc2 = true");

                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                            }
                            else if (!CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temppp = 1;
                                goto jump;

                            }
                        }
                        else if (ieecondition == 2)
                        {

                            if (ieestart <= ieerange && !CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temp = true;
                                ElseCon = false;
                            Debug.Log("exc2 = true");

                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                            }
                            else if (!CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temppp = 1;
                                goto jump;

                            }
                        }
                        else if (ieecondition == 3)
                        {

                            if (ieestart >= ieerange && !CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temp = true;
                                ElseCon = false;
                            Debug.Log("exc2 = true");

                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                            }
                            else if (!CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temppp = 1;
                                goto jump;
                            }
                        }
                    }
                    else if (temppp == 1)
                    {
                        if (ieecondition1 == 0)
                        {

                            if (ieestart1 < ieerange1 && !CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temp = true;
                                ElseCon = false;
                            Debug.Log("exc2 = true");

                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                            }
                            else if (!CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temppp = 2;
                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                            temp = true;
                                
                            }

                        }
                        else if (ieecondition1 == 1)
                        {
                            if (ieestart1 > ieerange1 && !CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temp = true;
                                ElseCon = false;
                            Debug.Log("exc2 = true");

                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                            }
                            else if (!CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temppp = 2;
                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                            temp = true;
                               
                            }
                        }
                        else if (ieecondition1 == 2)
                        {

                            if (ieestart1 <= ieerange1 && !CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temp = true;
                                ElseCon = false;
                            Debug.Log("exc2 = true");

                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                            }
                            else if (!CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temppp = 2;
                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                            temp = true;
                                
                            }
                        }
                        else if (ieecondition1 == 3)
                        {

                            if (ieestart1 >= ieerange1 && !CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temp = true;
                                ElseCon = false;
                            Debug.Log("exc2 = true");

                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                            }
                            else if (!CurrentBlock.GetComponent<BlockManager>().exception2)
                            {
                                temppp = 2;
                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                            temp = true;
                             
                            }
                        }
                    }
                
                break;
            case 7:
                if (ball == Balls.Length ||  CurrentBlock.GetComponent<BlockManager>().exception2)
                {
                    temp = false;
                }
                else
                {
                    TypeOfBall = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;

                    Debug.Log(TypeOfBall);
                    if (TypeOfBall == 0)
                    {
                        if (Balls[ball].name == "BasketBall" )
                        {
                            temp = true;
                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                        }
                    }
                    else if (TypeOfBall == 1)
                    {
                        if (Balls[ball].name == "FootBall")
                        {

                            temp = true;
                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                        }
                    }
                    else if (TypeOfBall == 2)
                    {
                        if (Balls[ball].name == "SoccerBall")
                        {

                            temp = true;
                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                        }
                    }


                    Debug.Log("Ball " + ball);
                    ball++;
                    Debug.Log("Ball after ++  " + ball);
                }
                break;
            case 8:
                if (ball == Balls.Length || CurrentBlock.GetComponent<BlockManager>().exception2)
                {
                    temp = false;
                }
                else
                {
                    TypeOfBall = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;

                    Debug.Log(TypeOfBall);
                    if (TypeOfBall == 0)
                    {
                        if (Balls[ball].name == "BasketBall")
                        {
                            temp = true;
                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                        }
                    }
                    else if (TypeOfBall == 1)
                    {
                        if (Balls[ball].name == "FootBall")
                        {

                            temp = true;
                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                        }
                    }
                    else if (TypeOfBall == 2)
                    {
                        if (Balls[ball].name == "SoccerBall")
                        {

                            temp = true;
                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                        }
                    }
                    Debug.Log("Ball " + ball);
                    ball++;
                    Debug.Log("Ball after ++  " + ball);
                    if (!temp)
                    {
                        CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                        temp = true;
                        ElseCon = true;
                    }
                }
                break;
            case 9:
                if (ball == Balls.Length || CurrentBlock.GetComponent<BlockManager>().exception2)
                {
                    temp = false;
                }
                else
                {
                    jump1:
                    if (temppp == 0)
                    {

                        TypeOfBall = CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value; 
                     }
                    else if(temppp==1)
                    {
                        TypeOfBall = CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                    }


                    if (TypeOfBall == 0)
                    {
                        if (Balls[ball].name == "BasketBall")
                        {
                            temp = true;
                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                        }
                    }
                    else if (TypeOfBall == 1)
                    {
                        if (Balls[ball].name == "FootBall")
                        {

                            temp = true;
                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                        }
                    }
                    else if (TypeOfBall == 2)
                    {
                        if (Balls[ball].name == "SoccerBall")
                        {

                            temp = true;
                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                        }
                    }
                    if (!temp)
                    {
                        temppp++;
                        if(temppp==2)
                        {
                            CurrentBlock.GetComponent<BlockManager>().exception2 = true;
                            temp = true;
                        }
                        else
                        {
                            goto jump1;
                        }
                    }
                    ball++;
                }
              
                break;
            case 10:
                if (CurrentBlock.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value == 0)
                {
                    if (level + 1 <= 3)
                    {
                        level = level + 1;
                    }
                    EndResult = EndResult + level;
                    
                }
                else if(CurrentBlock.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value==1)
                {
                    if (level - 1 >= 0)
                    {
                        level = level - 1;
                    }
                    EndResult = EndResult + level;
                    
                }
                else if (CurrentBlock.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value == 2)
                {
                    level = 0;
                    EndResult = EndResult + level;
                   
                }
                else
                {
                    level = CurrentBlock.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value - 2;
                    EndResult = EndResult + level;                   
                }
                temp = true;
                tempppp++;
                break;
            case 21:
                if (CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value == 0)
                {
                    EndResult = EndResult + Big;
                    if (Big > 0)
                    {
                        Big = Big - 1;
                    }


                }
                else if (CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value == 1)
                {

                    if (Small < 6)
                    {
                        Small = Small + 1;
                    }
                }
                else
                {
                   EndResult=EndResult + (CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value - 1 );
                }
                break;
            case 22:

                break;
            case 31:
               
                Debug.Log("TestBlock");
                ElseCon = false;
                break;
            case 32:
               
                Debug.Log("TestBlock2");
                ElseCon = false;
                break;
            case 33:
             
                Debug.Log("TestBlock3");
                ElseCon = false;
                break;
            case 34:
                TypeOfBall = CurrentBlock.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value;
                if (TypeOfBall == 0)
                {
                    Debug.Log("bb");
                    EndResult = EndResult + 0.ToString();
                    EndResult = EndResult + (TypeOfBall+1).ToString();
                }
                else if (TypeOfBall == 1)
                {
                    Debug.Log("ff");
                    EndResult = EndResult + 0.ToString();
                    EndResult = EndResult + (TypeOfBall + 1).ToString();
                }
                else if (TypeOfBall == 2)
                {
                    Debug.Log("sb");
                    EndResult = EndResult + 0.ToString();
                    EndResult = EndResult + (TypeOfBall + 1).ToString();
                }

                ElseCon = false;
                break;
        }
        return temp;
    }


        public void Funcinc_dec(int finc_dec,int code)
    {
        if(finc_dec==0)
        {
            switch(code)
            {
                case 1:
                    CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value++;
                    break;
                case 2:
                    CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value++;
                    break;
                case 3:
                    CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value++;
                    break;
            }
        }
        else if(finc_dec==1)
        {
            switch (code)
            {
                case 1:
                    CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value--;
                    break;
                case 2:
                    CurrentBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value--;
                    break;
                case 3:
                    CurrentBlock.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Dropdown>().value--;
                    break;
            }
        }
    }

        public IEnumerator ExecuteWithAnimation()
        {
#if UNITY_EDITOR
    // bm.ShowEn();
#elif UNITY_IOS || UNITY_ANDROID
            bm.ShowEn();
#else

#endif

        yield return new WaitForSeconds(1f);
        ExecuteEndAnimations();
        }



        public void ExecuteEndAnimations()
    {
        switch(LM.currentlevel)
        {
            case 1:
                LM.ExecuetLevel1(EndResult);
                break;
            case 2:
                LM.ExecuteLevel2(EndResult);
                break;
            case 3:
                LM.ExecuteLevel3(EndResult);
                break;
            case 4:
                LM.ExecuetLevel1(EndResult);
                break;
        }
    }

}
