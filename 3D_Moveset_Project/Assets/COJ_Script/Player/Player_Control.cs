using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public bool isGrounded;
    public bool isRunning;

    public float speed;
    public float run_speed;
    public float walk_speed;
    public float sprint_speed;
    public float rotat_speed;
    public float rotat_run_speed;
    public float rotat_sprint_speed;
    public float jumpHeight;
    public float jumpGravity;
    float timeBeforeSprint = 0;

    public Rigidbody playerRb;
    public Animator playerAnim;
    public CapsuleCollider playerCol;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = transform.GetChild(0).GetComponent<Animator>();
        playerCol = GetComponent<CapsuleCollider>();
        isGrounded = true;
        speed = run_speed;
        rotat_speed = rotat_run_speed;
    }

    // Update is called once per frame
    void Update()
    {
        var z = Input.GetAxis("Vertical");
        var y = Input.GetAxis("Horizontal2");
        var x = Input.GetAxis("Horizontal");

        playerAnim.SetFloat("speed", z);
        transform.Translate(x * speed, 0, z * speed);
        transform.Rotate(0, y * rotat_speed, 0);

        if(Input.GetButtonDown("Jump") && isGrounded == true)
        {
            StartCoroutine(Jump());
            playerAnim.SetBool("isJumping", true);
            isGrounded = false;
            Debug.Log("jump");
        }

        if(z > 0.6 || x > 0.6)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (isRunning == true)
        {
            
            timeBeforeSprint += Time.deltaTime;
            if(timeBeforeSprint > 4f)
            {
                speed = sprint_speed;
                rotat_speed = rotat_sprint_speed;
                playerAnim.SetBool("isSprinting", true);
            }
        }
        else
        {
            speed = run_speed;
            rotat_speed = rotat_run_speed;
            playerAnim.SetBool("isSprinting", false);
            timeBeforeSprint = 0;
        }

        IEnumerator Jump()
        {
            if(isRunning == true)
            {
                playerRb.AddForce(0, jumpHeight, 0);
                yield return new WaitForSeconds(0.3f);
                playerRb.AddForce(0, -jumpGravity, 0);
            }
            else if(isRunning == false)
            {
                yield return new WaitForSeconds(0.5f);
                playerRb.AddForce(0, jumpHeight, 0);
                yield return new WaitForSeconds(0.3f);
                playerRb.AddForce(0, -jumpGravity, 0);
            }
           
        }
    }
}
