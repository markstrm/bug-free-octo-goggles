using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Rigidbody2D rb;

    Vector2 _MousePos;

    [SerializeField] private float speed = 10f; //be able to see the variable in the inspector and be able to change it
    [SerializeField] Camera _Camera;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>(); //Grabs the rigidbody attatched to the player
    }

    private void OnEnable()
    {
        playerInputActions.Enable();

        playerInputActions.Movement.MousePos.performed += OnMousePos;

    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void OnMousePos(InputAction.CallbackContext context)
    {
        _MousePos = _Camera.ScreenToWorldPoint(context.ReadValue<Vector2>()); //Camera is used to convert screenspace to worldspace where the player is.
    }

    private void FixedUpdate() //fixedupdate for physics
    {
        Vector2 moveInput = playerInputActions.Movement.Move.ReadValue<Vector2>(); //Movement refers to the Action map / get vector2 value from player input and store it in vector2 to be able to use later.
        rb.velocity = moveInput * speed;

        Vector2 facingDirection = _MousePos - rb.position;
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle);

    }

}