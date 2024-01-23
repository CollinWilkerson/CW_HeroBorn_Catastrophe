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
            Destroy(this.transform.parent.gameObject);

            //Pickup Confirmation
            Debug.Log("Silencer Collected");
        }
    }
}
