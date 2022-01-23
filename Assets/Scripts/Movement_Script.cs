using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement_Script : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movement")]
    public float speed = 20f;
    public float gravity = -10f;
    public float jump_Height = 3f;
    public float wall_Jump_Height = 10f;
    public float mouse_Sens = 100f;
    public bool sprint = false;

    [Header("Ground")]
    public Transform ground_Check;
    public float ground_Distance = 0.4f;
    public LayerMask ground_Mask;
    Vector3 Velocity;
    public bool is_Grounded = false;

    [Header("Wall Run")]
    public GameObject player_Cam;
    public Rigidbody rb;
    public Transform orientation;
    private float desiredX;
    private float xRotation;
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
    public float wallRunCameraTilt = 0;
    public float jump_count = 0;
    public bool wall_Run_Jump = false;

    [Header("Pizza")]
    public bool pizza_Pick_Up = false;

    [Header("Timer")]
    public float start_Time = 0f;
    public Text countdown_Text;
    public float t;
    public float time_Storage;

    [Header("Score")]
    public Text score;
    public Text score_breakdown;
    public int total_Score;
    public int current_score;
    public int multiplyier = 1;
    public int off_ground_time;
    public int breakdown_Total;

    [Header("Pizza & Flag")]
    public Goal_Spawn_Script GSS;
    public Pizza_Spawn_Script PSS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            multiplyier = 2;
            Destroy(other.gameObject);
        }
    }

    private void Grounded()
    {
        is_Grounded = Physics.CheckSphere(ground_Check.position, ground_Distance, ground_Mask);
        if (is_Grounded)
        {
            jump_count = 1;
        }
    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        t = start_Time;
        countdown_Text.enabled = false;
    }

    private void Update()
    {//detect pizza
        if (pizza_Pick_Up == false)
        {
            GSS.bool_Check = 0;
        }
        if (pizza_Pick_Up == true)
        {
            PSS.bool_Check = 0;
        }

        score.text = "Total Score: " + total_Score.ToString();
        //breakdown_Total = current_score + off_ground_time * multiplyier;

        //if pizza picked up then start a timer
        if (pizza_Pick_Up == true)
        {
            countdown_Text.enabled = true;
            t += Time.deltaTime;
            countdown_Text.text = t.ToString("f1");


        }
        else if (pizza_Pick_Up == false && t > 0)
        {
            time_Storage = t;
            t = 0f;


            //when pizza delivered to objective find time and award score based on time
            if (time_Storage > 60)
            {

                Debug.Log("Bronze time: " + time_Storage);
                Debug.Log(t);
                countdown_Text.enabled = false;
                current_score = current_score + 10;
                breakdown_Total = ((current_score + off_ground_time) * multiplyier);
                Debug.Log(breakdown_Total);

                score_breakdown.text = "Score Breakdown: \n" + " Score from Medal: " + current_score.ToString() + "\nScore From Airtime: " + off_ground_time.ToString() + "\nMultiplier: " + multiplyier.ToString() + "\nFinal Total: " + breakdown_Total.ToString();
                total_Score = total_Score + breakdown_Total;
                current_score = 0;
                off_ground_time = 0;
               // multiplyier = 1f;

            }
            else if (time_Storage > 50)
            {

                Debug.Log("Silver time: " + time_Storage);
                Debug.Log(t);
                countdown_Text.enabled = false;
                current_score = current_score + 40;
                breakdown_Total = ((current_score + off_ground_time) * multiplyier);
                Debug.Log(breakdown_Total);

                score_breakdown.text = "Score Breakdown: \n" + " Score from Medal: " + current_score.ToString() + "\nScore From Airtime: " + off_ground_time.ToString() + "\nMultiplier: " + multiplyier.ToString() + "\nFinal Total: " + breakdown_Total.ToString();
                total_Score = total_Score + breakdown_Total;
                current_score = 0;
                off_ground_time = 0;
               // multiplyier = 1f;
            }
            else if (time_Storage > 30)
            {

                Debug.Log("Gold time: " + time_Storage);
                Debug.Log(t);
                countdown_Text.enabled = false;
                current_score = current_score + 60;
                breakdown_Total = ((current_score + off_ground_time) * multiplyier);
                Debug.Log(breakdown_Total);

                score_breakdown.text = "Score Breakdown: \n" + " Score from Medal: " + current_score.ToString() + "\nScore From Airtime: " + off_ground_time.ToString() + "\nMultiplier: " + multiplyier.ToString() + "\nFinal Total: " + breakdown_Total.ToString();
                total_Score = total_Score + breakdown_Total;
                current_score = 0;
                off_ground_time = 0;
                //multiplyier = 1f;
            }
            else
            {

                Debug.Log("Platinum time: " + time_Storage);
                Debug.Log(t);
                countdown_Text.enabled = false;
                current_score = current_score + 100;
                breakdown_Total = ((current_score + off_ground_time) * multiplyier);
                Debug.Log(breakdown_Total);

                score_breakdown.text = "Score Breakdown: \n" + " Score from Medal: " + current_score.ToString() + "\nScore From Airtime: " + off_ground_time.ToString() + "\nMultiplier: " + multiplyier.ToString() + "\nFinal Total: " + breakdown_Total.ToString();
                total_Score = total_Score + breakdown_Total;
                current_score = 0;
                off_ground_time = 0;
                //multiplyier = 1f;
            }

        }







        //mouse countrol
        float mouseY = Input.GetAxis("Mouse Y") * mouse_Sens * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (is_Grounded)
        {
            speed = 10;
        }
        else if (is_Grounded = false && isWallRunning == true)
        {
            speed = 15f;
        }
        else if (is_Grounded == false)
        {
            speed = 18;
        }



        Grounded();

        //Wall running checks
        isWallRight = Physics.Raycast(transform.position, orientation.right, out wallHitR, 1f, whatIsWall);
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, out wallHitL, 1f, whatIsWall);

        if (!isWallLeft && !isWallRight) StopWallRun();

        if (Input.GetKey(KeyCode.D) && isWallRight) StartWallrun();
        if (Input.GetKey(KeyCode.A) && isWallLeft) StartWallrun();



        if (is_Grounded == true && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);




        //if wall runnning allow for double jump
        if (Input.GetButtonDown("Jump") && is_Grounded == true && jump_count > 0)
        {
            Debug.Log("jumping");
            Velocity.y = Mathf.Sqrt(jump_Height * -2f * gravity);
            jump_count = 0;
        }

        if (Input.GetButtonDown("Jump") && isWallRunning == true == jump_count > 0)
        {
            Velocity.y = Mathf.Sqrt(wall_Jump_Height * -2f * gravity);
            jump_count = 0;

        }


        Velocity.y += gravity * Time.deltaTime;

        controller.Move(Velocity * Time.deltaTime);

        //sprint 
        if (Input.GetButtonDown("Sprint") && is_Grounded)
        {
            Debug.Log("running");

            speed = 40f;
            sprint = true;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            Debug.Log("walking");

            sprint = false;
            speed = 20f;
        }

        //rotate camera when wall running reset if not
        player_Cam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, wallRunCameraTilt);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);

        if (Math.Abs(wallRunCameraTilt) < maxWallRunCameraTilt && isWallRunning && isWallRight)
            wallRunCameraTilt += Time.deltaTime * maxWallRunCameraTilt * 10;
        if (Math.Abs(wallRunCameraTilt) < maxWallRunCameraTilt && isWallRunning && isWallLeft)
            wallRunCameraTilt -= Time.deltaTime * maxWallRunCameraTilt * 10;

        if (wallRunCameraTilt > 0 && !isWallRight && !isWallLeft)
            wallRunCameraTilt -= Time.deltaTime * maxWallRunCameraTilt * 10;
        if (wallRunCameraTilt < 0 && !isWallRight && !isWallLeft)
            wallRunCameraTilt += Time.deltaTime * maxWallRunCameraTilt * 10;

    }

    private void StartWallrun()
    {
        rb.useGravity = false;
        gravity = -10f;
        jump_Height = 7.5f;
        isWallRunning = true;
        Debug.Log("wallrunning");

        if (rb.velocity.magnitude <= maxWallSpeed)
        {
            Debug.Log("WALL WUNIN");

            rb.AddForce(orientation.forward * wallrunForce * Time.deltaTime);
            //player sticks to wall
            if (isWallRight)
                rb.AddForce(orientation.right * wallrunForce / 5 * Time.deltaTime);
            else
                rb.AddForce(-orientation.right * wallrunForce / 5 * Time.deltaTime);
        }
    }

    private void StopWallRun()
    {
        jump_Height = 5f;
        gravity = -25f;
        jump_count = 1;
        isWallRunning = false;
        rb.useGravity = true;
    }



}
