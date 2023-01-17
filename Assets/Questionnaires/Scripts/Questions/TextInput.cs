using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;
namespace VRQuestionnaireToolkit
{
public class TextInput : MonoBehaviour
{
   public int NumCheckboxButtons;
        public string QuestionnaireId;
        public string QId;
        public string QType;
        public string QInstructions;
        public string QText;
        public bool QMandatory;
   
        public GameObject textInput;
        public JSONArray QOptions;
   
        private RectTransform _questionRecTest;
        public List<GameObject> TextInputList;
   
        //qText look how many q in one file >4 deny
        public List<GameObject> CreateTextInputQuestion(string questionnaireId, string qType, string qInstructions, string qId, string qText, bool qMandatory, RectTransform questionRec)
        {
            this.QuestionnaireId = questionnaireId;
            this.QId = qId;
            this.QType = qType;
            this.QInstructions = qInstructions;
            this.QText = qText;
            this._questionRecTest = questionRec;
            this.QMandatory = qMandatory;
            
            TextInputList = new List<GameObject>();
            
            InitTextInput();
            
            return TextInputList;
        }
   
        void InitTextInput()
        {
            // Instantiate dropdown prefabs
            GameObject temp = Instantiate(textInput);
            temp.name = "textInput_";
            
            // Place in hierarchy 
            RectTransform textBoxRec = temp.GetComponent<RectTransform>();
            textBoxRec.SetParent(_questionRecTest);
            textBoxRec.localPosition = new Vector3(0 , -110 , 0);
            textBoxRec.localRotation = Quaternion.identity;
            //checkBoxRec.localScale = new Vector3(checkBoxRec.localScale.x * 0.01f, checkBoxRec.localScale.y * 0.01f, checkBoxRec.localScale.z * 0.01f);
            
            TextInputList.Add(temp);
            
        }
    }
}
