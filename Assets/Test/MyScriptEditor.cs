/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(MyScript))]
public class MyScriptEditor : Editor
{
    private void OnEnable()
    {
        var myScript = target as MyScript;
        myScript.m_ShowExtraFields = new UnityEditor.AnimatedValues.AnimBool(true);
        myScript.m_ShowExtraFields.valueChanged.AddListener(Repaint);
    }
    override public void OnInspectorGUI()
     {
         var myScript = target as MyScript;
        myScript.m_ShowExtraFields.target = EditorGUILayout.ToggleLeft("Show extra fields",myScript.m_ShowExtraFields.target);
        //Extra block that can be toggled on and off.
        if (EditorGUILayout.BeginFadeGroup(myScript.m_ShowExtraFields.faded))
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PrefixLabel("Color");
            myScript.m_Color = EditorGUILayout.ColorField(myScript.m_Color);
            EditorGUILayout.PrefixLabel("Text");
            myScript.m_String = EditorGUILayout.TextField(myScript.m_String);
            EditorGUILayout.PrefixLabel("Number");
            myScript.m_Number = EditorGUILayout.IntSlider(myScript.m_Number, 0, 10);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();
    }
}
*/
