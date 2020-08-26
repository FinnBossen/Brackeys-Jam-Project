using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chekifUnderneath : MonoBehaviour
{
    // Start is called before the first frame update
    private collisionControl _collisionControl;
    void Start()
    {
        _collisionControl = gameObject.transform.parent.gameObject.GetComponent<collisionControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("RewindCube"))
        {
            _collisionControl.ResetStomper();

        }
    }
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("RewindCube"))
        {
            _collisionControl.ResetStomper();

        }
    }
}
