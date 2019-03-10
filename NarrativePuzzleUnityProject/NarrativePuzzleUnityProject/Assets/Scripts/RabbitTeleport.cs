using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitTeleport : MonoBehaviour
{
    public GameObject teleLocation;
    private float coolDown = 3;
    private bool canTele = true;

    private void Update()
    {
        coolDown -= Time.deltaTime;
        if (coolDown <= 0f)
        {
            canTele = true;
        }
    }

    private void OnTriggerEnter(Collider theCol)
    {
        if (theCol.gameObject.CompareTag("Player"))
        {
            theCol.transform.position = teleLocation.transform.position;
            canTele = false;
            coolDown = 3f;
        }
    }
}
