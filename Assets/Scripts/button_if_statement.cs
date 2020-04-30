using UnityEngine;
using TMPro;

public class button_if_statement : MonoBehaviour
{


    public GameObject building_ahead;
    public GameObject roadblock_ahead;

    public void HandleInputData(int val)
    {

        if (val == 0)
        {
            //have to add this extra one as when you select the first one nothing happens, as you have to select another option then go back top first option.
        }
        else if (val == 1)
        {
            Instantiate(building_ahead);

        }
        else if (val == 2)
        {

            Instantiate(roadblock_ahead);
        }

        gameObject.GetComponent<TMP_Dropdown>().value = 0;
    }

}
