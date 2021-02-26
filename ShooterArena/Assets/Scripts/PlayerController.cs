using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject reticle;

    Animator animator;
    CharacterController characterController;

    [SerializeField]
    bool attacking;

    void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            attacking = !attacking;
            animator.SetBool("isStrafing", attacking);
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (!attacking)
        {
            reticle.SetActive(false);
            HandleFreeMovement(horizontal, vertical);
        }
        else 
        {
            reticle.SetActive(true);
            HandleAttackMovement(horizontal, vertical);
        }
        
    }

    void HandleFreeMovement(float horizontal, float vertical) 
    {
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        bool isMoving = moveDirection.magnitude > 0;
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0.0f;
        moveDirection.Normalize();
        animator.SetBool("isMoving", isMoving);
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        if (moveDirection.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    void HandleAttackMovement(float horizontal, float vertical)
    {
        
        animator.SetFloat("StrafeX", horizontal);
        animator.SetFloat("StrafeY", vertical);
        //Strafing
        //Back always to camera
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection.y = 0.0f;
        moveDirection.Normalize();
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        //Rotate with camera
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 lookDirection = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
