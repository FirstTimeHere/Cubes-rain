using TMPro;
using UnityEngine;

public class InfoText<T> : MonoBehaviour where T : Component
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        SetSettingsText(_text);
    }

    public virtual void ShowText(Spawner<T> spawner)
    {
        _text.text = $"������� ��� ������ Instantiate: {spawner.InstantiateObjects}\n" +
           $"����� ��������/������� GET: {spawner.AllObjects}\n" +
           $"�������� ��������: {spawner.ActiveObjects}\n\n";
    }

    private void SetSettingsText(TextMeshProUGUI text)
    {
        text.enableAutoSizing = true;
        text.overflowMode = TextOverflowModes.Overflow;
        text.color = Color.black;
    }
}
