using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using SimpleJSON;
using System; 

public class Highscore : IComparable<Highscore>
{
    string name;
    int score;

    public Highscore(string _name, int _score) 
    {
        name = _name;
        score = _score;
    }

    public int CompareTo(Highscore other) { 
    return score.CompareTo(other.score);
    }
}

public class levelWin : MonoBehaviour
{
    public string newLevelString;
    //remember to build the scene in file -> build scene
    private Highscore highscore1 = new Highscore("one", 150);
    private Highscore highscore2 = new Highscore("two", 160);
    private Highscore highscore3 = new Highscore("three", 170);
    private Highscore highscore4 = new Highscore("four", 180);
    private Highscore highscore5 = new Highscore("fifth", 190);
    static public int totalsaves = 0;



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
        //int highscore0 = GameObject.Find("gas meter").GetComponent<gas_meter>().gas;
        JSONObject playerJson = new JSONObject();
        List<Highscore> highscores = new List<Highscore>();
        highscores.Add(new Highscore("first", GameObject.Find("gas meter").GetComponent<gas_meter>().gas));
        highscores.Add(new Highscore("second", 150));
        highscores.Add(new Highscore("third", 150));
        highscores.Add(new Highscore("fourth", 150));
        highscores.Add(new Highscore("fifth", 150));
        highscores.Add(new Highscore("sixth", 150));


        //foreach (Highscore h in )


        /*playerJson.Add("highscore0", highscore0);
        playerJson.Add("highscore1", highscore1);
        playerJson.Add("highscore2", highscore2);
        playerJson.Add("highscore3", highscore3);
        playerJson.Add("highscore4", highscore4);
        playerJson.Add("highscore5", highscore5);
        playerJson.Add("level", SceneManager.GetActiveScene().name);*/

        //POSITION
        /*JSONArray position = new JSONArray();
        position.Add(transform.position.x);
        position.Add(transform.position.y);
        position.Add(transform.position.z);
        playerJson.Add("Position", position);*/

        //SAVE JSON IN COMPUTER

        string path = Application.dataPath + "/highscore/PlayerSave" + +totalsaves + ".json";
        Debug.Log("first one" + totalsaves);
        totalsaves++;
        Debug.Log("first 2" + totalsaves);
        File.WriteAllText(path, playerJson.ToString());

    }
}
