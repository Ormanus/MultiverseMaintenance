using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlPrompt : MonoBehaviour, IPointerClickHandler
{
    float opacity = 10f;

    Image img;

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        img = GetComponent<Image>();
        if (SaveData.saveData != null)
        {
            Destroy(gameObject);
        }
        Time.timeScale = 0.2f;
    }

    private void Update()
    {
        img.color = new Color(1f, 1f, 1f, Mathf.Clamp01(opacity));
        opacity -= Time.deltaTime * 5f;
        if (opacity < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
