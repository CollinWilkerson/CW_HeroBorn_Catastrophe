using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Behavior : MonoBehaviour
{
    //initial variables
    public float moveSpeed = 1f;
    private float crouchSpeed;
    public float bulletSpeed = 10f;

    //camera reference
    public Transform cam;

    //animator reference. allows us to pass the animator in from the catmando mesh
    public Animator animator;

    public GameObject bullet;
    
    //fetch box colider
    private BoxCollider plrCollider;

    private Vector3 vInput;
    private Vector3 hInput;

    private Rigidbody _rb;

    private int bullets = 0;
    private bool shoot = false;
    private bool isSilenced = false;
    private int currentHealth = 3;
    // Start is called before the first frame update
    void Start()
    {
        // this gets rid of the cursor for camera movement purposes
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //establishes rigidbody component attached to the gameObject 
        _rb = GetComponent<Rigidbody>();
        //establishes box collider component attached to the gameObject
        plrCollider = GetComponent<BoxCollider>();
        crouchSpeed = moveSpeed / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //crouching - changes the size of the box collider based on the crouched state
        if (Input.GetKey("space"))
        {
            plrCollider.size = new Vector3(0.2f, 0.3f, 0.2f);
            plrCollider.center = new Vector3(0f, 0.15f, 0f);
            moveSpeed = crouchSpeed;
        }
        else
        {
            plrCollider.size = new Vector3(0.2f, 0.45f, 0.2f);
            plrCollider.center = new Vector3(0f, 0.22f, 0f);
            moveSpeed = crouchSpeed * 2;
        }

        //animation stuff
        //takes parameter name and value
        animator.SetBool("isCrouched", Input.GetKey("space"));
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
        _rb.MovePosition(this.transform.position + (hInput + vInput).normalized * moveSpeed);

        if (bullets > 0 && Input.GetMouseButtonDown(0))
        {
            shoot = true;
        }
    }

    private void FixedUpdate()
    {
        if (shoot)
        {
            //instantiate creates a new bullet based on the object we pass
            GameObject newBullet = Instantiate(bullet, this.transform.position + (this.transform.right/5), this.transform.rotation) as GameObject;
            //gets the new bullet rigid body
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            //sets the bullet direction to the players heading and gives it a magnitude of the bullet speed
            bulletRB.velocity = this.transform.forward * bulletSpeed;
            shoot = false;
            bullets -= 1;
            if (isSilenced)
            {
                isSilenced = false;
            }
        }
    }

    public void addAmmo()
    {
        bullets += 100;
        Debug.Log("Got Bullets");
    }

    public int getAmmo()
    {
        return bullets;
    }

    public void addSilencer()
    {
        isSilenced = true;
        Debug.Log("Silencer Equiped");
    }

    public bool getSilencer()
    {
        return isSilenced;
    }

    public int getHealth()
    {
        return currentHealth;
    }

    public void damage()
    {
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            Debug.Log("player died");
            SceneManager.LoadScene(1);
        }
    }
}
