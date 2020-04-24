using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelWin : MonoBehaviour
{
    public string newLevelString;
    //remember to build the scene in file -> build scene
    private void OnTriggerEnter2D(Collider2D levelEnd)
    {
        //Application.LoadLevel("level 2")
        if (levelEnd.gameObject.CompareTag("Player")) 
        {
            SceneManager.LoadScene(newLevelString);
            Debug.Log("igothit");
        }
    }
}
