using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    // Find a way to read inputs
    // Move character based off of input

    public static PlayerInputManager instance { get; private set; }

    PlayerControls playerControls;

    [SerializeField] Vector2 movementInput;
    [SerializeField] public float verticalInput;
    [SerializeField] public float horizontalInput;
    [SerializeField] public float moveAmount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnSceneChange;
        instance.enabled = false;
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        // Only enable player input when we're in the world scene
        if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
        {
            instance.enabled = true;
        }
        else
        {
            instance.enabled = false;
        }
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

        }

        playerControls.Enable();
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        // Returns the absolute number (number without the negative sign)
        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput + horizontalInput));

        // Clamp to either 0, 0.5, or 1.
        if (moveAmount <= 0.5f && moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if (moveAmount >= 0.5f && moveAmount <= 1)
        {
            moveAmount = 1;
        }
    }
}
