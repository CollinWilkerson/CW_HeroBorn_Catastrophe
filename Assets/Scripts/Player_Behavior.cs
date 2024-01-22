using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player_Behavior : MonoBehaviour
{
    //initial variables
    public float moveSpeed = 100f;

    private float vInput;
    private float hInput;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        /* i have no clue what im doing */
        //sets up movement quantities for the rigidbody system
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * moveSpeed;
    }

    // use Fixedupdate for rigidbodies
    private void FixedUpdate()
    {
        //vertical movement
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        //horizontal movement
        _rb.MovePosition(this.transform.position + this.transform.forward * hInput * Time.fixedDeltaTime);
    }
}
