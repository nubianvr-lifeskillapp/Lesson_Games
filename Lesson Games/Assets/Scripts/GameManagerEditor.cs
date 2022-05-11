using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private void OnEnable()
    {
        var myScript = target as GameManager;
        //myScript.m_ShowExtraFields = new UnityEditor.AnimatedValues.AnimBool(true);
        //myScript.m_ShowExtraFields.valueChanged.AddListener(Repaint);
    }
}
