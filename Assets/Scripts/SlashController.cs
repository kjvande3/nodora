using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformingPhysics))]
public class SlashController : MonoBehaviour
{
    private PlatformingPhysics platformingPhysics;

    private Vector3 startAimingPos;
    private Vector3 stopAimingPos;

    private void Start()
    {
        platformingPhysics = GetComponent<PlatformingPhysics>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startAimingPos = Input.mousePosition;
            platformingPhysics.Aim();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            stopAimingPos = Input.mousePosition;

            float deltaX = stopAimingPos.x - startAimingPos.x;
            float deltaY = stopAimingPos.y - startAimingPos.y;

            float angle = Mathf.Atan2(deltaY, deltaX);
            angle = (angle + Mathf.PI) % (Mathf.PI * 2.0f);
            platformingPhysics.Slash(angle);
        }
    }
}
