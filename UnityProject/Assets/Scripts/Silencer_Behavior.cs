using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silencer_Behavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //remove item
            Destroy(this.transform.gameObject);

            //Pickup Confirmation
            Debug.Log("Silencer collected - Next shot won't alert enemies!");
        }
    }
}
