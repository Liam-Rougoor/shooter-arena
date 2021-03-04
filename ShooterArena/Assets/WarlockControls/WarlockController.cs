using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed = 3f;
    [SerializeField] float turnAroundTime = 1f;

    Animator animator;
    CharacterController characterController;

    void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            StartCoroutine(TurnAround());
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
 
        HandleMovement(horizontal, vertical);
    }

    IEnumerator TurnAround() 
    {
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation * Quaternion.Euler(transform.up * 180);
        float elapsed = 0.0f;
        while (elapsed < turnAroundTime) 
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / turnAroundTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
    }

    void HandleMovement(float horizontal, float vertical)
    {
        Vector3 moveDirection = new Vector3(0, 0, vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        bool isMoving = moveDirection.magnitude > 0;
        moveDirection.Normalize();
        animator.SetBool("isMoving", isMoving);
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        transform.Rotate(0, horizontal * rotationSpeed, 0);
    }
}
