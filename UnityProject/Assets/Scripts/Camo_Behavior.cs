using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camo_Behavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //remove item
            Destroy(this.transform.gameObject);

            //Pickup Confirmation
            Debug.Log("Camo Aquired - Enimes can't detect you while crouched");
        }
    }
}
