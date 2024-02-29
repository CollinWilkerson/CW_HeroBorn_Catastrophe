using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clip_Behavior : MonoBehaviour
{
    public Player_Behavior player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Behavior>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //remove item
            Destroy(this.transform.gameObject);

            //Pickup Confirmation
            Debug.Log("Ammo Collected");
            //Player_Behavior player = new Player_Behavior(GameObject.Find("player").GetComponent<Player_Behavior>());
            player.addAmmo();
        }
    }
}
