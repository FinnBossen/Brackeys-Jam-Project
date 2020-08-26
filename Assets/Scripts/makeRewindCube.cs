using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class makeRewindCube : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rewindCube;
    public GameObject mockUpRewindCube;
    public bool trailMode = true;
    public GameObject camera;
    private Camera mainCamera;
    private bool recordMousePoints = false;
    private List<Vector3> movePoints = new List<Vector3>();
    private float elapsed = 0f;
    private float timeConsumed = 0f;
    private bool timeEnded = false;
    private GameObject spawnedCube;
    private bool longEnoughToInitiate = false;
    private SpriteRenderer mockupSprite;
    public GameObject platformMagicIdle;
    private PlayerMovement playerMovement;
    private bool collidedWithGround = false;
    public GameObject timer;
    private TextMesh textMeshTimer;
    private float time = 8;
    private void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        mainCamera = camera.GetComponent<Camera>();
        textMeshTimer = timer.GetComponent<TextMesh>();
        textMeshTimer.text = 8f.ToString("0.0");
    }
    // Update is called once per frame
    private void Update()
    {
        if (trailMode)
        {
            if (Input.GetButtonDown("Fire1"))
            {
               
                longEnoughToInitiate = false;
                timeConsumed = 0;
                spawnedCube = Instantiate(mockUpRewindCube);
                mockupSprite = spawnedCube.GetComponent<SpriteRenderer>();
                mockupSprite.color = Color.red;
                AddMousePoint();
                recordMousePoints = true;
                platformMagicIdle.SetActive(true);
                playerMovement.toggleMagic();
                Debug.Log("trail started");
               
            }
            if (Input.GetButtonUp("Fire1") || timeEnded || collidedWithGround)
            {
                if (!recordMousePoints)
                    return;
             
                AddMousePoint();
                recordMousePoints = false;
                timeEnded = false;
                Debug.Log("trail ended");
                Debug.Log("Count" + movePoints.Count);
                if (longEnoughToInitiate &&!collidedWithGround)
                {
                    GameObject[] oldBlock = GameObject.FindGameObjectsWithTag("RewindCube");
                    if (oldBlock.Length > 0)
                    {
                        Destroy(oldBlock[0].gameObject);
                        Debug.Log(" oldBlock has been destroyed.");
                    }
                    
                    Instantiate(rewindCube);
                }
                else
                {
                    collidedWithGround = false;
                    movePoints.Clear();
                }
                platformMagicIdle.SetActive(false);
                playerMovement.toggleMagic();
                Destroy(spawnedCube);
                time = 8f;
                textMeshTimer.text = 8f.ToString("0.0");
            }
        }

    }

    private void FixedUpdate()
    {
        if (recordMousePoints)
        {
           
            spawnedCube.transform.position =  CreateMouseVector();
        }
    }

    private float getDistance(Vector3 vector1, Vector3 vector2)
    {
        Vector3 difference = new Vector3(
            vector1.x - vector2.x,
            vector1.y - vector2.y,
            vector1.z - vector2.z);
       
        return (float)Math.Sqrt(
            Math.Pow(difference.x, 2f) +
            Math.Pow(difference.y, 2f) +
            Math.Pow(difference.z, 2f));;
    }
    private void LateUpdate()
    {
        if (recordMousePoints)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 0.05f)
            {
                if (getDistance(movePoints[0], movePoints[movePoints.Count - 1]) > 2f)
                {
                    longEnoughToInitiate = true;
                    mockupSprite.color = Color.white;
                };
                timeConsumed += elapsed;
                textMeshTimer.text = (8f-timeConsumed).ToString("0.0");
                elapsed = 0;
                AddMousePoint();
                if (timeConsumed >= 8)
                {
                    timeEnded = true;
                }
            }
            CheckRayCastCollisionWithGround();
        }
        
    }

    private Vector3 CreateMouseVector()
    {
        Vector3 pz = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        return pz;
    }
    private void AddMousePoint()
    {
        movePoints.Add(CreateMouseVector());
    }
    public List<Vector3> GetMovePoints()
    {
        List<Vector3> trail = new List<Vector3>(movePoints);;
        movePoints.Clear();
        return trail;
     
    }

    public void CheckRayCastCollisionWithGround()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 5f;
 
        Vector2 v = Camera.main.ScreenToWorldPoint(mousePosition);
 
        Collider2D[] col = Physics2D.OverlapPointAll(v);
 
        if(col.Length > 0){
            foreach(Collider2D c in col)
            {
                Collider2D collider = c.GetComponent<Collider2D>();
                Debug.Log("Collided with: " + collider.gameObject.name);
                if(collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    if (!collider.gameObject.CompareTag("StomperGround") && !collider.gameObject.CompareTag("RewindCube"))
                    {
                        collidedWithGround = true;
                    }
                }
            }
        }
    }

}
