using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_check_Script : MonoBehaviour
{

    public Rigidbody rb;
    public Transform orientation;

    //Wallrunning
    public LayerMask whatIsWall;
    RaycastHit wallHitR, wallHitL;
    public bool isWallRight, isWallLeft;
    public float maxWallrunTime;
    public float wallrunForce;
    public float maxWallSpeed;
    bool readyToWallrunR, readyToWallrunL;
    public bool isWallRunning;
    public float maxWallRunCameraTilt;
    float wallRunCameraTilt = 0;





    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("walllllll");
            collision.gameObject.GetComponent<Rigidbody>().AddForce(0, 5, 0);
        }
    }

    private void Update()
    {
        //Check for Walls
        isWallRight = Physics.Raycast(transform.position, orientation.right, out wallHitR, 1f, whatIsWall);
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, out wallHitL, 1f, whatIsWall);

        //Stop wallrun
        if (!isWallLeft && !isWallRight) StopWallRun();

        //Wallrun Input
        if (Input.GetKey(KeyCode.D) && isWallRight) StartWallrun();
        if (Input.GetKey(KeyCode.A) && isWallLeft) StartWallrun();


    }

    private void StartWallrun()
    {
        rb.useGravity = false;
        isWallRunning = true;
        Debug.Log("wallrunning");


        if (rb.velocity.magnitude <= maxWallSpeed)
        {
            rb.AddForce(orientation.forward * wallrunForce * Time.deltaTime);

            //Make sure char sticks to wall
            if (isWallRight)
                rb.AddForce(orientation.right * wallrunForce / 5 * Time.deltaTime);
            else
                rb.AddForce(-orientation.right * wallrunForce / 5 * Time.deltaTime);
        }
    }
    private void StopWallRun()
    {
        isWallRunning = false;
        rb.useGravity = true;
    }
}
