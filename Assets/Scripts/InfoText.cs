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
        _text.text = $"Сколько раз вызван Instantiate: {spawner.InstantiateObjects}\n" +
           $"Всего объектов/вызовов GET: {spawner.AllObjects}\n" +
           $"Активных объектов: {spawner.ActiveObjects}\n\n";

        //spawner.ChangedText -= ShowText;
    }

    private void SetSettingsText(TextMeshProUGUI text)
    {
        text.enableAutoSizing = true;
        text.overflowMode = TextOverflowModes.Overflow;
        text.color = Color.black;
    }
}
