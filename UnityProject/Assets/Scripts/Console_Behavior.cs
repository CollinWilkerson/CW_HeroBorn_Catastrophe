using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console_Behavior : MonoBehaviour
{
    bool listen = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("In range");
            listen = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Debug.Log("Player out of range");
            listen = false;
        }
    }

    private void Update()
    {
        if (listen)
        {
            if(Input.GetKeyDown(KeyCode.E)) 
            {
                
            }
        }
    }
}
