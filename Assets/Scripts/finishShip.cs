using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishShip : MonoBehaviour
{
    // Start is called before the first frame update
    private float _movementSpeed = 4f;
    private bool finishActivated = false;

    // Update is called once per frame
    void Update()
    {
        if (finishActivated)
        {
            gameObject.transform.position += Vector3.right * Time.deltaTime * _movementSpeed;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
            finishActivated = true;
            collision.gameObject.GetComponent<PlayerMovement>().finishedGame();
            
        }
    }
}
