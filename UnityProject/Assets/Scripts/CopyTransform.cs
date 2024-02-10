using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform cpTransform;
    // Update is called once per frame
    void Update()
    {
        this.transform.SetParent(cpTransform);
    }
}
