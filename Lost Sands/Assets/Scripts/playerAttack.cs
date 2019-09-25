using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {

    public bool keyPressed;
    private float attackTime;
    private playerController theAttack;

	// Use this for initialization
	void Start () {
        theAttack.GetComponent<playerController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKeyDown("x") || theAttack.buttonB == true)
        {
            attackTime = 0.5f;
            keyPressed = true;
        }
        Debug.Log(theAttack.buttonB);
     
            attackTime -= Time.deltaTime;
            if (attackTime < 0f)
            {
                keyPressed = false;
            }


    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && keyPressed) {
            Destroy(collision.gameObject);
        }
    }
}
