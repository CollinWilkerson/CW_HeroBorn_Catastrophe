using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door_Behavior : MonoBehaviour
{
    public Player_Behavior player;
    private bool isActive = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" && isActive)
        {
            player.damage();
        }
        else if (collision.gameObject.name == "Player" && !isActive)
        {
            SceneManager.LoadScene(2);
        }

    }
    
}
