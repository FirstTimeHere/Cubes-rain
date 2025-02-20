using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Info : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _listText;

    private void Awake()
    {
        SetSettingsText(_listText);
    }

    private void SetSettingsText(List<TextMeshProUGUI> text)
    {
        for (int i = 0; i < text.Count; i++)
        {
            text[i].enableAutoSizing = true;
            text[i].overflowMode = TextOverflowModes.Overflow;
            text[i].color = Color.black;
        }
    }
}
