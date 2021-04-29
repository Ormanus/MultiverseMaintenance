using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HubUIControl : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject endScreen;

    public void Restart()
    {
        SaveData.instance.Restart();
        SceneManager.LoadScene(0);
    }
}
