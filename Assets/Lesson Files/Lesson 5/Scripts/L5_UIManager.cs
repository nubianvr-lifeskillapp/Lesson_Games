using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class L5_UIManager : MonoBehaviour
{
    //Properties...
    [Header("Question Properties")]
    [SerializeField]
    private Transform questionBoxUI;
    [SerializeField]
    private Transform ResultsMenuUI;
    
    public CanvasGroup FailedMenuUI;
    public GameObject FakeFade;
    public TMP_Text questionText;
    public TMP_Text[] textOptions;
    public Button[] buttonOptions;
    public Image questionImage;
    public TMP_Text AffirmationText;
    public Image threeStarsImage;
    public GameObject levelManager;
    public L5_GameManager levelManagerScript;

    private void Awake()
    {
        levelManagerScript = levelManager.GetComponent<L5_GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowQuestionUI(false);
        ShowResultsMenu(false);
        FakeFade.gameObject.SetActive(true);
        threeStarsImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetQuizElements(Question5Objects question)
    {
        
            questionText.text = question.questionText;
            questionImage.sprite = question.questionImage;
            for (int i = 0; i < textOptions.Length; i++)
            {
                textOptions[i].text = question.answers[i];
            }

    }

    public void ShowQuestionUI(bool condition)
    {
        if (questionBoxUI)
            questionBoxUI.gameObject.SetActive(condition);
    }

    public void ShowAffirmationText(string text)
    {
        //Set object active(Visible on screen)...
        AffirmationText.gameObject.SetActive(true);
        //Set text...
        AffirmationText.text = text;
        //Hide AffirmationText after 2.0s...
        Invoke(nameof(HideAffirmationText), 2.0f);
    }

    private void HideAffirmationText()
    {
        AffirmationText.gameObject.SetActive(false);
    }

    public void ShowResultsMenu(bool condition)
    {
        threeStarsImage.fillAmount = 0;
        ResultsMenuUI.gameObject.SetActive(condition);
        threeStarsImage.DOFillAmount((float)levelManagerScript.correctAnswers / levelManagerScript.NumberOfQuestionsToAnswer,
            2.0f);
    }

    public void ShowFailedScreen()
    {
        FailedMenuUI.gameObject.SetActive(true);
        FailedMenuUI.DOFade(1.0f, 1.0f);
    }
}
