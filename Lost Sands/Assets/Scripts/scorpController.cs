using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scorpController : MonoBehaviour {

    public float moveSpeed;
    public Rigidbody2D myRigidBody;
    private bool moving;
    public float timeBetweenMovement;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;
    private Vector3 moveDirection;

    public float waitToReload;
    private bool reloading;
    private GameObject wanderer;

	// Use this for initialization
	void Start () {
        myRigidBody.GetComponent<Rigidbody2D>();

        //timeBetweenMoveCounter = timeBetweenMovement;
        //timeToMoveCounter = timeToMove;

        timeBetweenMoveCounter = Random.Range(0.75f * timeBetweenMovement, 1.25f * timeBetweenMovement);
        timeToMoveCounter = Random.Range(0.75f * timeToMove, 1.25f * timeToMove);


    }
	
	// Update is called once per frame
	void Update () {

        if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;

            myRigidBody.velocity = moveDirection;
            if (timeToMoveCounter < 0f)
            {
                moving = false;
                timeBetweenMoveCounter = Random.Range(0.75f * timeBetweenMovement, 1.25f * timeBetweenMovement);
                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
            }
        }

        else
        {
            myRigidBody.velocity = Vector2.zero;
            timeBetweenMoveCounter -= Time.deltaTime;
            if (timeBetweenMoveCounter < 0f)
            {
                moving = true;

                timeToMoveCounter = Random.Range(0.75f * timeToMove, 1.25f * timeToMove);

                moveDirection = new Vector3(Random.Range(-1f, 1f)*moveSpeed, Random.Range(-1f, 1f)*moveSpeed, 0f);
            }
        }
		

        if (reloading)
        {
            waitToReload -= Time.deltaTime;
            if(waitToReload < 0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                wanderer.SetActive(true);
                reloading = false;
            }
        }

	}


    void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SetActive(false);
            reloading = true;
            wanderer = collision.gameObject;

        }*/
    }
}
