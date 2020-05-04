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

    public string getName()
    {
        return name;
    }

    public int getScore()
    {
        return score;
    }

    public int CompareTo(Highscore other) { 
    return score.CompareTo(other.score);
    }
}

public class levelWin : MonoBehaviour
{
    public string newLevelString;
    //remember to build the scene in file -> build scene
    static public int totalsaves = 0;
    void Save()
    {
        int gas = GameObject.Find("gas meter").GetComponent<gas_meter>().gas;
        Logger.writeString("User has completed level: " + SceneManager.GetActiveScene().name + " with a score of " + gas + "/n");
        JSONObject playerJson = new JSONObject();
        List<Highscore> highscores = new List<Highscore>();
        highscores.Add(new Highscore("YOU", gas));
        highscores.Add(new Highscore("ABC", 150));
        highscores.Add(new Highscore("ABE", 160));
        highscores.Add(new Highscore("DCE", 170));
        highscores.Add(new Highscore("FGE", 180));
        highscores.Add(new Highscore("XXX", 190));

        int i = 0;
        foreach (Highscore h in highscores)
        {   
            playerJson.Add("highscoreN" + i, h.getName());
            playerJson.Add("highscoreS" + i, h.getScore());
            i++;
        }

        playerJson.Add("level", SceneManager.GetActiveScene().name);

        string path = Application.dataPath + "/highscores/PlayerSave" + +totalsaves + ".json";
        Debug.Log("first one" + totalsaves);
        totalsaves++;
        Debug.Log("first 2" + totalsaves);
        File.WriteAllText(path, playerJson.ToString());
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(newLevelString);
    }
}
