using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using TMPro;
using System;

public class loader : MonoBehaviour
{
    public TextMeshProUGUI firstPlace;
    public TextMeshProUGUI secondPlace;
    public TextMeshProUGUI thirdPlace;
    public TextMeshProUGUI fourthPlace;
    public TextMeshProUGUI fifthPlace;
    public TextMeshProUGUI sixthPlace;
    public int[] highscore = new int[6];
    static private int totalloader = 0;

    //public string Name;
    /*public int highscore0;
    public float highscore1;
    public int highscore2;
    public int highscore3;
    public int highscore4;
    public int highscore5;*/

    void leaderboard(int[] highscore)
    {
        Array.Sort(highscore);
        Array.Reverse(highscore);
        firstPlace.text = "1. " + highscore[0].ToString();
        secondPlace.text = "2. " + highscore[1].ToString();
        thirdPlace.text = "3. " + highscore[2].ToString();
        fourthPlace.text = "4. " + highscore[3].ToString();
        fifthPlace.text = "5. " + highscore[4].ToString();
        sixthPlace.text = "6. " + highscore[5].ToString();
          
    }

    int[] Load()
    {
        
        Debug.Log("I AM LOADING");
        string path = Application.dataPath + "/highscore/PlayerSave" + totalloader + ".json";
        string jsonString = File.ReadAllText(path);
        JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
        //SET VALUES
        //Name = playerJson["Name"];
        int[] highscore = new int[6];
        highscore[0] = playerJson["highscore0"];
        highscore[1] = playerJson["highscore1"];
        highscore[2] = playerJson["highscore2"];
        highscore[3] = playerJson["highscore3"];
        highscore[4] = playerJson["highscore4"];
        highscore[5] = playerJson["highscore5"];
        GameObject.Find("level_x").GetComponent<TextMeshProUGUI>().text = playerJson["level"];
        //int[] highscore = {highscore0, highscore1, highscore2, highscore3, highscore4, highscore5};
        //POSITION
        /*transform.position = new Vector3(
            playerJson["Position"].AsArray[0],
            playerJson["Position"].AsArray[1],
            playerJson["Position"].AsArray[2]
        );*/
        totalloader++;
        return highscore;
    }
    void Start()
    {
        //if (Input.GetKeyDown("right"))
        //{
            highscore = Load();
            leaderboard(highscore);
        //}
    }
}
