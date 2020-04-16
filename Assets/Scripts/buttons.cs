using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class buttons : MonoBehaviour
{ 

    public TextMeshProUGUI output;

    public void HandleInputData(int val)
    {
        if(val == 0)
        {

        }
        else if(val == 1)
        {
            forLoopFunction();
        }

        else if (val == 2)
        {
            whileFunction();
        }

        else if (val == 3)
        {
            doWhileFunction();
        }

        else if (val == 4)
        {
            forEachFunction();
        }
    }

    void forLoopFunction()
    {
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
}
