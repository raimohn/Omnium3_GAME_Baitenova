using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private MobileJoystick playerJoystick;
    private Rigidbody2D rig;

    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;

    private Animator animator;
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = Vector2.right;
        animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        rig.velocity = playerJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
         if(moveInput != Vector2.zero){
            // Try to move player in input direction, followed by left right and up down input if failed
            bool success = MovePlayer(moveInput);
            
            if(!success)
            {
                // Try Left / Right
                success = MovePlayer(new Vector2(moveInput.x, 0));
 
                if(!success)
                {
                    success = MovePlayer(new Vector2(0, moveInput.y));
                }
            }
 
            animator.SetBool("isWalking", success);
        } 
        else{
            animator.SetBool("isWalking", false);
        }
    }

    private bool MovePlayer(Vector2 moveInput)
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        // Получаем направление от джойстика
        Vector2 moveInput = playerJoystick.GetMoveVector();

        // Устанавливаем параметры для Blend Tree
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);

    }
}
