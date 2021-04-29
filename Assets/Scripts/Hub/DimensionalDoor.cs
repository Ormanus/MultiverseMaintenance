using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DimensionalDoor : MonoBehaviour, IPointerClickHandler
{
    public string levelName;
    public EnterLevelTransition transition;

    public void OnClick()
    {
        if (!EnterLevelTransition.enteringLevel)
            transition.EnterLevel(this);
    }

    public void EnterLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }
}
