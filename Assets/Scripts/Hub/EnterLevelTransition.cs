using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterLevelTransition : MonoBehaviour
{
    public static bool enteringLevel = false;

    public Image image;
    public float transitionLength = 0.5f;
    public Color color;

    DimensionalDoor door;

    private void Awake()
    {
        Color alphaless = color;
        alphaless.a = 0f;
        image.color = alphaless;
    }

    public void EnterLevel(DimensionalDoor door)
    {
        enteringLevel = true;
        this.door = door;
        StartCoroutine(transition());
    }

    IEnumerator transition()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            Color c = image.color;
            c.a = t;
            image.color = c;
        }

        {
            Color c = image.color;
            c.a = 1f;
            image.color = c;
        }


        enteringLevel = false;
        door.EnterLevel();
    }
}
