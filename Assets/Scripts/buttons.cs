using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class buttons : MonoBehaviour
{ 

    public GameObject button;
    private TMP_Dropdown derp;

    public void HandleInputData(int val)
    {
        derp = button.GetComponent<TMP_Dropdown>();
        
        if (val == 0)
        {
            //have to add this extra one as when you select the first one nothing happens, as you have to select another option then go back top first option.
        }
        else if (val == 1)
        {
            if (button.name == "Loop_button") {
                forLoopFunction();
            }

            else if (button.name == "method_button")
            {
                driveForward();
            }

        }

        else if (val == 2)
        {
            if (button.name == "Loop_button")
            {
                whileFunction();
            }

            else if (button.name == "method_button")
            {
                driveLeft();
            }
        }

        else if (val == 3)
        {
            if (button.name == "Loop_button")
            {
                doWhileFunction();
            }

            else if (button.name == "method_button")
            {
                driveRight();
            }

        }

        else if (val == 4)
        {
            if (button.name == "Loop_button")
            {
                forEachFunction();
            }

            else if (button.name == "method_button")
            {
                Make_u_turn();
            }
        }
        derp.value = 0;
    }


    void forLoopFunction()
    {
        GameObject test;
        Debug.Log("for-loop");
    }
    void whileFunction()
    {
        Debug.Log("while-loop");
    }
    void doWhileFunction()
    {
        Debug.Log("do-While-loop");
    }

    void forEachFunction()
    {
        Debug.Log("for-each-loop");
    }
    void driveForward()
    {
        Debug.Log("Drive forward");
    }
    void driveLeft()
    {
        Debug.Log("Drive left");
    }
    void driveRight()
    {
        Debug.Log("drive right");
    }
    void Make_u_turn()
    {
        Debug.Log("Make_u_turn uturn");
    }
}
