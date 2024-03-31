using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private const string IS_WALKING = "speed";

    private CharacterController controller;
    private Animator animator;

    [SerializeField] private float speed;
    [SerializeField] private Transform spawnPoint;

    public bool canMove = true;
    private bool isColliding;
    private bool isRight;
    public bool isFrozen;

    Vector3 moveDirection = Vector3.zero;

    [SerializeField] private float idleTime;

    [SerializeField] private Slider freezeBar;

    [SerializeField] private GameObject freezePuzzlePanel;
    [SerializeField] private GameObject interactionText;

    [SerializeField] float freezeTime;

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.gameObject.GetComponent<CharacterMovement>())
        {
            if (isColliding)
                return;
            isColliding = true;


            StartCoroutine(StayIdleAndRotate());
        }
        else
        {
            collision.gameObject.GetComponent<CharacterMovement>().Respawn(spawnPoint.position);
        }
    }

    //private void OnTriggerStay(Collider collision)
    //{
    //    if (!collision.gameObject.GetComponent<CharacterMovement>())
    //    {
            


    //        StartCoroutine(StayIdleAndRotate());
    //    }
    //    else
    //    {
    //        collision.gameObject.GetComponent<CharacterMovement>().Respawn(spawnPoint.position);
    //    }
    //}

    private void Awake()
    {
        isRight = true;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        freezeBar.maxValue = freezeTime;
    }

    private void Update()
    {
        if (!GameHandler.Instance.EnemyDisablePuzzleOngoing && !isColliding && !isFrozen)
        {
            canMove = true;
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float moveSpeed = canMove? speed : 0f;
        
        moveDirection = (forward * moveSpeed);
        controller.Move(moveDirection * Time.deltaTime);
        
        animator.SetFloat(IS_WALKING, moveDirection.sqrMagnitude);

    }

    IEnumerator StayIdleAndRotate()
    {
        canMove = false;
        isRight = !isRight;


        float dir = isRight ? -180f : 180f;

        transform.Rotate(0f, dir, 0f);

        yield return new WaitForSeconds(idleTime);
        isColliding = false;


        if (!GameHandler.Instance.EnemyDisablePuzzleOngoing)
        {
            canMove = true;
        }
    }

    public void ShowInteractionText()
    {
        interactionText.gameObject.SetActive(true); 
    }

    public void ShowFreezePuzzlePanel()
    {
        freezePuzzlePanel.SetActive(true);
    }

    public void HideFreezePuzzlePanel()
    {
        freezePuzzlePanel.SetActive(false);
    }

    public void FreezeEnemy()
    {
        freezeBar.gameObject.SetActive(true);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        isFrozen = true;
        while(freezeTime >= 0)
        {
            freezeTime -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
            freezeBar.value = freezeTime;
        }
        isFrozen = false;
        freezeBar.gameObject.SetActive(false);
    }
}
