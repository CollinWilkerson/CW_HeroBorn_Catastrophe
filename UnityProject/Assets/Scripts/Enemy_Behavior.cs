using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior : MonoBehaviour
{
    public Player_Behavior player;
    public bool inRange = false;
    public bool detected = false;
    //triggers when a gameobject enters the sphere
    void OnTriggerEnter(Collider other)
    {
        //checks game object name
        if (other.name == "Player")
        {
            Debug.Log("Player detected - Listening");
            inRange = true;
        }
    }

    //triggers when gameobject leaves the sphere
    void OnTriggerExit(Collider other)
    {
        //does same thing as before
        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
            inRange = false;
        }
    }
    private void Update()
    {
        if(inRange && Input.GetMouseButtonDown(0) && !player.getSilencer())
        {
            detected = true;
            Debug.Log("Player Detected");
        }
    }
}
