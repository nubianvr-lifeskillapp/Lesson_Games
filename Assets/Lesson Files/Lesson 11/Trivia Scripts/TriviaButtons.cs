using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriviaButtons : MonoBehaviour
{
    [HideInInspector]
    public Button btn;
    [HideInInspector]
    public Image btnImage;
    [HideInInspector]
    public TMP_Text textComponent;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btnImage = GetComponent<Image>();
        textComponent = GetComponentInChildren<TMP_Text>();
    }

    public void SetButtonActive(bool boolValue)
    {
        this.gameObject.SetActive(boolValue);
    }

}
