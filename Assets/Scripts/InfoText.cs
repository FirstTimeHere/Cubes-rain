using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoText<T> where T : Component
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
        _text.text = "Сколько раз вызван Instantiate: {spawner.AllObjects}\n" +
           $"Всего объектов/вызовов GET: {spawner.InstantiateObjects}\n" +
           $"Активных объектов: {spawner.ActiveObjects}\n\n";

        //spawner.ChangedText -= ShowText;
        //специально оставил, так как не знаю где отписываться от ивента
        //если отписаться сейчас, то больше он не подпишется (по понятным причина ведь конструктор один раз вызовиться)
    }
    private void SetSettingsText(TextMeshProUGUI text)
    {
        text.enableAutoSizing = true;
        text.overflowMode = TextOverflowModes.Overflow;
        text.color = Color.black;
    }
}
