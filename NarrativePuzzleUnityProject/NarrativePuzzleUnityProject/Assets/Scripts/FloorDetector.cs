using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetector : MonoBehaviour
{


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider theCol)
    {
        if (theCol.gameObject.CompareTag("Ground")) {
            theCol.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
