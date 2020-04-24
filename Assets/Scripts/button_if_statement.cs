using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class button_if_statement : MonoBehaviour
{

    public GameObject button;
    public Canvas parent;
    private TMP_Dropdown derp;

    public GameObject building_ahead;
    public GameObject roadblock_ahead;
    public GameObject end_of_road;
    public GameObject nothing;
    public void HandleInputData(int val)
    {
        derp = button.GetComponent<TMP_Dropdown>();

        if (val == 0)
        {
            //have to add this extra one as when you select the first one nothing happens, as you have to select another option then go back top first option.
        }
        else if (val == 1)
        {
            driveForward();
            Instantiate(building_ahead);

        }
        else if (val == 2)
        {

            driveLeft();
            Instantiate(roadblock_ahead);
        }

        else if (val == 3)
        {
            driveRight();
            Instantiate(end_of_road);

        }

        else if (val == 4)
        {
            Make_u_turn();
            Instantiate(nothing);

        }
        derp.value = 0;
    }

    //This will probably not be used but ready if it will be =)

    void driveForward()
    {
        //Debug.Log("Drive forward");
    }
    void driveLeft()
    {
        //Debug.Log("Drive left");
    }
    void driveRight()
    {
        //Debug.Log("drive right");
    }
    void Make_u_turn()
    {
        //Debug.Log("Make_u_turn uturn");
    }

}
