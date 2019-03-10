using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEmotions : MonoBehaviour
{
    private PlayerMovementController controller;
    private DeerEmotions deerEmotions;

    void Start()
    {
        //Getting the controller
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
        deerEmotions = GameObject.FindGameObjectWithTag("Player").GetComponent<DeerEmotions>();
    }

    private void OnTriggerStay(Collider theCol)
    {
        //Becoming a rabbit
        if (theCol.gameObject.CompareTag("Water"))
        {
            if (controller.movementStates != MovementStates.TurtleMoving)
            {
                controller.movementStates = MovementStates.TurtleMoving;
                Instantiate(controller.transformationPS, controller.rabbitMesh.transform.position, Quaternion.identity);
                controller.isTransforming = true;
                StartCoroutine(controller.TransformationDelay());
            }

            deerEmotions.isCarryingItem = true;
        }

        if (theCol.gameObject.CompareTag("Pickup"))
        {
            if (controller.movementStates == MovementStates.TurtleMoving)
            {
                theCol.gameObject.GetComponent<Rigidbody>().mass = 0.1f;
                theCol.gameObject.GetComponent<Rigidbody>().drag = 1f;
            }
            else
            {
                theCol.gameObject.GetComponent<Rigidbody>().mass = 100f;
                theCol.gameObject.GetComponent<Rigidbody>().drag = 100f;
            }
        }
    }

    private void OnTriggerExit(Collider theCol)
    {
        //Becoming a human
        if (theCol.gameObject.CompareTag("Water"))
        {
            if (controller.movementStates != MovementStates.HumanMoving)
            {
                controller.movementStates = MovementStates.HumanMoving;
                Instantiate(controller.transformationPS, controller.humanMesh.transform.position, Quaternion.identity);
                controller.isTransforming = true;
                StartCoroutine(controller.TransformationDelay());
                deerEmotions.isCarryingItem = false;
            }
        }
    }
}
