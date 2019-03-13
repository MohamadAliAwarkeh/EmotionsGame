using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    public GameObject player;
    public Transform respawnZone;
    public CameraControllers camOneController;
    public CameraControllers camTwoController;

    private void OnTriggerEnter(Collider theCol)
    {
        if (theCol.gameObject.CompareTag("Player"))
        {
            theCol.gameObject.transform.position = respawnZone.position;
            camOneController.StartingAreaPriorities();
            camOneController.camStates = CameraStates.StartingAreaZone;
            camTwoController.camStates = CameraStates.PickupAreaZone;
        }
    }
}
