using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Shard : MonoBehaviour
{
    public int level;

    bool collecting = false;

    public static Shard current;

    private void Awake()
    {
        current = this;
        if (level > 0)
            gameObject.SetActive(false);
    }

    public void Collect()
    {
        if (!collecting)
        {
            collecting = true;
            StartCoroutine(Animation());
        }

        current = null;
    }

    IEnumerator Animation()
    {
        float startScale = transform.localScale.x;
        float t = 1f;
        while (t > 0f)
        {
            float s = startScale * t;
            transform.localScale = new Vector3(s, s, s);
            t -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1f);

        if (SaveData.instance)
            SaveData.instance.Collect(level);

        SceneManager.LoadScene("HubScene");
    }
}
