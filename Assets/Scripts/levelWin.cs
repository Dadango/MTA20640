using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using SimpleJSON;

public class levelWin : MonoBehaviour
{
    public string newLevelString;
    //remember to build the scene in file -> build scene
    private int highscore0;
    private int highscore1 = 3;
    private int highscore2 = 45;
    private int highscore3 = 6;
    private int highscore4 = 78;
    private int highscore5 = 9;


    
    private void OnTriggerEnter2D(Collider2D levelEnd)
    {
        //Application.LoadLevel("level 2")
        if (levelEnd.gameObject.CompareTag("Player")) 
        {
            Save();
            SceneManager.LoadScene(newLevelString);
            Debug.Log("igothit");
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void Save()
    {
        int highscore0 = GameObject.Find("gas meter").GetComponent<gas_meter>().gas;
        Debug.Log("this is a derp" + highscore0);
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
}
