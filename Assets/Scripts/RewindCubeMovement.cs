using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RewindCubeMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject player;
    List<Vector3> movePoints = new List<Vector3>();
    private float seconds = 0.05f;
    private float timer = 10f;
    private Vector2 start;
    private Vector2 Difference;
    private float percent ;
    private int count;
    // Start is called before the first frame update

    private void Awake()
    {
        player = GameObject.Find("Player");
        movePoints = player.GetComponent<makeRewindCube>().GetMovePoints();
        Debug.Log("Count"+ movePoints.Count);
        if (movePoints.Count > 0)
        {
            count = movePoints.Count - 1;
        }
        transform.position = new Vector2(movePoints[count].x, movePoints[count].y);
        player.GetComponent<makeRewindCube>().GetMovePoints();
    }

    // Update is called once per frame
    void Update()
    {
   
            if (timer <= seconds) {
                // basic timer
                timer += Time.deltaTime;
                // percent is a 0-1 float showing the percentage of time that has passed on our timer!
                percent = timer / seconds;
                // multiply the percentage to the difference of our two positions
                // and add to the start
                transform.position = start + Difference * percent; 
            }
            else
            {
                if (count == 0)
                {
                    if(transform.childCount > 0)
                    {
                        transform.GetChild(0).transform.SetParent(null);
                    }
                    count = movePoints.Count - 1;
                    transform.position = new Vector2(movePoints[count].x, movePoints[count].y);
         
                }
                else
                {   
                    start = new Vector2(movePoints[count].x, movePoints[count].y);
                    count--;
                    Difference = new Vector2(movePoints[count].x, movePoints[count].y) - start;
                    timer = 0f;
                    percent = 0f;
                }
            }
    }
}
