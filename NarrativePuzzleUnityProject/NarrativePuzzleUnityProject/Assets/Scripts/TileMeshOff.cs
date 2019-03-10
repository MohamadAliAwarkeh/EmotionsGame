using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMeshOff : MonoBehaviour
{
    //Private variables
    private MeshRenderer mesh;
    public float meshClimb;

    //Private variables
    private Vector3 lowerPos;
    private Vector3 currentPos;

    void Start()
    {
        //Gets the meshRenderer componenet
        mesh = GetComponent<MeshRenderer>();
        //Disabled it
        mesh.enabled = false;
    }

    void Update()
    {

    }
}
