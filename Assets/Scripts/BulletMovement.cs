using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 4f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * (Time.deltaTime * speed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Turret"))
        {
            return;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
           
        }else if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }else if (collision.CompareTag("RewindCube"))
        {
            Destroy(gameObject);
        }
    }
}
