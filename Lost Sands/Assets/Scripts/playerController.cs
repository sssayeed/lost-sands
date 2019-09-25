using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class playerController : MonoBehaviour {

    public float moveSpeed;
    private Animator anim;
    private bool playerMoving;
    public Vector2 lastMove;
    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;

    int dirX = 0;
    int dirY = 0;
    SerialPort stream = new SerialPort("COM3", 9600);
    string dataString = null;
    public static bool playerExists = false;
    public bool buttonB = false;
   



    public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
    {
        DateTime initialTime = DateTime.Now;
        DateTime nowTime;
        TimeSpan diff = default(TimeSpan);

        dataString = null;

        do
        {
            try
            {
                dataString = stream.ReadLine();
            }

            catch (TimeoutException)
            {
                dataString = null;
            }

            if (dataString != null)
            {
                callback(dataString);
                yield break; // Terminates the Coroutine
            }
            else
                yield return null; // Wait for next frame

            nowTime = DateTime.Now;
            diff = nowTime - initialTime;

        } while (diff.Milliseconds < timeout);

        if (fail != null)
            fail();
        yield return null;
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
        stream.ReadTimeout = 50;
        stream.Open();

        
    }
	
	// Update is called once per frame
	void FixedUpdate () {




        //WriteToArduino("PING");

        StartCoroutine(AsynchronousReadFromArduino
      ((string s) => Debug.Log(s), () => Debug.LogError("Error!"), 10000f)
      );

        //Debug.Log(dataString);
        int dataX = 538;
        int dataY = 512;
        int buttonA = 0;
        int butB = 0;
        int start = 0;
        int LS = 0;

        playerMoving = false;
        if(dataString != null)
        {
            string[] coordinate = dataString.Split('.');
            dataX = int.Parse(coordinate[0]);
            dataY = int.Parse(coordinate[1]);
            buttonA = int.Parse(coordinate[2]);
            butB = int.Parse(coordinate[3]);
            start = int.Parse(coordinate[4]);
            LS = int.Parse(coordinate[5]);
        }
        if(butB != 0)
        {
            buttonB = true;
        }

        else
        {
            buttonB = false;
        }
        //Debug.Log(dataX);
        //Debug.Log(dataY);
        if (buttonA == 1)
        {
            moveSpeed = 6;
        }

        else
        {
            moveSpeed = 4;
        }

        if ( dataX > 700 || dataX < 300)
        {
            dirX = dataX > 512 ? 1 : -1;
            transform.Translate(new Vector3(dirX * moveSpeed * Time.deltaTime, 0f, 0f));
            playerMoving = true;
            lastMove = new Vector2(dataX, 0);
        }

        if (dataY > 700 || dataY < 300)
        {
            dirY = dataY > 512 ? -1 : 1;
            transform.Translate(new Vector3(0f, dirY* moveSpeed * Time.deltaTime, 0f));
            playerMoving = true;
            lastMove = new Vector2(0, dirY);
        }
        //also takes keyboard inputs
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            playerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            playerMoving = true;
            lastMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
        }
        //Debug.Log(Input.GetAxisRaw("Vertical"));
        if(dirX != 0 || dirY != 0)
        {
            anim.SetFloat("moveX", dirX);
            anim.SetFloat("moveY", dirY);
        }
        else
        {
            anim.SetFloat("moveX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("moveY", Input.GetAxisRaw("Vertical"));
        }


        if(Input.GetKeyDown("x") || buttonB == true){
            attackTimeCounter = attackTime;
            attacking = true;
            anim.SetBool("attacking", true);

        }

        if (attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        
        }

        else if (attackTimeCounter <= 0)
        {
            attacking = false;
            anim.SetBool("attacking", false);

        }

        anim.SetBool("playerMoving", playerMoving);
        anim.SetFloat("lastMoveX", lastMove.x);
        anim.SetFloat("lastMoveY", lastMove.y);

    }

    public void WriteToArduino(string message)
    {
        stream.WriteLine(message);
        stream.BaseStream.Flush();
    }

    

}
