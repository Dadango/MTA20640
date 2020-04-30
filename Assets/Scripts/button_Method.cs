using UnityEngine;
using TMPro;

public class button_Method : MonoBehaviour
{

    public GameObject drive_up;
    public GameObject drive_left;
    public GameObject drive_right;
    public GameObject drive_down;

    public void HandleInputData(int val)
    {

        if (val == 0)
        {
            //have to add this extra one as when you select the first one nothing happens, as you have to select another option then go back top first option.
        }
        else if (val == 1)
        {
            Instantiate(drive_up);

        }
        else if (val == 2)
        {

            Instantiate(drive_left);
        }

        else if (val == 3)
        {
            Instantiate(drive_right);

        }

        else if (val == 4)
        {
            Instantiate(drive_down);

        }

        gameObject.GetComponent<TMP_Dropdown>().value = 0; //resets the spiderman
    }

}
