using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriviaSystem : MonoBehaviour
{
    //Array to hold all trivia question object...
    [SerializeField]
    private TriviaQuestionObject[] questionObjects;

    private int noOfQuestions;
    private int noOfCorrectAnswers;
    private int noOfWrongAnswers;

    // A variable to keep track of the question index...
    private int questionIndex = 0;
    [SerializeField]
    private GameObject questionBoxUI;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private TMP_Text questionNumberText;
    // Array holding reference to all buttons on the trivia layout...
    [SerializeField]
    private TMP_Text[] answerTexts;

    // Array holding reference to question text ui...
    [SerializeField] TMP_Text questionText;
    [SerializeField]
    private TMP_Text commentaryText;
    [SerializeField]
    private TMP_Text endCommentaryText;
    [SerializeField]
    private TMP_Text priceText;
    [SerializeField]
    private TMP_Text detailsText;

    private int price = 0;



    [SerializeField]
    private TMP_Text resultPriceText;
    [SerializeField]
    private Sprite defaultSprite;

    [SerializeField]
    private Sprite correctSprite;
    [SerializeField]
    private Sprite wrongSprite;
    private Button selectedButton;



    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
        noOfQuestions = questionObjects.Length;
        selectedButton = answerTexts[0].transform.parent.GetComponent<Button>();
        SetUIElements();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SetUIElements()
    {
        questionNumberText.text = (questionIndex + 1) + "/" + questionObjects.Length;
        questionBoxUI.SetActive(true);
        commentaryText.gameObject.SetActive(false);
        questionText.text = questionObjects[questionIndex].question;
        // Set answer text elements...
        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].transform.parent.gameObject.SetActive(false);
        }
        for (int i = 0; i < questionObjects[questionIndex].answers.Length; i++)
        {
            answerTexts[i].transform.parent.gameObject.SetActive(true);
            answerTexts[i].text = questionObjects[questionIndex].answers[i];
        }
        detailsText.text = "This question is worth $" + questionObjects[questionIndex].points;
        //Set button image color to white...
        //selectedButton.GetComponent<Image>().color = Color.white;
        selectedButton.GetComponent<Image>().sprite = defaultSprite;
        SetButtonsInteractable(true);
    }

    public void CheckSelection(int index)
    {
        SetButtonsInteractable(false);
        if (!commentaryText)
            return;
        commentaryText.gameObject.SetActive(true);
        if (index == questionObjects[questionIndex].correctAnswerIndex)
        {
            Debug.Log("Correct Answer!!!");
            commentaryText.text = "Correct!";
            commentaryText.color = Color.green;
            noOfCorrectAnswers++;
            price = questionObjects[questionIndex].points;
            priceText.text = "$" + price;
            //Set button image color to green...
            //selectedButton.GetComponent<Image>().color = Color.green;
            selectedButton.GetComponent<Image>().sprite = correctSprite;
            //MoveToNextQuestion...
            if (questionIndex < questionObjects.Length - 1)
            {
                questionIndex++;
                Invoke(nameof(SetUIElements), 2.0f);
            }
            else
            {
                Invoke(nameof(ShowGameOverUI), 2.0f);
            }
        }
        else
        {
            Debug.Log("Incorrect Answer!!!");
            selectedButton.GetComponent<Image>().color = Color.red;
            selectedButton.GetComponent<Image>().sprite = wrongSprite;
            Invoke(nameof(ShowGameOverUI), 2.0f);
            commentaryText.text = "Incorrect!";
            commentaryText.color = Color.red;
            noOfWrongAnswers++;
        }
    }
    private void ShowGameOverUI()
    {
        if (price <= 0)
            endCommentaryText.text = "Awnn, try again!!!";
        else if (price > 0 && price <= 5000)
            endCommentaryText.text = "You can do better!!!";
        else if (price > 5000 && price <= 10000)
            endCommentaryText.text = "Good work!!!";
        else if (price > 10000 && price <= 20000)
            endCommentaryText.text = "Great job!!!";
        else
            endCommentaryText.text = "Excellent Performance!!!";

        resultPriceText.text = priceText.text;
        questionBoxUI.SetActive(false);
        gameOverUI.SetActive(true);
    }
    public void SetSelectedButton(Button button)
    {
        selectedButton = button;
    }

    private void SetButtonsInteractable(bool condition)
    {
        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].transform.parent.GetComponent<Button>().interactable = condition;
        }
    }
}
