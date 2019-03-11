using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTrigger : MonoBehaviour
{
    public PlayerAnimalController playerController;

    public void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimalController>();
    }

    public void OnTriggerExit(Collider theCol)
    {
        if (theCol.gameObject.CompareTag("PickUp2"))
        {
            playerController.gemTwoGO.GetComponent<BoxCollider>().isTrigger = true;
            playerController.gemTwoRB.useGravity = false;
        }
    }
}
