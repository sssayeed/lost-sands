using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStartPoint : MonoBehaviour {

    private playerController thePlayer;
    private cameraController theCamera;

    public Vector2 startDirection;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<playerController>();
        thePlayer.transform.position = transform.position;
        thePlayer.lastMove = startDirection;
        theCamera = FindObjectOfType<cameraController>();
        theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
