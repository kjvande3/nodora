using System;
using UnityEngine;

[RequireComponent(typeof(PlatformingPhysics))]
public class PlayerPlatformingController : MonoBehaviour
{
    private PlatformingPhysics platformingPhysics;

    private void Start()
    {
        platformingPhysics = GetComponent<PlatformingPhysics>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            platformingPhysics.MoveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            platformingPhysics.MoveRight();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            platformingPhysics.Jump();
        }
    }
}
