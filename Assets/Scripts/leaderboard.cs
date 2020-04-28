using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class leaderboard : MonoBehaviour
{
    public TextMeshProUGUI firstPlace;
    public TextMeshProUGUI secondPlace;
    public TextMeshProUGUI thirdPlace;
    public TextMeshProUGUI fourthPlace;
    public TextMeshProUGUI fifthPlace;
    public TextMeshProUGUI sixthPlace;
    // Start is called before the first frame update
    void Start()
    {
        firstPlace.text = "4";
        secondPlace.text = "5";
        thirdPlace.text = "3. derp";
        fourthPlace.text = "4. derp";
        fifthPlace.text = "5. derp";
        sixthPlace.text = "6. derp";


        int derp = int.Parse(firstPlace.text);
        int derp2 = int.Parse(secondPlace.text);

        if(derp > derp2)
        {
            firstPlace.text = derp.ToString();
            secondPlace.text = derp2.ToString();
        }
        else
        {
            firstPlace.text = derp2.ToString();
            secondPlace.text = derp.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
