using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float hp = 100f;
    public Image hpBar;

    public Button damageButton;
    public Button healButton;
    public TextMeshProUGUI ammoText;
    public int ammoAvailable = 3;

    public GameObject playerChar;

    void Start()
    {
        Button dbm = damageButton.GetComponent<Button>();
        dbm.onClick.AddListener(TakeDamage);
        Button hbm = healButton.GetComponent<Button>();
        hbm.onClick.AddListener(Heal);

       
        AmmoCounter();
    }
    void FixedUpdate()
    {
        AmmoCounter();
    }
    void TakeDamage()
    {
        hp = hp - 20f;
        hpBar.fillAmount = hp / 100f;
    }

    void Heal() 
    {
        hp = hp + 15f;
        hp = Mathf.Clamp(hp, 0, 100);
        hpBar.fillAmount = hp / 100f;
    }

    public void AmmoCounter()
    {
        Player playerValues = playerChar.GetComponent<Player>();
        ammoAvailable = playerValues.fireRate;
        ammoText.text = "Ammo Available: " + ammoAvailable;
    }


}
