using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class L1_UIManager : MonoBehaviour
{
    //Properties...
    [Header("Question Properties")]
    [SerializeField]
    private RectTransform inputUI;
    [SerializeField]
    private RectTransform questionBoxUI;
    [SerializeField]
    private RectTransform ResultsMenuUI;
    [SerializeField]
    private RectTransform introUI;
    public RectTransform proceedUI;
    [SerializeField]
    private RectTransform videoUI;
    [SerializeField]
    private RectTransform againUI;
    [SerializeField]
    public RectTransform endUI;
    [SerializeField]
    private RectTransform indicator;
    [SerializeField]
    private GameObject point;
    [SerializeField]
    private float pointRotation;

    public RectTransform questionTextBackground;
    public TMP_Text questionText;
    public TMP_Text trueAnswerText;
    public TMP_Text falseAnswerText;
    public Button TrueAnswerButton;
    public Button FalseAnswerButton;
    public TMP_Text AffirmationText;
    public TMP_Text QuestionsAskedText;
    public TMP_Text QuestionsAnsweredText;
    public TMP_Text CorrectAnswersText;
    public TMP_Text WrongAnswersText;
    [SerializeField]
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //ShowQuestionUI(false);
        //ShowResultsMenu(false);
        //introUI.gameObject.SetActive(true);
        ShowInputUI(false);
        ShowQuestionUI(false, 0.0f);
        //AffirmationText.gameObject.SetActive(false);
        //point.gameObject.SetActive(false);
        indicator.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //IncrementPointLocation();
    }

    public void SetQuestionElements(Questions question)
    {
        if (questionText && trueAnswerText && falseAnswerText)
        {
            questionText.text = question.textQuestion;
            trueAnswerText.text = question.trueAnswerText;
            falseAnswerText.text = question.falseAnswerText;
        }
    }

    public void ShowQuestionUI(bool condition, float time)
    {
        //if (questionBoxUI)
        //    questionBoxUI.gameObject.SetActive(condition);
        StartCoroutine(SetQuestionUIActive(condition, time));
    }
    private IEnumerator SetQuestionUIActive(bool condition, float time)
    {
        //point.gameObject.SetActive(true);
        indicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        if (questionBoxUI)
            questionBoxUI.gameObject.SetActive(condition);
    }
    public void ShowInputUI(bool condition)
    {
        //L1_GameManager.gameManager.errorText.gameObject.SetActive(false);
        gameManager.gameObject.GetComponent<L1_GameManager>().inputField.text = "";
        if (inputUI)
            inputUI.gameObject.SetActive(condition);
    }

    public void ShowAffirmationText(string text)
    {
  
        //Set text...
        //AffirmationText.text = text;
        //Hide AffirmationText after 2.0s...
        Invoke(nameof(HideAffirmationText), 2.0f);
    }
    private void HideAffirmationText()
    {
        //AffirmationText.gameObject.SetActive(false);
    }

    public void IncrementPointLocation()
    {
        if (!point)
            return;
        //point.anchoredPosition += new Vector2(pointParentWidth / gameManager.gameObject.GetComponent<L1_GameManager>().noOfQuestions, 0);
        //point.transform.DOMoveX((point.transform.position.x-60)+pointRotation / gameManager.gameObject.GetComponent<L1_GameManager>().noOfQuestions,1f, false);
        point.transform.DORotate(
            new Vector3(0,0,-68) + point.transform.rotation.eulerAngles, 1f, RotateMode.Fast);
    }


    public void SetUIActive(RectTransform uI)
    {
        introUI.gameObject.SetActive(false);
        ResultsMenuUI.gameObject.SetActive(false);
        questionBoxUI.gameObject.SetActive(false);
        proceedUI.gameObject.SetActive(false);
        videoUI.gameObject.SetActive(false);
        endUI.gameObject.SetActive(false);
        againUI.gameObject.SetActive(false);

        uI.gameObject.SetActive(true);
    }
}
