using UnityEngine;
using TMPro;

public class gas_meter : MonoBehaviour
{
    public TextMeshProUGUI gas_text;
    private int gas_total = 300;
    public int gas = 300;

    public void gasChecker(int id)
    {
        switch (id)
        {
            case 0: //method raw
                gas -= 20;
                gas_text.text = "Gas" + "\n" + gas + "/" + gas_total;
                break;
            case 1: //method inside a loop
                gas -= 10;
                gas_text.text = "Gas" + "\n" + gas + "/" + gas_total;
                break;
            case 2: //method inside an if statement
                gas -= 15;
                gas_text.text = "Gas" + "\n" + gas + "/" + gas_total;
                break;
            case 3: //method inside an if statement inside a loop
                gas -= 5;
                gas_text.text = "Gas" + "\n" + gas + "/" + gas_total;
                break;
        }
    }

    public void gasReset() {
        gas = gas_total;
        gas_text.text = "Gas" + "\n" + gas + "/" + gas_total;
    }

}
