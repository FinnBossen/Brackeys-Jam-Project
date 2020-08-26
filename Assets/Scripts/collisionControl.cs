using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionControl : MonoBehaviour
{
    private Animation _stomperAnim;
    private string _animName;
    private bool rewinds = false;
    // Start is called before the first frame update
    void Start()
    {
        _animName = "StomperAnim";
        _stomperAnim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rewinds)
        {
            if (_stomperAnim[_animName].time <= 0f)
            {
              ResetStomper();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("RewindCube"))
        {
            Debug.Log("Stomper Collided with RewindCube");
            _stomperAnim[_animName].speed = -1f;
            rewinds = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("RewindCube"))
        {
            Debug.Log("Stomper Collided with RewindCube");
            _stomperAnim[_animName].speed = 1f;
            rewinds = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.CompareTag("RewindCube"))
        {
  
                    Debug.Log("Stomper Collided with RewindCube");
                    _stomperAnim[_animName].speed = -1f;
                    rewinds = true;

        }
    }

    public void ResetStomper()
    {
        Debug.Log("reseted stomper");
        _stomperAnim[_animName].speed = 1f;
        _stomperAnim[_animName].time = 0f;
        _stomperAnim.Play(_animName);
    }
}
