using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public enum PlayerStates
{
    HumanMoving,
    TurtleMoving,
    RabbitMoving,
    DeerMoving
}

public class PlayerAnimalController : MonoBehaviour
{
    //Public variables
    #region Public Variables
    [Header("Player common variables")]
    [HideInInspector]public int playerID = 0;
    public PlayerStates playerStates = PlayerStates.HumanMoving;
    public GameObject transformationPS;
    public bool canTransform = false;

    [Header("Human variables")]
    public float moveSpeed;
    public GameObject humanMesh;

    [Header("Rabbit variables")]
    public float rabbitSpeed;
    public GameObject rabbitMesh;
    public GameObject diggingPS;

    [Header("Turtle variables")]
    public float turtleSpeed;
    public GameObject turtleMesh;
    public bool isUnderground;
    
    [Header("Turtle variables")]
    public float deerSpeed;
    public GameObject deerMesh;
    public Transform carryDest;
    public bool isCarryingItem;
    public float deerTimer;

    [Header("Gems")]
    public GameObject gemOneGO;
    public Rigidbody gemOneRB;
    public GameObject gemTwoGO;
    public Rigidbody gemTwoRB;
    #endregion

    //Private variables
    #region Private Variables
    private Player character;
    private Rigidbody myRB;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Camera mainCamera;
    private Vector3 playerDirection;
    private Vector2 playerLookDirection;
    private float countdown;
    #endregion

    //This is irrelevant! Do not open
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

        //Setting the particle System off
        diggingPS.SetActive(false);
    }

    void Update()
    {
        Flip();

        if (playerStates != PlayerStates.DeerMoving && isCarryingItem == true)
        {
            isCarryingItem = false;
            gemOneGO.transform.parent = null;
            gemTwoGO.transform.parent = null;
        }

        if (playerStates == PlayerStates.TurtleMoving)
        {
            gemTwoRB.mass = 0.1f;
            gemTwoRB.drag = 0;
        }
        else
        {
            gemTwoRB.mass = 100f;
            gemTwoRB.drag = 100f;
        }

        if (playerStates == PlayerStates.RabbitMoving)
        {

        }
        else
        {
            this.gameObject.layer = 0;
            diggingPS.SetActive(false);
        }

        switch (playerStates)
        {
            //Human states
            case PlayerStates.HumanMoving:
            HumanMesh();
            HumanMovement();
            HumanRotation();
                break;

            //Rabbit states
            case PlayerStates.RabbitMoving:
            RabbitMesh();
            RabbitMovement();
            if (Input.GetKey(KeyCode.Space))
            {
                RabbitDig();
            }
            else
            {
                rabbitMesh.SetActive(true);
                diggingPS.SetActive(false);
                this.gameObject.layer = 0;
            }
                break;

            //Turtle states
            case PlayerStates.TurtleMoving:
            TurtleMesh();
            TurtleMovement();
            TurtlePush();
                break;

            //Deer states
            case PlayerStates.DeerMoving:
            DeerMesh();
            DeerMovement();
            if (!isCarryingItem)
            {
                DeerPickup();
                DeerPickup2();
            }
            break;
        }
    }

    void FixedUpdate()
    {
        //applies the velocity to the rigidbody
        myRB.velocity = moveVelocity;
    }

    #region Human Code
    /*
    *
    * ALL CODE RELATED TO HUMAN GOES HERE
    *
    */
    void HumanMesh()
    {
        //Setting human mesh active
        humanMesh.SetActive(true);
        rabbitMesh.SetActive(false);
        turtleMesh.SetActive(false);
        deerMesh.SetActive(false);
    }

    void HumanMovement()
    {
        //Setting the vector3 equal to the inputs
        moveInput = new Vector3(character.GetAxisRaw("MoveHorizontal"), 0f, character.GetAxisRaw("MoveVertical"));
        //giving the player velocity and multiplies it by human speed
        moveVelocity = moveInput * moveSpeed;
    }

    void HumanRotation()
    {
        //Human rotation code
    }

    /*
    *
    * ALL HUMAN CODE ENDS HERE
    *
    */
    #endregion

    #region Rabbit Code
    /*
    *
    * ALL CODE RELATED TO RABBIT GOES HERE
    *
    */
    void RabbitMesh()
    {
        //Setting rabbit mesh active
        rabbitMesh.SetActive(true);
        humanMesh.SetActive(false);
        turtleMesh.SetActive(false);
        deerMesh.SetActive(false);
    }

    void RabbitMovement()
    {
        //Setting the vector3 equal to the inputs
        moveInput = new Vector3(character.GetAxisRaw("MoveHorizontal"), 0f, character.GetAxisRaw("MoveVertical"));
        //giving the player velocity and multiplies it by rabbit speed
        moveVelocity = moveInput * rabbitSpeed;
    }

    void RabbitDig()
    {
        //Turning off the rabbit mesh
        rabbitMesh.SetActive(false);
        //Setting the digging PS active
        diggingPS.SetActive(true);
        //Changing the players layer so that they can move through certain objects
        this.gameObject.layer = 12;
        //Changing a bool depending on whether they are digging or not
        isUnderground = true;
    }

    /*
    *
    * ALL RABBIT CODE ENDS HERE
    *
    */
    #endregion

    #region Turtle Code
    /*
    *
    * ALL CODE RELATED TO TURTLE GOES HERE
    *
    */
    void TurtleMesh()
    {
        //Setting turtle mesh active
        turtleMesh.SetActive(true);
        humanMesh.SetActive(false);
        rabbitMesh.SetActive(false);
        deerMesh.SetActive(false);
    }

    void TurtleMovement()
    {
        //Setting the vector3 equal to the inputs
        moveInput = new Vector3(character.GetAxisRaw("MoveHorizontal"), 0f, character.GetAxisRaw("MoveVertical"));
        //giving the player velocity and multiplies it by turtle speed
        moveVelocity = moveInput * turtleSpeed;
    }

    void TurtlePush()
    {

    }

    /*
    *
    * ALL TURTLE CODE ENDS HERE
    *
    */
    #endregion

    #region Deer Code
    /*
    *
    * ALL CODE RELATED TO DEER GOES HERE
    *
    */
    void DeerMesh()
    {
        //Setting turtle mesh active
        deerMesh.SetActive(true);
        turtleMesh.SetActive(false);
        humanMesh.SetActive(false);
        rabbitMesh.SetActive(false);
    }

    void DeerMovement()
    {
        //Setting the vector3 equal to the inputs
        moveInput = new Vector3(character.GetAxisRaw("MoveHorizontal"), 0f, character.GetAxisRaw("MoveVertical"));
        //giving the player velocity and multiplies it by turtle speed
        moveVelocity = moveInput * deerSpeed;
    }

    void DeerPickup()
    {
        gemOneGO.GetComponent<Rigidbody>().useGravity = false;
        gemOneGO.transform.position = carryDest.position;
        gemOneGO.transform.SetParent(carryDest.transform);
        isCarryingItem = true;
    }

    void DeerPickup2()
    {
        gemTwoGO.GetComponent<Rigidbody>().useGravity = false;
        gemTwoGO.transform.position = carryDest.position;
        gemTwoGO.transform.SetParent(carryDest.transform);
        isCarryingItem = true;
    }

    void DeerTimer()
    {
        deerTimer -= Time.deltaTime;
        if (deerTimer <= 0)
        {
            playerStates = PlayerStates.HumanMoving;
            deerTimer = 30;
            gemOneGO.transform.parent = null;
            gemOneGO.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    /*
    *
    * ALL DEER CODE ENDS HERE
    *
    */
    #endregion

    private void OnTriggerEnter(Collider theCol)
    {

        //If the player is close to a pickup then they transform into a deer
        if (theCol.gameObject.CompareTag("DeerArea"))
        {
            if (playerStates != PlayerStates.DeerMoving)
            {
                playerStates = PlayerStates.DeerMoving;
                Instantiate(transformationPS, transform.position, Quaternion.identity);
            }
        }

        if (theCol.gameObject.CompareTag("PickUp1") && !isCarryingItem)
        {
            DeerPickup();
        }

        if (theCol.gameObject.CompareTag("PickUp2") && !isCarryingItem)
        {
            DeerPickup2();
        }
    }

    private void OnTriggerStay(Collider theCol)
    {
        //If the player is close to the bear cave then they trasnform into a rabbit
        if (theCol.gameObject.CompareTag("BearCave"))
        {
            if (playerStates != PlayerStates.RabbitMoving)
            {
                playerStates = PlayerStates.RabbitMoving;
                Instantiate(transformationPS, transform.position, Quaternion.identity);
            }
        }

        //If the player is close to water then they transform into a turtle
        if (theCol.gameObject.CompareTag("Water"))
        {
            if (playerStates != PlayerStates.TurtleMoving)
            {
                playerStates = PlayerStates.TurtleMoving;
                Instantiate(transformationPS, transform.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerExit(Collider theCol)
    {
        //If the player leaves the bear cave
        if (theCol.gameObject.CompareTag("BearCave"))
        {
            if (playerStates != PlayerStates.HumanMoving)
            {
                playerStates = PlayerStates.HumanMoving;
                Instantiate(transformationPS, transform.position, Quaternion.identity);
            }
        }

        //If the player leaves the water
        if (theCol.gameObject.CompareTag("Water"))
        {
            if (playerStates != PlayerStates.DeerMoving)
            {
                playerStates = PlayerStates.DeerMoving;
                Instantiate(transformationPS, transform.position, Quaternion.identity);
            }
        }
    }

    void Flip()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
