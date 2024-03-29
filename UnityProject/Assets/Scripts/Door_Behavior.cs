using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door_Behavior : MonoBehaviour
{
    public Player_Behavior player;
    public Console_Behavior security;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" && security.getActive())
        {
            player.damage();
        }
        else if (collision.gameObject.name == "Player" && !security.getActive())
        {
            SceneManager.LoadScene(3);
        }

    }
    
}
