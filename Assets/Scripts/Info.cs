using TMPro;
using UnityEngine;

public class Info<T> : MonoBehaviour where T : Component
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        SetSettingsText();
    }

    public Info(T showInstantate,T showGet,T showAcveObjects)
    {
        _instantiate = showInstantate;
        _showGet = showGet;
        _activeObjects = showAcveObjects;
    }
    private void SetSettingsText()
    {
        _text.enableAutoSizing = true;
        _text.overflowMode = TextOverflowModes.Overflow;
        _text.color = Color.black;
    }

    public void ShowText()
    {
        _text.text = $"������� ��� ������ Instantiate: {_instantiate}\n" +
            $"����� ��������/������� GET: {_showGet}\n" +
            $"�������� ��������: {_activeObjects}\n\n";
    }
}
