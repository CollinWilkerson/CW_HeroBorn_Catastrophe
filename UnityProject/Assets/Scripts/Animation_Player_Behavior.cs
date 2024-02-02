using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Player_Behavior : MonoBehaviour
{
    public Animator animator;

    private float zVel;
    private float yVel;
    private float isCrouch;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("", zVel);
    }
}
