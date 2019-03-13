using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public enum CameraStates
{
    StartingAreaZone,
    PickupAreaZone,
    BridgeCrossingZone,
    MainAreaZone,
    WaterZone,
    FinalAreaZone,
}

public enum AreasForCameras
{
    AreaOne,
    AreaTwo,
    AreaThree,
    Water,
    Statue,
}

public class CameraControllers : MonoBehaviour
{
    public CameraStates camStates = CameraStates.StartingAreaZone;
    public AreasForCameras areaPositions = AreasForCameras.AreaOne;
    public GameObject startingAreaCam;
    public GameObject pickupAreaOneCam;
    public GameObject bridgeCrossingCam;
    public GameObject mainAreaCam;
    public GameObject waterCam;
    public GameObject finalAreaCam;

    public void Update()
    {

        switch (camStates)
        {
            //Starting are one states
            case CameraStates.StartingAreaZone:
                break;

            //Pick up area one states
            case CameraStates.PickupAreaZone:
                break;

            //bridge crossing states
            case CameraStates.BridgeCrossingZone:
                break;

            //Main area states
            case CameraStates.MainAreaZone:
                break;

            //Water states
            case CameraStates.WaterZone:
                break;

            //Final area states
            case CameraStates.FinalAreaZone:
                break;

        }
    }

    public void StartingAreaPriorities()
    {
        startingAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        pickupAreaOneCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        bridgeCrossingCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        mainAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        waterCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        finalAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
    }

    public void PickupAreaPriorities()
    {
        pickupAreaOneCam.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        startingAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        bridgeCrossingCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        mainAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        waterCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        finalAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
    }

    public void BridgeCrossingPriorities()
    {
        bridgeCrossingCam.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        startingAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        pickupAreaOneCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        mainAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        waterCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        finalAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
    }

    public void MainAreaPriorities()
    {
        mainAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        startingAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        pickupAreaOneCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        startingAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        waterCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        finalAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
    }

    public void WaterAreaPriorities()
    {
        waterCam.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        startingAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        pickupAreaOneCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        startingAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        mainAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        finalAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
    }

    public void FinalAreaPriorities()
    {
        finalAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        waterCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        startingAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        pickupAreaOneCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        startingAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        mainAreaCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
    }

    public void OnTriggerEnter(Collider theCol)
    {
        if (theCol.gameObject.CompareTag("Player"))
        {
            //Area one
            if (areaPositions == AreasForCameras.AreaOne)
            {
                if (camStates == CameraStates.StartingAreaZone)
                {
                    if (camStates != CameraStates.PickupAreaZone)
                    {
                        camStates = CameraStates.PickupAreaZone;
                        PickupAreaPriorities();
                    }
                }
                else if (camStates == CameraStates.PickupAreaZone)
                {
                    if (camStates != CameraStates.StartingAreaZone)
                    {
                        camStates = CameraStates.StartingAreaZone;
                        StartingAreaPriorities();
                    }
                }
            }
            //Area two
            else if (areaPositions == AreasForCameras.AreaTwo)
            {
                if (camStates == CameraStates.PickupAreaZone)
                {
                    if (camStates != CameraStates.BridgeCrossingZone)
                    {
                        camStates = CameraStates.BridgeCrossingZone;
                        BridgeCrossingPriorities();
                    }
                }
                else if (camStates == CameraStates.BridgeCrossingZone)
                {
                    if (camStates != CameraStates.PickupAreaZone)
                    {
                        camStates = CameraStates.PickupAreaZone;
                        PickupAreaPriorities();
                    }
                }
            }
            //Area three
            else if (areaPositions == AreasForCameras.AreaThree)
            {
                if (camStates == CameraStates.BridgeCrossingZone)
                {
                    if (camStates != CameraStates.MainAreaZone)
                    {
                        camStates = CameraStates.MainAreaZone;
                        MainAreaPriorities();
                    }
                }
                else if (camStates == CameraStates.MainAreaZone)
                {
                    if (camStates != CameraStates.BridgeCrossingZone)
                    {
                        camStates = CameraStates.BridgeCrossingZone;
                        BridgeCrossingPriorities();
                    }
                }
            }
            //Water area
            else if (areaPositions == AreasForCameras.Water)
            {
                if (camStates == CameraStates.MainAreaZone)
                {
                    if (camStates != CameraStates.WaterZone)
                    {
                        camStates = CameraStates.WaterZone;
                        WaterAreaPriorities();
                    }
                }
                else if (camStates == CameraStates.WaterZone)
                {
                    if (camStates != CameraStates.MainAreaZone)
                    {
                        camStates = CameraStates.MainAreaZone;
                        MainAreaPriorities();
                    }
                }
            }
            //Final area
            else if (areaPositions == AreasForCameras.Statue)
            {
                if (camStates == CameraStates.MainAreaZone)
                {
                    if (camStates != CameraStates.FinalAreaZone)
                    {
                        camStates = CameraStates.FinalAreaZone;
                        FinalAreaPriorities();
                    }
                }
                else if (camStates == CameraStates.FinalAreaZone)
                {
                    if (camStates != CameraStates.MainAreaZone)
                    {
                        camStates = CameraStates.MainAreaZone;
                        MainAreaPriorities();
                    }
                }
            }
        }
    }
}