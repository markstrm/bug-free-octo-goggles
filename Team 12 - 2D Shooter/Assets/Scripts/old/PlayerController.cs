using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions; //reference to our actionmap
    private Rigidbody2D rb;
    private bool canShoot = true;
    private Camera main;

    Vector2 _MousePos;

    [SerializeField] private float speed = 10f; //be able to see the variable in the inspector and be able to change it
    [SerializeField] Camera _Camera;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletDirection;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();  //instantiate our controls
        rb = GetComponent<Rigidbody2D>(); //Grabs the rigidbody attatched to the player
    }

    private void OnEnable()
    {
        playerInputActions.Enable();    // enables our controls

        playerInputActions.Player.MousePosition.performed += OnMousePos;    //Action map -> Fixa

    }

    private void OnDisable()
    {
        playerInputActions.Disable();   // disables our controls
    }

    private void OnMousePos(InputAction.CallbackContext context)
    {
        _MousePos = _Camera.ScreenToWorldPoint(context.ReadValue<Vector2>()); //Camera is used to convert screenspace to worldspace where the player is.
    }

    private void Start()
    {
        playerInputActions.Player.Shoot.performed += _ => PlayerFire();    //listens to our mouseclicks
        main = Camera.main;
    }

    private void PlayerFire()
    {
        Vector2 mousePosition = playerInputActions.Player.MousePosition.ReadValue<Vector2>(); //Reading vecto2 of current mousepos
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        GameObject g = Instantiate(bullet, bulletDirection.position, bulletDirection.rotation);
        g.SetActive(true);
        StartCoroutine(CanShoot());

    }

    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(.5f);
        canShoot = true;
    }
        

    private void FixedUpdate() //fixedupdate for physics
    {
        Vector2 moveInput = playerInputActions.Player.Movement.ReadValue<Vector2>(); //Movement refers to the Action map / get vector2 value from player input and store it in vector2 to be able to use later.
        rb.velocity = moveInput * speed;

        /**Vector2 facingDirection = _MousePos - rb.position;
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle);
        **/

        // Rotation
        Vector2 mouseScreenPosition = playerInputActions.Player.MousePosition.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = main.ScreenToWorldPoint(mouseScreenPosition);
        Vector3 targetDirection = mouseWorldPosition - transform.position; //returns vector pointing in the direction of our mousepos in the world
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

    }

    /**private void Update()
    {
        
        // Rotation
        Vector2 mouseScreenPosition = playerInputActions.Player.MousePos.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = main.ScreenToWorldPoint(mouseScreenPosition);
        Vector3 targetDirection = mouseWorldPosition - transform.position; //returns vector pointing in the direction of our mousepos in the world
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));    
    }
    **/
}