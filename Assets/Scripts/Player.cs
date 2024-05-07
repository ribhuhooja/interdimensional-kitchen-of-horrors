using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Placeable))]
public class Player : MonoBehaviour
{
    private Controls controls;
    private Placeable placeable;

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
        
        placeable = GetComponent<Placeable>();
    }


    private void VerticalMoveOnperformed(InputAction.CallbackContext obj)
    {
        AttemptMovePlayer(new(0, obj.ReadValue<float>()));
    }

    private void HorizontalMoveOnperformed(InputAction.CallbackContext obj)
    {
        AttemptMovePlayer(new(obj.ReadValue<float>(), 0));
    }

    private void MoveOnperformed(InputAction.CallbackContext obj)
    {
        obj.ReadValue<int>();
    }

    private void AttemptMovePlayer(Vector2 move)
    {
        // diagonal movement is not allowed
        if (move.x * move.y != 0)
        {
            return;
        }
        
        // get rid of floating point problems
        // constrain the x and y to -1, 0, 1
        int x = move.x > 0 ? 1 : (move.x < 0 ? -1 : 0);
        int y = move.y > 0 ? 1 : (move.y < 0 ? -1 : 0);

        placeable.ground.AttemptMove(placeable.tileOn, new(x, y));
    }
}
