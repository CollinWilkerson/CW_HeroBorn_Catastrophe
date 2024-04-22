using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Behavior : MonoBehaviour
{
    public void ToCutscene()
    {
        SceneManager.LoadScene("Cutscene");
    }
}
