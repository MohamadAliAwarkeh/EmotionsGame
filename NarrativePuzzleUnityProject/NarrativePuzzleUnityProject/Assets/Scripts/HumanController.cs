using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class HumanController : MonoBehaviour
{

    public float moveSpeed;

    //Private variables
    private int playerID = 0;
    private Player character;
    private Rigidbody myRB;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Camera mainCamera;
    private Vector3 playerDirection;
    private Vector2 playerLookDirection;

    void Awake()
    {
        //Getting the Rewired player object for this gameObject and keeping it for the characters lifetime
        character = ReInput.players.GetPlayer(playerID);
    }

    void Start()
    {
        //Simply setting things at the start
        myRB = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();

        //Player directions for rotation and movement
        playerLookDirection.x = 0f;
        playerLookDirection.y = 1f;
    }

    void Update()
    {
        //HumanMovement();
        //HumanRotation();
    }

    public void FixedUpdate()
    {
        //applies the velocity to the rigidbody
        myRB.velocity = moveVelocity;
    }

    /*
    *
    * ALL CODE RELATED TO HUMAN GOES HERE
    *
    */
    public void HumanMovement()
    {
        //Setting the Human material colour
        this.GetComponent<MeshRenderer>().material.color = Color.black;
        //Setting the vector3 equal to the inputs
        moveInput = new Vector3(character.GetAxisRaw("MoveHorizontal"), 0f, character.GetAxisRaw("MoveVertical"));
        //giving the player velocity and multiplies it by human speed
        moveVelocity = moveInput * moveSpeed;
        Debug.Log("Zoinks");
    }

    public void HumanRotation()
    {
        //Human rotation code
    }

    /*
    *
    * ALL HUMAN CODE ENDS HERE
    *
    */
}
