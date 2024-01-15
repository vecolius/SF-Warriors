using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buttonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button button;
    Color originalColor;
    void Start()
    {
        button = GetComponent<Button>();
        originalColor = button.colors.normalColor;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = Color.yellow;
        button.colors = colors;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //ColorBlock colors = button.colors;
        //colors.normalColor = originalColor;
        //button.colors = colors;
    }
}
