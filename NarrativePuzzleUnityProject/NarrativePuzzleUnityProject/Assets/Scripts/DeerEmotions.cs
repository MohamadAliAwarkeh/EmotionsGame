using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerEmotions : MonoBehaviour
{
    public Transform carryDestination;
    public bool isCarryingItem;
    public float deerTimer;
    public Transform pickupOnePos;

    private PlayerMovementController controller;

    void Start()
    {
        //Getting the controller
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
    }

    void Update()
    {

        if (controller.movementStates != MovementStates.DeerMoving && isCarryingItem)
        {
            //Setting the gravity on
            carryDestination.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
            //Setting the position of the object
            carryDestination.GetChild(0).transform.position = pickupOnePos.position;
            //Setting the trigger
            carryDestination.GetChild(0).GetComponent<BoxCollider>().isTrigger = false;
            //Removing it from being a child
            carryDestination.GetChild(0).SetParent(null);
            //Setting the bool
            isCarryingItem = false;
        }

        if (!isCarryingItem)
        {

        }
    }

    void DeerTimer()
    {
        deerTimer -= Time.deltaTime;
        if (deerTimer <= 0)
        {
            controller.movementStates = MovementStates.HumanMoving;
            deerTimer = 30;
            isCarryingItem = false;
        }
    }

    private void OnCollisionEnter(Collision theCol)
    {
        if (theCol.gameObject.CompareTag("Pickup") && controller.movementStates == MovementStates.HumanMoving)
        {
            //If it isnt a deer, turning it into a deer
            if (controller.movementStates != MovementStates.DeerMoving && !isCarryingItem)
            {
                //Changing into the deer state
                controller.movementStates = MovementStates.DeerMoving;
                //Instantiating the transformation effect
                Instantiate(controller.transformationPS, controller.deerMesh.transform.position, Quaternion.identity);
                //Setting the bool so the player cannot walk
                controller.isTransforming = true;
                //Starting the coroutine
                StartCoroutine(controller.TransformationDelay());
                //Turning the gravity off
                theCol.gameObject.GetComponent<Rigidbody>().useGravity = false;
                //Setting the position
                theCol.gameObject.transform.position = carryDestination.position;
                //Additionally, setting it to parent to the position so it stays
                theCol.gameObject.transform.SetParent(carryDestination.transform);
                //Setting velocity
                theCol.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                theCol.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                //Turning the collider into a trigger so it doesnt mess with stuff
                theCol.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                //Setting the bool
                isCarryingItem = true;
            }
        }
    }
}
