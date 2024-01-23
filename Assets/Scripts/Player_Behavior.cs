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

    private float vInput;
    private float hInput;
    private float targetAngle;

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
        /* i have no clue what im doing */
        
        //sets up movement quantities for the rigidbody system
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        //Debug.Log(vInput);
        hInput = Input.GetAxis("Horizontal") * moveSpeed;
    }

    // use Fixedupdate for rigidbodies
    private void FixedUpdate()
    {
        //removes everything but the y component
        Vector3 YRot = new Vector3(0,cam.eulerAngles.y,0);
        Quaternion QYRot = Quaternion.Euler(YRot);
        //making rotation follow the rotation of the camera
        _rb.MoveRotation(QYRot);
        //Debug.Log("v3" + YRot + "Q" + QYRot.y);

        //Debug.Log("rb" + _rb.rotation.eulerAngles.y);
        //Debug.Log("cam" + cam.eulerAngles.y);

        // if you try to update one position then another the rb will only operate on the last one because of the frame call, so you have to consalidate movements
        //_rb.MovePosition(this.transform.position + this.transform.right * hInput * Time.fixedDeltaTime + this.transform.forward * vInput * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.right * hInput * Time.fixedDeltaTime + this.transform.forward * vInput * Time.fixedDeltaTime);
    }
}
