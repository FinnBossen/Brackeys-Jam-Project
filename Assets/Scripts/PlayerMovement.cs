using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public GameObject head;
    public GameObject crouchHead; 
    public float runSpeed = 40f;
    private Rigidbody2D rigidbody;

    private bool doesMagic = false;
    //Anim Objects
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    RigidbodyConstraints2D originalConstraints;
    private bool isMoving = false;
    private bool finish = false;
    

    private void Start()
    { 
        rigidbody = GetComponent<Rigidbody2D>();
        originalConstraints = rigidbody.constraints;
    }

    private void LateUpdate()
    {
        if (isMoving)
        {
            rigidbody.constraints = originalConstraints;
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation ;
        }
    }

    void Update()
    {
       
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0 )
        {
            isMoving = true;
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }
        else
        {
            isMoving = false;
        }
       

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    void FixedUpdate()
    {
        // Move our character
        if (!doesMagic && !finish)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, isMoving);
            jump = false; 
        }
        else
        {
            controller.doesMagic();
        }
       
    }

    public void finishedGame()
    {
        gameObject.transform.localPosition=Vector3.zero;
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
        finish = true;
        StartCoroutine(ExampleCoroutine());
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        
    }
    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        SceneManager.LoadScene("Menu Scene");
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    public void toggleMagic()
    {
        doesMagic = !doesMagic;
    }
    
}