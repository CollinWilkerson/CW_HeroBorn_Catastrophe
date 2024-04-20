using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//based on a tutorial by SpeedTutor - https://www.youtube.com/watch?v=SeXdXvtunNY
public class PP_Behavior : MonoBehaviour
{
    [SerializeField] private Volume postProfileVolume;
    [SerializeField] private bool disable;

    [Header("Post Processing Profiles")]
    [SerializeField] private VolumeProfile postProfileMain;
    [SerializeField] private VolumeProfile postProfileSecondary;

    public void MainPostProcess()
    {
        postProfileVolume.profile = postProfileMain;
    }

    public void SecondaryPostProcess()
    {
        postProfileVolume.profile = postProfileSecondary;
    }

}
