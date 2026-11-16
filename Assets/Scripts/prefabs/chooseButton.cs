using UnityEngine;
using UnityEngine.UI;
using System;

public class chooseButton : MonoBehaviour
{
    public Button button;
    public Text buttonText;

    private int id;
    private Action<int> onClick;

    public void Setup(int buttonId, string text, Action<int> callback)
    {
        id = buttonId;
        onClick = callback;

        if (button == null)
            button = GetComponent<Button>();

        if (buttonText == null)
            buttonText = GetComponentInChildren<Text>();

        if (button == null)
        {
            Debug.LogError("Brak komponentu Button!");
            return;
        }

        if (buttonText == null)
        {
            Debug.LogError("Brak komponentu Text!");
            return;
        }

        buttonText.text = text;

        button.onClick.AddListener(() =>
        {
            onClick?.Invoke(id);
        });
    }
}