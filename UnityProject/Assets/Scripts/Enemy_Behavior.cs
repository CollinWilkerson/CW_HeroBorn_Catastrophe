using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Behavior : MonoBehaviour
{
    public Player_Behavior player;
    //gives the enemy a loop to follow
    [Range(0,5)]
    public int LoopID = 0;
    public Canvas detectionUI;

    //holds positions to patrol to
    private Vector3[] route = new Vector3[4];
    private NavMeshAgent agent;
    private bool inRange = false;
    public bool detected = false;
    private bool chase = false;
    public float activeDetection = 0f;
    private int position = 0;
    private int lives = 3;
    //triggers when a gameobject enters the sphere

    //All of my visual detection variables from https://www.youtube.com/watch?v=j1-OyLo77ss
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

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
        playerRef = GameObject.FindGameObjectWithTag("Player");  //from tutorial
        //starts the coroutine that runs slower than update for performance reasons
        StartCoroutine(FOVRoutine());
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
            activeDetection += 0.5f;
            detected = true;
            Debug.Log("Player Detected");
        }
        //calculates new patrol location if the enemy is close enough to their destination
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
        if (detected && activeDetection <= 1)
        {
            activeDetection += 0.4f * Time.deltaTime;
        }
        else if(!detected && activeDetection >= 0)
        {
            activeDetection -= 0.2f * Time.deltaTime;
        }
        if (activeDetection < 0)
            detectionUI.gameObject.SetActive(false);
        else
            detectionUI.gameObject.SetActive(true);
        //if the player is detected all the way the enemy will chase them, once the player looses all detection the enemy will leave them alone
        if(activeDetection >= 1.0f)
            chase = true;
        else if(activeDetection <= 0.0f)
            chase = false;
        
        
    }

    // coroutine runs less often than update for performance reasons
    private IEnumerator FOVRoutine()
    {
        //how often the coroutine runs
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FeildOfViewCheck();
        }
    }

    private void FeildOfViewCheck()
    {
        //this is what actually looks for the player
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        //If anything is in our array it has picked up our player
        if (rangeChecks.Length != 0)
        {
            //the only thing in the targetmask is the player, so we use the first index
            Transform target = rangeChecks[0].transform;
            //establishes direction to enemy rotation to player location
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //gets the angle between the forward direction and the normalized vector to the target and compares it to half the angle we established in the beginning.
            //the angle is halved because half of the angle is to the left and half is to the right
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                //starts raycast from center of enemy, toward the player, from the distance to the player, only checking objects in the obstructionMask
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    detected = true;
                    agent.destination = playerRef.transform.position;
                }
                else
                    detected = false;
            }
            else
                //this could cause problems later
                detected = false;
        }
        //if the player is detected and leaves the detection range then they are no longer being detected
        else if (detected)
            detected = false;
        
        //chases the player if the detection is maxed out.
        if (chase)
        {
            agent.destination = playerRef.transform.position;
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

    public void damage()
    {
        Debug.Log("enemy hit");
        lives -= 1;
        if (lives == 0)
        {
            Destroy(gameObject);
        }
    }
}
