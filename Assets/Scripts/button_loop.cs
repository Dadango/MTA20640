using UnityEngine;
using TMPro;

public class button_loop : MonoBehaviour
{ 


    public GameObject for_loop;

    public void HandleInputData(int val)
    {
       
        if (val == 0)
        {
            //have to add this extra one as when you select the first one nothing happens, as you have to select another option then go back top first option.
        }
        else if (val == 1)
        {
            Instantiate(for_loop);
            
        }

        gameObject.GetComponent<TMP_Dropdown>().value = 0; //resets the spiderman
    }

}
