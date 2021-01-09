﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class New_Camera : MonoBehaviour
{
    CinemachineFreeLook Camera;
    public void Setup()
    {
        Debug.Log("Attempting to access LocalStats.Player");
        Camera = GetComponent<CinemachineFreeLook>();
        Camera.Follow = LocalStats.Player.transform;
        Camera.LookAt = LocalStats.Player.transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
