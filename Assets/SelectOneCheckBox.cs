using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VRQuestionnaireToolkit;


public class SelectOneCheckBox : MonoBehaviour
{
    private GameObject _vrQuestionnaireFactory;
    private PageFactory _pageFactory;
    public bool startStuff = false;
    Toggle m_Toggle;

    void Start()
    {
        //init necessary relationships
        _vrQuestionnaireFactory = GameObject.FindGameObjectWithTag("QuestionnaireFactory");
        _pageFactory = _vrQuestionnaireFactory.GetComponent<PageFactory>();
        m_Toggle = GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });
    }

    // private void OnDisable()
    // {
    //     m_Toggle.onValueChanged.RemoveAllListeners();
    // }

    public void ToggleValueChanged(Toggle change)
    {
        SelectOneBox();
        //m_Toggle.isOn = true;
    }
    public void SelectOneBox()
    {
  
        for (int i = 0; i < _pageFactory.QuestionList.Count; i++)
        {
            if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>() != null)
            {
                if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>()
                    .CheckboxList.Any(checkbox => checkbox.GetComponentInChildren<Toggle>().isOn))
                {
                    foreach (var checkbox in _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>()
                        .CheckboxList)
                    {
                        if (checkbox.GetComponentInChildren<Toggle>() == this.gameObject.GetComponent<Toggle>())
                        {
                            checkbox.GetComponentInChildren<Toggle>().Select();
                        }
                        else
                        {
                            checkbox.GetComponentInChildren<Toggle>().isOn = false;
                        }

                    }
                }
                
            }
        }

    }
}
