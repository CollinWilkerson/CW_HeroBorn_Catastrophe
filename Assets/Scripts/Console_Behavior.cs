using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console_Behavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("In range");

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Debug.Log("Player out of range");
        }
    }
}
