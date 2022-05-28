using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public Vector3 Drag;

    [SerializeField]
    public float DashDistance = 15f;

    [SerializeField]
    private float playerSpeed = 5f;

    [SerializeField]
    private float jumpHeight = 1.0f;

    [SerializeField]
    private float gravityValue = -9.81f;

    [SerializeField]
    private float dashVelocity = 20f;

    [SerializeField]
    private bool isShielding = false;

    Rigidbody rb;
    GameObject shield;

    Vector3 lookTarget;

    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        shield = GameObject.FindGameObjectWithTag("Shield");
        shield.SetActive(false);
    }

    void Update()
    {

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            lookTarget = new Vector3(hit.point.x, transform.position.y, hit.point.z);

        }
        transform.LookAt(lookTarget);


        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        playerVelocity.x /= 1 + Drag.x * Time.deltaTime;
        playerVelocity.y /= 1 + Drag.y * Time.deltaTime;
        playerVelocity.z /= 1 + Drag.z * Time.deltaTime;

        if (Input.GetMouseButton(1))
        {
            isShielding = true;
            shield.SetActive(true);

        }
        else if (Input.GetMouseButtonUp(1))
        {
            isShielding = false;
            shield.SetActive(false);
        }

        if (isShielding && Input.GetMouseButtonDown(0))
        {
            print("dashing");
            playerVelocity += Vector3.Scale(transform.forward,
                                      DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime),
                                                                 0,
                                                                 (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));
        }
    }
}