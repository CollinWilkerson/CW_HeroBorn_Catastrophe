using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Behavior : MonoBehaviour
{
    //brings in all of the ui elements I need to hide
    public TMP_Text BulletCounter;
    public Player_Behavior Player;
    public GameObject Silencer;
    public GameObject ActiveCamo;
    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;
    // Update is called once per frame
    void Update()
    {
        BulletCounter.text = "Ammo: " + Player.getAmmo().ToString();
        Silencer.SetActive(Player.getSilencer());
        switch (Player.getHealth())
        {
            case 0:
                Life1.SetActive(false);
                Life2.SetActive(false);
                Life3.SetActive(false);
                break;
            case 1:
                Life1.SetActive(true);
                Life2.SetActive(false);
                Life3.SetActive(false);
                break;
            case 2:
                Life1.SetActive(true);
                Life2.SetActive(true);
                Life3.SetActive(false);
                break;
            case 3:
                Life1.SetActive(true);
                Life2.SetActive(true);
                Life3.SetActive(true);
                break;
        }
    }
}
