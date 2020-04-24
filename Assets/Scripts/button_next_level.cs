
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class button_next_level : MonoBehaviour
{
    public Button yourButton;
    public string newLevelString;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(nextLevel);
    }

    void nextLevel()
    {

        Debug.Log("Game is exiting");
        SceneManager.LoadScene(newLevelString);
    }
}