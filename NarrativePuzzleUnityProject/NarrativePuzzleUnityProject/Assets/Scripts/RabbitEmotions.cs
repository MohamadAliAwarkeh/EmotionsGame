using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitEmotions : MonoBehaviour
{
    
    public GameObject diggingPS;
    public bool isUnderground;

    private PlayerMovementController controller;

    void Start()
    {
        //Turning the digging PS off
        diggingPS.SetActive(false);
        //Getting the controller
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
    }

    void Update()
    {
        //Allowing the player to dig through the stuff
        if (controller.movementStates == MovementStates.RabbitMoving)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Digging();
            }
            else
            {
                this.gameObject.layer = 0;
                isUnderground = false;
                diggingPS.SetActive(false);
            }
        }
        else
        {
            if (diggingPS.active)
            {
                this.gameObject.layer = 0;
                isUnderground = false;
                diggingPS.SetActive(false);
            }
        }
    }

    void Digging()
    {
        //Turning off the rabbit mesh
        controller.rabbitMesh.SetActive(false);
        //Setting the digging PS active
        diggingPS.SetActive(true);
        //Changing the players layer so that they can move through certain objects
        this.gameObject.layer = 12;
        //Changing a bool depending on whether they are digging or not
        isUnderground = true;
    }

    private void OnTriggerStay(Collider theCol)
    {
        //Becoming a rabbit
        if (theCol.gameObject.CompareTag("BearCave"))
        {
            if (controller.movementStates != MovementStates.RabbitMoving)
            {
                controller.movementStates = MovementStates.RabbitMoving;
                Instantiate(controller.transformationPS, controller.rabbitMesh.transform.position, Quaternion.identity);
                controller.isTransforming = true;
                StartCoroutine(controller.TransformationDelay());
            }
        }
    }

    private void OnTriggerExit(Collider theCol)
    {
        //Becoming a human
        if (theCol.gameObject.CompareTag("BearCave"))
        {
            if (controller.movementStates != MovementStates.HumanMoving)
            {
                controller.movementStates = MovementStates.HumanMoving;
                Instantiate(controller.transformationPS, controller.humanMesh.transform.position, Quaternion.identity);
                controller.isTransforming = true;
                StartCoroutine(controller.TransformationDelay());
            }
        }
    }
}
