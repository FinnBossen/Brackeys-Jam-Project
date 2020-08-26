using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindCubeTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
