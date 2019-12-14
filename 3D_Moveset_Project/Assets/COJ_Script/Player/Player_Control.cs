using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public bool isGrounded;

    public float speed;
    public float run_speed;
    public float walk_speed;
    public float sprint_speed;
    public float rotat_speed;
    public float jumpHeight;

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
    }

    // Update is called once per frame
    void Update()
    {
        var z = Input.GetAxis("Vertical") * speed;
        var y = Input.GetAxis("Horizontal2") * rotat_speed;
        var x = Input.GetAxis("Horizontal") * speed;

        transform.Translate(x, 0, z);
        transform.Rotate(0, y, 0);

        if(Input.GetButtonDown("Jump") && isGrounded == true)
        {
            playerRb.AddForce(0, jumpHeight, 0);
            isGrounded = false;
            Debug.Log("jump");
        }
    }
}
