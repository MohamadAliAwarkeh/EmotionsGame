using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public GameObject cam;
    public bool inAreaOne;
    public Transform areaOnePos;
    public Transform areaTwoPos;

    void Start()
    {
        cam.transform.position = areaOnePos.position;
        inAreaOne = true;
    }

    private void OnTriggerEnter(Collider theCol)
    {
        if (theCol.gameObject.CompareTag("Player") && inAreaOne)
        {
            inAreaOne = false;
            cam.transform.position = areaTwoPos.position;
        }
        else if (theCol.gameObject.CompareTag("Player") && !inAreaOne)
        {
            inAreaOne = true;
            cam.transform.position = areaOnePos.position;
        }
    }
}
