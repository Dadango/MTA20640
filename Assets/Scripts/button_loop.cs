using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class button_loop : MonoBehaviour
{ 

    public GameObject button;
    public Canvas parent;
    private TMP_Dropdown derp;

    public GameObject for_loop;
    public GameObject while_loop;
    public GameObject do_while_loop;
    public GameObject for_each_loop;
    public void HandleInputData(int val)
    {
        derp = button.GetComponent<TMP_Dropdown>();
        
        if (val == 0)
        {
            //have to add this extra one as when you select the first one nothing happens, as you have to select another option then go back top first option.
        }
        else if (val == 1)
        {
            forLoopFunction();
            Instantiate(for_loop);
            // Old code for creating text buttons
            /*GameObject For_loop_text = new GameObject("myTextGO"); 
            For_loop_text.transform.SetParent(parent.transform);
            For_loop_text.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            For_loop_text.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            TextMeshProUGUI myText = For_loop_text.AddComponent<TextMeshProUGUI>();
            Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            myText.material = ArialFont.material;
            myText.fontSize = 20;
            myText.text = "for loop";

            DragNDrop drag = For_loop_text.AddComponent<DragNDrop>();
            drag.speed = 10; */
        }

        else if (val == 2)
        {
            whileFunction();
            Instantiate(while_loop);
        }

        else if (val == 3)
        {
            doWhileFunction();
            Instantiate(do_while_loop);
        }

        else if (val == 4)
        {
            forEachFunction();
            Instantiate(for_each_loop);
        }
        derp.value = 0; //resets the spiderman
    }

    //Probably won't use this but it is there just in case
    void forLoopFunction()
    {
        //Debug.Log("for-loop");
    }
    void whileFunction()
    {
        //Debug.Log("while-loop");
    }
    void doWhileFunction()
    {
       // Debug.Log("do-While-loop");
    }

    void forEachFunction()
    {
        //Debug.Log("for-each-loop");
    }


}
