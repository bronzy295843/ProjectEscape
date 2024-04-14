using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public GameObject puzzlePanel;
    [SerializeField] private GameObject interactionText;
    [SerializeField] private GameObject pickUpInteractionText;
    [SerializeField] private GameObject dropInteractionText;
    [SerializeField] private GameObject enemyInteractionText;

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

    private bool isHolding;
    [SerializeField] private float sphereRadius = 0.5f;
    [SerializeField]private float groundCheckDistance = 0.9f;
    [SerializeField] private Vector3 deviation;
    [SerializeField] GameObject objectHolder;
    private GameObject heldObject;
    private GameObject placementObject;

    public GameHandler gameHandler;
    public GameObject EnemyInteractingWith;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void Awake()
    {
        canMove = false;
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
            moveDirection.y -= gravity;
        }
        else
        {
            //Debug.Log("Grounded");
        }

        characterController.Move(moveDirection * Time.deltaTime);
        playerAnimator.SetFloat(IS_WALK, moveDirection.magnitude);

        if (canMove)
        {
            SavePosition();

            if (Input.GetKey(KeyCode.E))
            {
                UIManager.Instance.ShowPauseMenu();
            }

            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

       

        if (Input.GetKey(KeyCode.F) && canInteract)
        {
            puzzlePanel.SetActive(true);
            interactionText.SetActive(false);
            canMove = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
        }

        if(Input.GetKeyDown(KeyCode.F) && GetComponent<ObjectDetection>().AnyObjectDetected() && !isHolding)
        {
            heldObject = GetComponent<ObjectDetection>().GetInteractedObject();
            heldObject.GetComponent<Rigidbody>().useGravity = false;

            heldObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ
                                                              |RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            heldObject.GetComponent<ObjectHoldInteractable>().ChangeParent(objectHolder.transform);
            heldObject.transform.localPosition = Vector3.zero;
            isHolding = true;
        }
        
        else if(Input.GetKeyDown(KeyCode.F) && isHolding && GetComponent<ObjectDetection>().AnyPlacementObjectDetected())
        {
            placementObject = GetComponent<ObjectDetection>().GetInteractedPlacementObject();
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            //heldObject.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionX | ~RigidbodyConstraints.FreezePositionY | ~RigidbodyConstraints.FreezePositionZ;
            heldObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            heldObject.GetComponent<ObjectHoldInteractable>().ChangeParent(placementObject.transform);
            heldObject.transform.localPosition = Vector3.zero;
            heldObject = null;
            isHolding = false;
        }

        else if (Input.GetKeyDown(KeyCode.F) && isHolding)
        {
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            heldObject.GetComponent<ObjectHoldInteractable>().ChangeParent(null);
            heldObject = null;
            isHolding = false;
        }
        else if(Input.GetKeyDown(KeyCode.F) && GetComponent<ObjectDetection>().AnyEnemyDetected())
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            EnemyInteractingWith = GetComponent<ObjectDetection>().GetInteractedEnemy();
            EnemyInteractingWith.GetComponent<EnemyController>().ShowFreezePuzzlePanel();
            EnemyInteractingWith.GetComponent<EnemyController>().canMove = false;
            EnemyInteractingWith.GetComponent<EnemyController>().isFrozen = true;
        }

    }

    private void SavePosition()
    {
        if (PlayerPrefs.GetInt("HasSaveState", 0) == 0) 
        {
            PlayerPrefs.SetInt("HasSaveState", 1);
        }

        PlayerPrefs.SetFloat("xPosition", transform.position.x); 
        PlayerPrefs.SetFloat("yPosition", transform.position.y); 
        PlayerPrefs.SetFloat("zPosition", transform.position.z); 
    }

    public void LoadPosition()
    {
        characterController.enabled = false;
        transform.position = new Vector3(PlayerPrefs.GetFloat("xPosition"), PlayerPrefs.GetFloat("yPosition"), PlayerPrefs.GetFloat("zPosition"));
        characterController.enabled = true;
    }

    public void LoadStartingPosition()
    {
        characterController.enabled = false;
        transform.position = new Vector3(-7.4f, 0.6f, 3.98f);
        characterController.enabled = true;
    }

    public void Respawn(Vector3 pos)
    {
        Debug.Log("Respawn!");
        characterController.enabled = false;
        transform.position = pos;
        characterController.enabled = true;
    }
    public void SetPuzzleToInteract(GameObject puzzle)
    {
        puzzlePanel = puzzle;
    }

    public void HideEnemyInteractPopUp()
    {
        enemyInteractionText.SetActive(false);
    }

    public void HideInteractPopUp()
    {
        interactionText.SetActive(false);
    }

    public void HidePickUpInteractPopUp()
    {
        pickUpInteractionText.SetActive(false);
    }
    public void HideDropInteractPopUp()
    {
        dropInteractionText.SetActive(false);
    }
    public void SetCanMove(bool setCanMove){
        canMove = setCanMove;
    }

    public void CursorLockOn()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public bool IsHoldingObject()
    {
        return isHolding;
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
