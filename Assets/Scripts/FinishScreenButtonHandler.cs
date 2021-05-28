using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScreenButtonHandler : MonoBehaviour
{
    private Button _finishScreenButton;
    private bool _isClicked = false;

    public bool IsClicked => _isClicked;
    void Start()
    {
        _finishScreenButton = GetComponent<Button>();
        _finishScreenButton.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        _isClicked = true;
    }

    private void OnDestroy()
    {
        _finishScreenButton.onClick.RemoveListener(ButtonClicked);
    }
}
