using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardScript : MonoBehaviour
{
    public InputField TextField;
    public RectTransform EngLayoutSml, EngLayoutBig, SymbLayout;
    /*RusLayoutSml, RusLayoutBig,*/

    public void alphabetFunction(string alphabet)
    {
        TextField.text=TextField.text + alphabet;
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
}
