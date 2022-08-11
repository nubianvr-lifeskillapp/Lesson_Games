using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class L_Dialog : MonoBehaviour
{
    [SerializeField] 
    [Tooltip("Specify a location relative to the current location of this game object")]
    private Vector2 dialogEndPosition = new Vector2();
    [SerializeField]
    private float tweenTime = 1.0f;
    [SerializeField]
    private TMP_Text messageText;
    [SerializeField]
    private string message;
    [SerializeField]
    private float timePerChar = 0.15f;
    private Button button;
    private Vector2 defaultPosition = new Vector2();
    private RectTransform gameObjectTransform = new RectTransform();
    
    // Start is called before the first frame update
    void Start()
    {
        messageText.text = "";
        button = gameObject.GetComponentInChildren<Button>();
        button.interactable = false;
        gameObjectTransform = gameObject.GetComponent<RectTransform>();
        defaultPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
    }

    public void ShowDialog()
    {
        if(dialogEndPosition.x == 0.0f)
            gameObject.GetComponent<RectTransform>().DOLocalMoveY(dialogEndPosition.y, tweenTime);
        else if(dialogEndPosition.y == 0.0f)
            gameObject.GetComponent<RectTransform>().DOLocalMoveX(dialogEndPosition.x, tweenTime);
        else
            gameObject.GetComponent<RectTransform>().DOLocalMove(dialogEndPosition, tweenTime);

        StartCoroutine(DialogText(timePerChar, messageText));
    }
    private IEnumerator DialogText(float timePerChar, TMP_Text messageBox)
    {
        foreach (char c in message)
        {
            //Debug.Log(c);
            yield return new WaitForSeconds(timePerChar);
            messageBox.text += c;
        }
        button.interactable = true;
    }
    public void SetDialogText()
    {
        Debug.Log("Clicked!");
        StopAllCoroutines();
        messageText.text = message;
        button.interactable = true;
    }

    public void ResetPosition()
    {
        gameObjectTransform.anchoredPosition = defaultPosition;
    }

    public void SetMessage(string text)
    {
        message = text;
    }
    //public void SetFontSize(float fontSize)
    //{
    //    messageText.fontSize = fontSize;
    //}
}
