using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHealthManager : MonoBehaviour {
    public int playerMaxHealth;
    public int playerCurrentHealth;
	// Use this for initialization
	void Start () {
        playerCurrentHealth = playerMaxHealth;

	}
	
	// Update is called once per frame
	void Update () {
		if(playerCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gameObject.SetActive(true);
            playerCurrentHealth = playerMaxHealth;
        }
        if(playerCurrentHealth > playerMaxHealth)
        {
            playerCurrentHealth = playerMaxHealth;
        }
	}

    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }
}
