using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI buttonText;
    private Color originalColor;
    public Color[] HighlightColor;

    Animator ani;
    void Start()
    {
        // Get the Text component of the button
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        ani = GetComponent<Animator>();

        // Store the original color of the button text
        originalColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the color of the button text to the highlight color
        int randomIndex = Random.Range(0, HighlightColor.Length);
        buttonText.color = HighlightColor[randomIndex];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the color of the button text to the original color
        buttonText.color = originalColor;
    }
}
