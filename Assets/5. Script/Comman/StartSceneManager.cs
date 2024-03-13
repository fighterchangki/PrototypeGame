using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartSceneManager : MonoBehaviour
{
    public Button startButton;
    public Button ExitButton;
    // Start is called before the first frame update
    void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(StartButtonClick);
        if (ExitButton != null)
            ExitButton.onClick.AddListener(ExitButtonClick);
    }
    public void StartButtonClick()
    {
        SceneChangeManager.Instance.SceneChange("GameScene");
    }
    public void ExitButtonClick()
    {
        Application.Quit();
    }
    // Update is called once per frame
}
