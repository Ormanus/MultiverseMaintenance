using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Diamond : MonoBehaviour
{
    public GameObject[] shards;
    public GameObject diamond;
    public HubUIControl UIControl;

    void Start()
    {
        diamond.SetActive(false);

        for (int i = 0; i < shards.Length; i++)
        {
            shards[i].SetActive(SaveData.instance.isCollected(i));
        }

        if (SaveData.instance.allCollected())
        {
            StartCoroutine(WinCutScene());
        }
    }

    IEnumerator WinCutScene()
    {
        DontDestroyOnLoad(gameObject);

        yield return new WaitForSeconds(1f);

        float t = 0f;

        Vector2[] startPositions = new Vector2[shards.Length];
        for (int i = 0; i < shards.Length; i++)
        {
            startPositions[i] = shards[i].transform.position;
        }

        while (t < 1f)
        {
            t += Time.deltaTime;
            for (int i = 0; i < shards.Length; i++)
            {
                shards[i].transform.position = Vector2.Lerp(startPositions[i], diamond.transform.position, t * t);
            }
            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < shards.Length; i++)
        {
            shards[i].SetActive(false);
        }

        diamond.SetActive(true);

        yield return new WaitForSeconds(1f);

        UIControl.endScreen.SetActive(true);
    }
}
