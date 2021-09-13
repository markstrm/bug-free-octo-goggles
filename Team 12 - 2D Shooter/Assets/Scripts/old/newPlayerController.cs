using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newPlayerController : MonoBehaviour
{

    private PlayerInputActions playerInputActions; //reference to our actionmap

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletDirection;

    private bool canShoot = true;
    private Camera main;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    void Start()
    {
        main = Camera.main;
        playerInputActions.Player.Shoot.performed += _ => PlayerShoot();
    }

    private void PlayerShoot()
    {
        if (!canShoot) return;

        Vector2 mousePosition = playerInputActions.Player.MousePosition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        GameObject g = Instantiate(bullet, bulletDirection.position, bulletDirection.rotation);
        g.SetActive(true);
        StartCoroutine(CanShoot());
    }

    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(3f);
        canShoot = true;
    }

    void Update()
    {
        Vector2 mouseScreenPosition = playerInputActions.Player.MousePosition.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = main.ScreenToWorldPoint(mouseScreenPosition);
    }
}
