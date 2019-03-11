using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public enum MovementStates
{
    HumanMoving,
    TurtleMoving,
    RabbitMoving,
    DeerMoving
}

public class PlayerMovementController : MonoBehaviour
{
    //Public variables
    #region Public Variables
    [Header("Enum")]
    [HideInInspector] public int playerID = 0;
    public MovementStates movementStates = MovementStates.HumanMoving;
    public bool isTransforming;

    [Header("Human variables")]
    public float moveSpeed;
    public GameObject humanMesh;
    public Animator humanAnim;

    [Header("Rabbit variables")]
    public float rabbitSpeed;
    public GameObject rabbitMesh;
    public Animator rabbitAnim;

    [Header("Turtle variables")]
    public float turtleSpeed;
    public GameObject turtleMesh;
    public Animator turtleAnim;

    [Header("Deer variables")]
    public float deerSpeed;
    public GameObject deerMesh;
    public Animator deerAnim;

    [Header("VFX")]
    public GameObject transformationPS;
    public float transformationDelayTimer;
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
    }

    void Update()
    {
        Rotations();

        switch (movementStates)
        {
            //Human states
            case MovementStates.HumanMoving:
                HumanMesh();
                HumanMovement();
                break;

            //Rabbit states
            case MovementStates.RabbitMoving:
                RabbitMesh();
                RabbitMovement();
                break;

            //Turtle states
            case MovementStates.TurtleMoving:
                TurtleMesh();
                TurtleMovement();
                break;

            //Deer states
            case MovementStates.DeerMoving:
                DeerMesh();
                DeerMovement();
                break;
        }
    }

    void FixedUpdate()
    {
        //applies the velocity to the rigidbody
        if (isTransforming)
        {
            moveVelocity = Vector3.zero;
            myRB.velocity = Vector3.zero;
        }
        else
        {
            myRB.velocity = moveVelocity;
        }
    }

    public IEnumerator TransformationDelay()
    {

        yield return new WaitForSeconds(transformationDelayTimer);
        isTransforming = false;
        
    }

    void Rotations()
    {
        //If the player is playing with any controller then execute this code
            //The normal direction is where the player is set to being able to move around and rotate
            //whilst shooting
            playerDirection = Vector3.right * character.GetAxisRaw("MoveHorizontal") + Vector3.forward * character.GetAxisRaw("MoveVertical");
            //Checking if the vector3 has got a value inputed
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                Vector3 tempRotationValue = transform.rotation.eulerAngles;
                tempRotationValue.y = tempRotationValue.y;
                transform.rotation = Quaternion.Euler(tempRotationValue);
                playerLookDirection.x = playerDirection.x;
                playerLookDirection.y = playerDirection.z;
            }
            else
            {
                //The alt direction is where the player is set to being able to move around and rotate
                //whilst not having to shoot
                Vector3 playerDirectionAlt = Vector3.right * character.GetAxisRaw("MoveHorizontal") + Vector3.forward * character.GetAxisRaw("MoveVertical");
                if (playerDirectionAlt.sqrMagnitude > 0.0f)
                {
                    transform.rotation = Quaternion.LookRotation(playerDirectionAlt, Vector3.up);
                    Vector3 tempRotationValue = transform.rotation.eulerAngles;
                    tempRotationValue.y = tempRotationValue.y;
                    transform.rotation = Quaternion.Euler(tempRotationValue);
                    playerLookDirection.x = playerDirectionAlt.x;
                    playerLookDirection.y = playerDirectionAlt.z;
                }
            }
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
        moveVelocity = moveInput.normalized * moveSpeed;

        if (Input.GetKey(KeyCode.A))
        {
            humanAnim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            humanAnim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            humanAnim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            humanAnim.SetBool("isWalking", true);
        }
        else
        {
            humanAnim.SetBool("isWalking", false);
        }
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
        moveVelocity = moveInput.normalized * rabbitSpeed;

        if (Input.GetKey(KeyCode.A))
        {
            rabbitAnim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rabbitAnim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rabbitAnim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rabbitAnim.SetBool("isWalking", true);
        }
        else
        {
            rabbitAnim.SetBool("isWalking", false);
        }
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
        moveVelocity = moveInput.normalized * turtleSpeed;

        if (Input.GetKey(KeyCode.A))
        {
            turtleAnim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            turtleAnim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turtleAnim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            turtleAnim.SetBool("isWalking", true);
        }
        else
        {
            turtleAnim.SetBool("isWalking", false);
        }
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
        moveVelocity = moveInput.normalized * deerSpeed;

        if (Input.GetKey(KeyCode.A))
        {
            deerAnim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            deerAnim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            deerAnim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            deerAnim.SetBool("isWalking", true);
        }
        else
        {
            deerAnim.SetBool("isWalking", false);
        }
    }
    /*
    *
    * ALL DEER CODE ENDS HERE
    *
    */
    #endregion
}
