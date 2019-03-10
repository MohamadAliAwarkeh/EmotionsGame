using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LeftOrRight
{
    Left,
    Right
}

public class ElephantStatue : MonoBehaviour
{
    public LeftOrRight eleStatue;
    public Transform itemPlacement;
    public bool isFull;
    public DeerEmotions deerEmotions;
    public PlayerMovementController controller;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
        deerEmotions = GameObject.FindGameObjectWithTag("Player").GetComponent<DeerEmotions>();
    }

    private void OnTriggerEnter(Collider theCol)
    {
        if (theCol.gameObject.CompareTag("Pickup") && deerEmotions.isCarryingItem == true && !isFull)
        {
            controller.movementStates = MovementStates.HumanMoving;
            theCol.gameObject.transform.position = itemPlacement.position;
            theCol.gameObject.transform.parent = null;
            isFull = true;
        }

        /*
        if (eleStatue == LeftOrRight.Left)
        {
            if (theCol.gameObject.CompareTag("Pickup") && deerEmotions.isCarryingItem == true && !isFull)
            {
                controller.movementStates = MovementStates.HumanMoving;
                theCol.gameObject.transform.position = itemPlacement.position;
                theCol.gameObject.transform.parent = null;
                isFull = true;
            }
        }

        if (eleStatue == LeftOrRight.Right)
        {
            if (theCol.gameObject.CompareTag("Player") && deerEmotions.isCarryingItem == true && !isFull)
            {
                playerController.playerStates = PlayerStates.HumanMoving;
                playerController.gemTwoGO.transform.position = itemPlacement.position;
                playerController.gemTwoGO.transform.parent = null;
                playerController.isCarryingItem = false;
                isFull = true;
            }
        }
        */
    }
}
