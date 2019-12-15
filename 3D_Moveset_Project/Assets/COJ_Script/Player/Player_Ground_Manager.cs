using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ground_Manager : MonoBehaviour
{
    Player_Control player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("floor"))
        {
            player.isGrounded = true;
            player.playerAnim.SetBool("isJumping", false);
        }
    }
}
