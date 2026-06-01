using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class PlayerControls : MonoBehaviour
{
    // Mouse Position (Duh)
    public Vector2 MousePositionVector;

    // Input Action refs
    InputAction MousePositionInput;
    InputAction MouseLeftClick;

    // Camera refs for world position
    public Camera GameCamera;

    // Processed values
    public Vector2 MouseDistPlayer;

    // Aim object objects & positions
    public List<GameObject> AimObjects = new List<GameObject>();

    // Gameplay
    public float PowerLimit = 5f;
    public Vector2 ShotPower;
    public int playerMoveCooldown;

    public GameManager gameManager;

    Rigidbody2D rb;
    void Start()
    {
        // Define as 0, 0 for now
        MouseDistPlayer = new Vector2(0f, 0f);

        // Reference input actions
        MousePositionInput = InputSystem.actions.FindAction("MousePosition");
        MouseLeftClick = InputSystem.actions.FindAction("MouseLeftClick");

        // Component Refs
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // Get the mouse's world position
        MousePositionVector = GameCamera.ScreenToWorldPoint(MousePositionInput.ReadValue<Vector2>());

        // Get distance from the mouse to the player
        MouseDistPlayer = new Vector2(MousePositionVector.x - transform.position.x, MousePositionVector.y - transform.position.y);

        ShotPower = new Vector2(Mathf.Clamp(MouseDistPlayer.x, -PowerLimit, PowerLimit), Mathf.Clamp(MouseDistPlayer.y, -PowerLimit, PowerLimit));

        for (int i = 0; i < AimObjects.Count; i++)
        {
            AimObjects[i].transform.localPosition = Vector2.Lerp(Vector2.zero, -ShotPower, ((float)i + 1) / AimObjects.Count);
        }

        if (MouseLeftClick.WasPressedThisFrame() && playerMoveCooldown <= 0)
        {
            LaunchAtom(ShotPower);
            playerMoveCooldown = 150;
        }
    }

    public void LaunchAtom(Vector2 power)
    {
        rb.linearVelocity = -power * 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Proton":
                rb.linearVelocity = rb.linearVelocity * 2;
                gameManager.UpdateScores(1, 0);
                collision.gameObject.SetActive(false);
                break;
            case "Neutron":
                gameManager.UpdateScores(0, 1);
                collision.gameObject.SetActive(false);
                break;
            case "Electron":
                rb.linearVelocity = -rb.linearVelocity * 2;
                gameManager.UpdateScores(-1, 0);
                collision.gameObject.SetActive(false);
                break;
        }
    }

    private void FixedUpdate()
    {
        if(playerMoveCooldown >= 0)
        {
            playerMoveCooldown -= 1;
        }
    }


}
