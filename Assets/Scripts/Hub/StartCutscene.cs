using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StartCutscene : MonoBehaviour
{
    public HubUIControl UIControl;

    float opacity = 5f;

    Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
        if (SaveData.saveData != null)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        img.color = new Color(1f, 1f, 1f, Mathf.Clamp01(opacity));
        opacity -= Time.deltaTime * 5f;
        if (opacity < 0f)
        {
            Destroy(gameObject);
            UIControl.startScreen.SetActive(true);
        }
    }
}
