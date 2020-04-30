using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class save : MonoBehaviour
{

    //public string Name;
    private int highscore0 = 12;
    private int highscore1 = 3;
    private int highscore2 = 45;
    private int highscore3 = 6;
    private int highscore4 = 78;
    private int highscore5 = 9;


    void Save()
    {
        Debug.Log("I AM RUNNING");
        JSONObject playerJson = new JSONObject();
        //playerJson.Add("Name", Name);
        playerJson.Add("highscore0", highscore0);
        playerJson.Add("highscore1", highscore1);
        playerJson.Add("highscore2", highscore2);
        playerJson.Add("highscore3", highscore3);
        playerJson.Add("highscore4", highscore4);
        playerJson.Add("highscore5", highscore5);

        //POSITION
        /*JSONArray position = new JSONArray();
        position.Add(transform.position.x);
        position.Add(transform.position.y);
        position.Add(transform.position.z);
        playerJson.Add("Position", position);*/

        //SAVE JSON IN COMPUTER
        string path = Application.dataPath + "/highscore/PlayerSave.json";
        File.WriteAllText(path, playerJson.ToString());

    }

    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left")) Save();
    }
}
