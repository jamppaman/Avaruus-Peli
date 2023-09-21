using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float hp = 100f;
    public Image hpBar;

    public Button quitButton;
    public TextMeshProUGUI ammoText;
    public int ammoAvailable = 3;

    public GameObject playerChar;

    void Start()
    {
        Button qbn = quitButton.GetComponent<Button>();
        qbn.onClick.AddListener(EndGame);

       
        AmmoCounter();
    }
    void FixedUpdate()
    {
        AmmoCounter();
    }
    void EndGame()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }

    public void AmmoCounter()
    {
        Player playerValues = playerChar.GetComponent<Player>();
        ammoAvailable = playerValues.fireRate;
        ammoText.text = "Ammo Available: " + ammoAvailable;
    }


}
