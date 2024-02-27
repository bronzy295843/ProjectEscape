using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public GameObject puzzlePanel;
    public GameObject interactionText;

    private CharacterController characterController;
    private Animator playerAnimator;
    const string IS_WALK = "speed";

    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    Vector3 moveDirection = Vector3.zero;
    public bool canMove = true;
    public bool canInteract = false;

    public Camera playerCamera;
    float rotationX = 0;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    [SerializeField] private float sphereRadius = 0.5f;
    [SerializeField]private float groundCheckDistance = 0.9f;
    [SerializeField] private Vector3 deviation;

    public GameHandler gameHandler;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        //movement handling
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //handles jumping when implemented later
        //if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        //{
        //    moveDirection.y = jumpPower;
        //}
        //else
        //{
        //    moveDirection.y = movementDirectionY;
        //}

        if (!IsGrounded())
        {
            moveDirection.y -= gravity ;
        }
        else
        {
            Debug.Log("Grounded");
        }

        characterController.Move(moveDirection * Time.deltaTime);
        playerAnimator.SetFloat(IS_WALK, moveDirection.magnitude);
        
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if(Input.GetKey(KeyCode.F) && canInteract)
        {
            if (!GameHandler.Instance.PuzzleCompleted)
            {
                puzzlePanel.SetActive(true);
                interactionText.SetActive(false);
                canMove = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

    }

    public void SetCanMove(bool setCanMove){
        canMove = setCanMove;
    }

    public void CursorLockOn()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        bool isGrounded = Physics.SphereCast(transform.position + deviation, sphereRadius, Vector3.down, out hit, groundCheckDistance);

        if (isGrounded)
            return true;
        else
            return false;
    }
}
