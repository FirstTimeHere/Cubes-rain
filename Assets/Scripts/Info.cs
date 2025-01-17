using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Info : MonoBehaviour
{
    [SerializeField] private SpawnerCubs _spawnerCubs;
    [SerializeField] private SpawnerBombs _spawnerBomb;
    [SerializeField] private SpawnerPlatforms _spawnerPlatforms;

    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        GetSettingsText();
    }

    private void OnEnable()
    {
        _spawnerCubs.Created += GetText;
        _spawnerBomb.Created += GetText;
        _spawnerPlatforms.Created += GetText;
    }

    private void OnDisable()
    {
        _spawnerCubs.Created -= GetText;
        _spawnerBomb.Created -= GetText;
        _spawnerPlatforms.Created -= GetText;
    }

    private void GetSettingsText()
    {
        _text.enableAutoSizing = true;
        _text.overflowMode = TextOverflowModes.Overflow;
        _text.color = Color.black;
    }

    private void GetText()
    {
        int sum = _spawnerCubs.AllObjecctsCreate + _spawnerBomb.AllObjecctsCreate + _spawnerPlatforms.AllObjecctsCreate;
        _text.text = $"Всего объектов: {sum}\n" +
            $"Кубов: {_spawnerCubs.AllObjecctsCreate}\n" +
            $"Платформ: {_spawnerPlatforms.AllObjecctsCreate}\n" +
            $"Бомб: {_spawnerBomb.AllObjecctsCreate} \n";
    }
}
