using TMPro;
using UnityEngine;

public class InfoText<T> : MonoBehaviour where T : Component
{
    private TextMeshProUGUI _text;

    public InfoText(Spawner<T> spawner, TextMeshProUGUI text)
    {
        SetSettingsText(text);

        _text = text;
        spawner.ChangedText += ShowText;
    }

    private void ShowText(Spawner<T> spawner)
    {
        _text.text = $"������� ��� ������ Instantiate: {spawner.InstantiateObjects}\n" +
           $"����� ��������/������� GET: {spawner.AllObjects}\n" +
           $"�������� ��������: {spawner.ActiveObjects}\n\n";

        //spawner.ChangedText -= ShowText;
    }

    private void SetSettingsText(TextMeshProUGUI text)
    {
        text.enableAutoSizing = true;
        text.overflowMode = TextOverflowModes.Overflow;
        text.color = Color.black;
    }
}
