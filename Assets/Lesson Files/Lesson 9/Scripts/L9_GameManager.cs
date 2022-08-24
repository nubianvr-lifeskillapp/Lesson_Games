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
    public int correctAnswers = 0;
    public int wrongAnswers = 0;
    // Question Image is Post Image...
    // Question Text is SenderCommentText...

    [SerializeField]
    private Image postImage;
    [SerializeField]
    private Image senderProfileImage;
    [SerializeField]
    private TMP_Text senderCommentText;
    [SerializeField]
    private TMP_Text senderUsernameText;



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
        postImage.sprite = questionObjects[questionIndex].questionImage;
        senderProfileImage.sprite = questionObjects[questionIndex].senderProfileImage;
        senderCommentText.text = questionObjects[questionIndex].questionText;
        senderUsernameText.text = questionObjects[questionIndex].senderName;
    }

    public void CheckSelection(bool condition)
    {


        if (condition == questionObjects[questionIndex].isClickTrue)
        {
            //Execute true statement...
            correctAnswers++;
            Debug.Log("Correct!");
        }
        else
        {
            //Execute false statement...
            wrongAnswers++;
            Debug.Log("Incorrect!");
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
