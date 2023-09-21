using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        Button starter = startButton.GetComponent<Button>();
        starter.onClick.AddListener(StartPressed);
    }

    void StartPressed() 
    {
        SceneManager.LoadScene(sceneName: "SampleScene");
    }
}
