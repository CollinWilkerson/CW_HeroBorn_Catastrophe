using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cutscene_Behavior : MonoBehaviour
{
    [SerializeField] GameObject dialougeBox;
    [SerializeField] TMP_Text dialouge;
    [SerializeField] GameObject text;
    [SerializeField] Image catmando;
    [SerializeField] Sprite catOpen;
    [SerializeField] Sprite catClosed;
    [SerializeField] Image boss;
    [SerializeField] Sprite bossOpen;
    [SerializeField] Sprite bossClosed;

    float time = 0;
    bool active = false;

    [SerializeField] string[] script;
    int line = -1;

    // Update is called once per frame
    void Update()
    {
        if (time < 0.5)
        {
            time += (1 * Time.deltaTime);
        }
        else if (!active)
        {
            active = true;
            dialougeBox.SetActive(true);
            text.SetActive(true);
        }
    }
    public void DialougeAdvance()
    {
        if (active)
        {
            Debug.Log("line: " + line);
            line++;
            if (line < script.Length)
            {
                dialouge.text = script[line];
                if(line == 0 || line == 4 || line == 9)
                {
                    boss.sprite = bossClosed;
                    catmando.sprite = catOpen;
                }
                else
                {
                    boss.sprite = bossOpen;
                    catmando.sprite = catClosed;
                }
            }
            else
            {
                SceneManager.LoadScene("Level1");
            }
        }
    }
}
