using TMPro;
using UnityEngine;

public class Info : MonoBehaviour
{
    [SerializeField] private SpawnerCube _spawnerCubs;
    [SerializeField] private SpawnerBomb _spawnerBomb;
    [SerializeField] private SpawnerPlatform _spawnerPlatforms;

    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        SetSettingsText();
    }

    private void OnEnable()
    {
        _spawnerBomb.ChangedText += ShowText;
        _spawnerCubs.ChangedText += ShowText;
    }

    private void OnDisable()
    {
        _spawnerBomb.ChangedText -= ShowText;
        _spawnerCubs.ChangedText -= ShowText;
    }

    private void SetSettingsText()
    {
        _text.enableAutoSizing = true;
        _text.overflowMode = TextOverflowModes.Overflow;
        _text.color = Color.black;
    }

    private void ShowText()
    {
        _text.text = $"����� �����: {_spawnerCubs.AllObjects}\n" +
            $"��������: {_spawnerCubs.ActiveObjects}\n" +
            $"����������: {_spawnerCubs.InactiveObjects}\n" +
            $"����� ����: {_spawnerBomb.AllObjects}\n" +
            $"��������: {_spawnerBomb.ActiveObjects}\n" +
            $"����������: {_spawnerBomb.InactiveObjects}\n";
    }
}
