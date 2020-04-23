using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class button_Method : MonoBehaviour
{

    public GameObject button;
    public Canvas parent;
    private TMP_Dropdown derp;

    public GameObject drive_up;
    public GameObject drive_left;
    public GameObject drive_right;
    public GameObject drive_down;
    public GameObject make_u_turn;

    public void HandleInputData(int val)
    {
        derp = button.GetComponent<TMP_Dropdown>();

        if (val == 0)
        {
            //have to add this extra one as when you select the first one nothing happens, as you have to select another option then go back top first option.
        }
        else if (val == 1)
        {
            driveUp();
            Instantiate(drive_up);

        }
        else if (val == 2)
        {

            driveLeft();
            Instantiate(drive_left);
        }

        else if (val == 3)
        {
            driveRight();
            Instantiate(drive_right);

        }

        else if (val == 4)
        {
            driveDown();
            Instantiate(drive_down);

        }
        else if (val == 5)
        {
            Make_u_turn();
            Instantiate(make_u_turn);

        }
        derp.value = 0; //resets the spiderman
    }

    //This will probably not be used but ready if it will be =)

    void driveUp()
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
    void driveDown()
    {
        //Debug.Log("drive back");
    }
    void Make_u_turn()
    {
        //Debug.Log("Make_u_turn uturn");
    }

}
