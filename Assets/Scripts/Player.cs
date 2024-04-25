using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Placeable
{
    private Controls controls;

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Awake()
    {
        controls = new Controls();
        controls.Player.HorizontalMove.performed += HorizontalMoveOnperformed;
        controls.Player.VerticalMove.performed += VerticalMoveOnperformed;
    }

    private void VerticalMoveOnperformed(InputAction.CallbackContext obj)
    {
        AttemptMovePlayer(new(0, obj.ReadValue<int>()));
    }

    private void HorizontalMoveOnperformed(InputAction.CallbackContext obj)
    {
        AttemptMovePlayer(new(obj.ReadValue<int>(), 0));
    }

    private void MoveOnperformed(InputAction.CallbackContext obj)
    {
        obj.ReadValue<int>();
    }

    private void AttemptMovePlayer(Vector2Int move)
    {
        
    }
}
