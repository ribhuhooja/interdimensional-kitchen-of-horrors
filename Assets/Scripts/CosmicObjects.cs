using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class CosmicObjects : MonoBehaviour
{
    private float dayLength;
    void Start()
    {
        GameManager manager = GameManager.Instance;
        dayLength = manager.DayLength;
        manager.SubscribeToPerFrame(SetRotation);
    }

    public void SetRotation(float currentTime)
    {
        float t = currentTime / (2*dayLength);
        t -= 0.5f;      // to set in phase
        transform.up = RotationVector(2*Mathf.PI * t);
    }

    private static Vector2 RotationVector(float angleInRadians)
    {
        return new Vector2((float)Math.Cos(angleInRadians), (float)Mathf.Sin(angleInRadians));
    }
}
