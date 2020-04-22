using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gas_meter : MonoBehaviour
{
    public TextMeshProUGUI gas_text;
    private int gas_total = 300;
    private int gas = 300;
    //Might want to add public gameObject just to make sure everything interacts with the same object, not sure if it is needed or not 
    //public GameObject gas_variable;


    // used for testing the gas checker function
    /*void Start()
    {
        
        for (int i = 0; i < 1; i++)
        {

            gasChecker(2);

        }
    }*/

        //Could also change id to string so it correlates to loop, method or whatever 
    void gasChecker(int id)
    {
        //gas = gas_variable.GetComponent<gas>().gass;
        if (id == 0) //id refers to if it is a loop, method or if-statement
        {
            gas -= 20;
            gas_text.text = "Gas" + "\n" + gas + "/" + gas_total;
        }
        else if(id == 1)
        {
            gas -= 10;
            gas_text.text = "Gas" + "\n" + gas + "/" + gas_total;
        }
        else if(id == 2)
        {
            gas -= 15;
            gas_text.text = "Gas" + "\n" + gas + "/" + gas_total;
        }
        
    }
}
