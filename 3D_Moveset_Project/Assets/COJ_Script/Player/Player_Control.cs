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

    private CharacterController characterController;
    public float InputX, InputZ, p_Speed;
    Vector3 desiredMoveDirection;

    [SerializeField] float rotationSpeed = 0.3f;
    [SerializeField] float allowRotation = 0.1f;
    [SerializeField] float movementSpeed = 1.5f;
    [SerializeField] float gravity;
    [SerializeField] float gravityMultiplier;

    public Rigidbody playerRb;
    public Animator playerAnim;
    public CapsuleCollider playerCol;
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
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
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        InputDecider();
        MouvementManager();
        /*var z = Input.GetAxis("Vertical");
        var y = Input.GetAxis("Horizontal");
        Vector3 playerDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) ;
        Vector3 playerRotatDir = new Vector3(0f, Input.GetAxis("Horizontal"), 0f);
        playerAnim.SetFloat("speed", z);
        transform.Translate(playerDir * speed * Time.deltaTime, Space.Self);
        transform.Rotate(playerRotatDir * rotat_speed,Space.Self);

        if(Input.GetButtonDown("Jump") && isGrounded == true)
        {
            StartCoroutine(Jump());
            playerAnim.SetBool("isJumping", true);
            isGrounded = false;
            Debug.Log("jump");
        }

        if(z > 0/6 || z <-0.6)
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
           
        }*/
    }

  

    void InputDecider()
    {
        p_Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if(p_Speed > allowRotation)
        {
            RotationManager();
        }
        else
        {
            desiredMoveDirection = Vector3.zero;
        }
    }
    void RotationManager()
    {
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), rotationSpeed);
    }

    void MouvementManager()
    {
        gravity -= 9.8f * Time.deltaTime;
        gravity = gravity * gravityMultiplier;

        Vector3 moveDirection = desiredMoveDirection * (movementSpeed * Time.deltaTime);
        moveDirection = new Vector3(moveDirection.x, gravity, moveDirection.z);
        characterController.Move(moveDirection);

        if (characterController.isGrounded)
        {
            gravity = 0;
        }
    }
}
