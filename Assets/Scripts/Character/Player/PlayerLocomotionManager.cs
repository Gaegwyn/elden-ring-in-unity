using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager playerManger;

    // Taken from InputManager
    public float verticalMovement;
    public float horizontalMovement;
    public float moveAmount;

    private Vector3 moveDirection;
    [SerializeField] float walkingSpeed = 2f;
    [SerializeField] float runningSpeed = 5f;

    protected override void Awake()
    {
        base.Awake();
        playerManger = GetComponent<PlayerManager>();
    }

    public void HandleAllMovement()
    {
        // Ground movement
        HandleGroundedMovement();
        // Aerial Movement
    }

    private void GetVerticalAndHorizontalInputs()
    {
        verticalMovement = PlayerInputManager.instance.verticalInput;
        horizontalMovement = PlayerInputManager.instance.horizontalInput;

        // TODO Clamp movement
    }    

    public void HandleGroundedMovement()
    {
        GetVerticalAndHorizontalInputs();

        // Movement direction based on camera direction and inputs
        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (PlayerInputManager.instance.moveAmount > 0.5f)
        {
            // Move at running speed
            playerManger.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
        }
        else if (PlayerInputManager.instance.moveAmount <= 0.5f)
        {
            // Move at walking speed
            playerManger.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
        }
    }
 

}
