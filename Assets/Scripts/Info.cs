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
        GetSettingsText();
    }

    private void Update()
    {
        ShowText();
    }

    private void GetSettingsText()
    {
        _text.enableAutoSizing = true;
        _text.overflowMode = TextOverflowModes.Overflow;
        _text.color = Color.black;
    }

    private void ShowText()
    {
        _text.text = $"Всего кубов: {_spawnerCubs.AllObjects}\n" +
            $"Активных: {_spawnerCubs.ActiveObjects}\n" +
            $"Неактивных: {_spawnerCubs.InactiveObjects}\n" +
            $"Всего бомб: {_spawnerBomb.AllObjects}\n" +
            $"Активных: {_spawnerBomb.ActiveObjects}\n" +
            $"Неактивных: {_spawnerBomb.InactiveObjects}\n";
    }
}
