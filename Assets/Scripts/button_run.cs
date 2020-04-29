
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class button_run : MonoBehaviour
{
    public Button yourButton;
    public static bool runButtonPH = false;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(run_console);
    }

    void run_console()
    {
        runButtonPH = true;
    }
}