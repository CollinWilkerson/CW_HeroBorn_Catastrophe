using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public GameObject player;
    public Enemy_Behavior enemy;
    public Image mask;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(player.transform);
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        mask.fillAmount = enemy.activeDetection;
    }
}
