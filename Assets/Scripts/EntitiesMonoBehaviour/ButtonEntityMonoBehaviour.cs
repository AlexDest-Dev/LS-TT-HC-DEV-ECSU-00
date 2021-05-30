using System;
using System.Collections;
using System.Collections.Generic;
using EntitiesMonoBehaviour;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEntityMonoBehaviour : EcsEntityMonoBehaviour
{
    private Button _button;
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        _ecsEntity.Get<Clicked>();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClicked);
    }
}
