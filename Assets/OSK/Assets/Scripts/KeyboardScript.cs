using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyboardScript : MonoBehaviour
{
    public InputField TextField;
    public RectTransform EngLayoutSml, EngLayoutBig, SymbLayout;

    public GameObject localGameManager;
    /*RusLayoutSml, RusLayoutBig,*/


    private void Start()
    {
        
    }

    private void OnEnable()
    {
        TextField.onValueChanged.AddListener(delegate { ValueChangeCheck();  });
    }

    public void alphabetFunction(string alphabet)
    {
        TextField.text=TextField.text + alphabet;
    }

    public void ValueChangeCheck()
    {
        ExecuteEvents.Execute<ICustomMessengerScript>(localGameManager, null, (x, y) => x.SetErrorMessageInactive());
        if (!SymbLayout.gameObject.activeSelf)
        {
             if (TextField.text.Length > 0)
             {
                 ShowLayout(EngLayoutSml);
             }
             else
             {
                 ShowLayout(EngLayoutBig);
             }
        }
    }

    public void SwapToLetterKeys()
    {
        if (TextField.text.Length > 0)
        {
            ShowLayout(EngLayoutSml);
        }
        else
        {
            ShowLayout(EngLayoutBig);
        }
    }



    public void BackSpace()
    {
        if (TextField.text.Length > 0) TextField.text = TextField.text.Remove(TextField.text.Length - 1);
    }

    public void CloseAllLayouts()
    {
        //RusLayoutSml.SetActive(false);
        //RusLayoutBig.SetActive(false);
        EngLayoutSml.gameObject.SetActive(false);
        EngLayoutBig.gameObject.SetActive(false);
        SymbLayout.gameObject.SetActive(false);
    }

    public void ShowLayout(RectTransform SetLayout)
    {
        CloseAllLayouts();
        SetLayout.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        TextField.onValueChanged.RemoveAllListeners();
    }
}
