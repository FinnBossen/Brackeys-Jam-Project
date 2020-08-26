using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    // Update is called once per frame
    public GameObject bullet;
    public float time = 4;
    private float timer;

    private void Start()
    {
        timer = time;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Instantiate (bullet, transform);
            timer = time;
        }
    }
}
