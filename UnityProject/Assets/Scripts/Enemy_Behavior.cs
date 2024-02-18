using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Behavior : MonoBehaviour
{
    public Player_Behavior player;
    //gives the enemy a loop to follow
    public int LoopID = 0;

    //holds positions to patrol to
    private Vector3[] route = new Vector3[4];
    private NavMeshAgent agent;
    private bool inRange = false;
    private bool detected = false;
    private int position = 0;
    private int lives = 3;
    //triggers when a gameobject enters the sphere

    private void Start()
    {
        //contains the 6 possible patrol routes for the level.
        switch (LoopID){
            //character spawn loop F2
            case 0:
                route = new Vector3[]{new Vector3(0f, 2f, 7f), new Vector3(0f, 2f, -7f), new Vector3(-11.5f, 2f, -7f), new Vector3(-11.5f, 2f, 7f)};
                break;
            //far end loop F2
            case 1:
                route = new Vector3[] { new Vector3(11.5f, 2f, -7f), new Vector3(11.5f, 2f, 7f), new Vector3(0f, 2f, 7f), new Vector3(0f, 2f, -7f) };
                break;
            //Middle Isle F1
            case 2:
                route = new Vector3[] { new Vector3(0f, 0f, 8f), new Vector3(2f, 0f, 0f), new Vector3(0f, 0f, -8f), new Vector3(-2f, 0f, 0f) };
                break;
            // right of the door F1
            case 3:
                route = new Vector3[] { new Vector3(12f, 0f, -1f), new Vector3(11f, 0f, -7.5f), new Vector3(4.5f, 0f, -7.5f), new Vector3(6f, 0f, -2f) };
                break;
            // left of door F1
            case 4:
                route = new Vector3[] { new Vector3(12f, 0f, 1f), new Vector3(11f, 0f, 6f), new Vector3(5f, 0f, 8f), new Vector3(6f, 0f, 3f) };
                break;
            // below spawn F1
            case 5:
                route = new Vector3[] { new Vector3(-10f, 0f, -6f), new Vector3(-5.5f, 0f, -6f), new Vector3(-6f, 0f, 5f), new Vector3(-11f, 0f, 6f) };
                break;
        }
        agent = GetComponent<NavMeshAgent>();
        MoveToNextPatrolLocation();
    }
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
    //damages the player on contact
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player.damage();
        }
    }

    private void Update()
    {
        //checks if the player shoots in the enemy's listening range
        if(inRange && Input.GetMouseButtonDown(0) && !player.getSilencer())
        {
            detected = true;
            Debug.Log("Player Detected");
        }
        //calculates new patrol location if the enemy is close enough to their destination
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (route.Length == 0)
            return;
        //takes a vector3
        agent.destination = route[position];
        position = (position + 1) % route.Length;
    }
}
