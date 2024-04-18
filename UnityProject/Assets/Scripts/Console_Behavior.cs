using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console_Behavior : MonoBehaviour
{
    public GameObject text;
    private bool listen = false;
    private bool active = true;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player" && active)
        {
            Debug.Log("In range");
            listen = true;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Debug.Log("Player out of range");
            listen = false;
            text.SetActive(false);
        }
    }

    private void Update()
    {
        if (listen)
        {
            if(Input.GetKeyDown(KeyCode.E)) 
            {
                GetComponent<AudioSource>().Play();
                text.SetActive(false);
                active = false;
            }
        }
    }

    public bool getActive()
    {
        return active;
    }
}
