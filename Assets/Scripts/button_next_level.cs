
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
        newLevelString = GameObject.Find("level_x").GetComponent<TextMeshProUGUI>().text;
        string level = newLevelString.Substring(6,1);
        int test = int.Parse(level) + 1;
        Debug.Log(test);
        Debug.Log("Game is going to next level");
        SceneManager.LoadScene("level " + test);
    }
}