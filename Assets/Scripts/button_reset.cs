
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class button_reset : MonoBehaviour
{
    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(Reset);
    }

    void Reset()
    {
        Logger.writeString("User reset the level: " + SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().path, LoadSceneMode.Single);
    }
}