using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private PlayerInputActions playerInputActions; //reference to our actionmap
    private Rigidbody2D rb;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();  //instantiate our controls
        rb = GetComponent<Rigidbody2D>(); //Grabs the rigidbody attatched to the player
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
