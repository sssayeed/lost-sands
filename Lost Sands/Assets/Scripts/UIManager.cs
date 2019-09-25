using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Slider healthBar;
    public playerHealthManager playerHeath;

    private static bool UIExists;
	// Use this for initialization
	void Start ()
    {
        if (!UIExists)
        {
            UIExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }

        else
        {
            Destroy(gameObject);
        } 

    }
	
	// Update is called once per frame
	void Update () {
        healthBar.maxValue = playerHeath.playerMaxHealth;
        healthBar.value = playerHeath.playerCurrentHealth;

	}
}
