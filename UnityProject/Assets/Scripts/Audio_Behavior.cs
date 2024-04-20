using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Behavior : MonoBehaviour
{
    [SerializeField] AudioClip musicTwoClip;

    private bool musicSwitched = false;
    private Console_Behavior console;
    private void Start()
    {
        console = GameObject.FindFirstObjectByType<Console_Behavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!musicSwitched && !console.getActive())
        {
            musicSwitched = true;
            AudioSource audioSouce = GetComponent<AudioSource>();
            audioSouce.Stop();
            audioSouce.PlayOneShot(musicTwoClip);
        }
    }
}
