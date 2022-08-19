using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class L9_GameManager : MonoBehaviour
{
    //Properties...
    [SerializeField]
    private Questions[] questionObjects;
    int questionIndex = 0;
    public int noOfQuestions = 0;
    private int questionCount = 0;
    public int noOfQuestionsAnswered = 0;
    public int correctAnswers = 0;
    public int wrongAnswers = 0;
    public bool isLevelFinished = false;

   
    [SerializeField]
    private RectTransform questionBoxUI;
    [SerializeField]
    private RectTransform commentaryBox;
    [SerializeField]
    private TMP_Text questionText;
    [SerializeField]
    private TMP_Text commentaryText;
    [SerializeField]
    private Image questionImage;
    [SerializeField]
    private Button trueButton;
    [SerializeField]
    private Button falseButton;




    // Start is called before the first frame update
    void Start()
    {
        SetQuestionBox();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetQuestionBox()
    {
        questionText.text = questionObjects[questionIndex].textQuestion;
        questionImage.sprite = questionObjects[questionIndex].questionImage;
    }

    public void CheckSelection(bool condition)
    {

       
        if (condition == questionObjects[questionIndex].isClickTrue)
        {
            //Execute true statement...
            correctAnswers++;
           
        }
        else
        {
            //Execute false statement...
            wrongAnswers++;
        }

        // Increment Question Index...
        questionIndex++;
        if (!(questionIndex < questionObjects.Length))
        {
            // Game Over
        }
        else
        {
            // Set new question set...
            SetQuestionBox();
        }
    }
}
