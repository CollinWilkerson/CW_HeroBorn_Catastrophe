using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player_Behavior : MonoBehaviour
{
    //initial variables
    public float moveSpeed = 100f;

    //camera reference
    public Transform cam;

    //animator reference
    public Animator animator;

    private Vector3 vInput;
    private Vector3 hInput;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        // this gets rid of the cursor for camera movement purposes
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _rb = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        //animation stuff
        //takes parameter name and value 
        animator.SetFloat("velocityZ", Input.GetAxis("Vertical"));
        animator.SetFloat("velocityX", Input.GetAxis("Horizontal"));
        //sets up movement quantities for the rigidbody system
        vInput = this.transform.forward * Input.GetAxis("Vertical");
        //Debug.Log(vInput);
        hInput = this.transform.right * Input.GetAxis("Horizontal");

        //removes everything but the y component
        Vector3 YRot = new Vector3(0, cam.eulerAngles.y, 0);
        Quaternion QYRot = Quaternion.Euler(YRot);
        //making rotation follow the rotation of the camera
        _rb.MoveRotation(QYRot);
        //Debug.Log("v3" + YRot + "Q" + QYRot.y);

        //Debug.Log("rb" + _rb.rotation.eulerAngles.y);
        //Debug.Log("cam" + cam.eulerAngles.y);

        // if you try to update one position then another the rb will only operate on the last one because of the frame call, so you have to consalidate movements
        _rb.MovePosition(this.transform.position + (hInput + vInput).normalized * moveSpeed * Time.fixedDeltaTime);
    }

    // use Fixedupdate for rigidbodies
    private void FixedUpdate()
    {

    }
}
