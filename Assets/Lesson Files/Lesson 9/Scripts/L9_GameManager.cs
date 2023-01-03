using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class L9_GameManager : MonoBehaviour
{
    //Properties...
    [SerializeField]
    private Questions[] questionObjects;
    int questionIndex = 0;
    private int correctAnswers = 0;
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

    [SerializeField] private TMP_Text evidenceCollectedText;
    
    private int noOfQuestionsAnswered = 0;

    private bool isCorrectAnalysis = false;

    public Flowchart Flowchart;

    // Start is called before the first frame update
    void Start()
    {
        SetQuestionBox();
        PlaySfx("BackgroundMusic");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetQuestionBox()
    {
        postImage.sprite = questionObjects[questionIndex].questionImage;
        senderProfileImage.sprite = questionObjects[questionIndex].senderProfileImage;
        senderCommentText.text = questionObjects[questionIndex].questionText;
        senderUsernameText.text = questionObjects[questionIndex].senderName;
    }

    public void CheckSelection(bool condition)
    {
        noOfQuestionsAnswered++;
        if (condition == questionObjects[questionIndex].isClickTrue)
        {
            //Execute true statement...
            correctAnswers += 1;
            evidenceCollectedText.text = $"Evidence Collected: {correctAnswers}";
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Incorrect!");
        }

        // Increment Question Index...
        questionIndex++;
        if (!(questionIndex < questionObjects.Length))
        {
            
        }
        else
        {
            // Set new question set...
            SetQuestionBox();
        }
        CheckQuestionsFinished();
    }
    
    private void CheckQuestionsFinished()
    {
        if (noOfQuestionsAnswered < questionObjects.Length) return;
        Debug.Log("Completed");
        StartCoroutine(QuestionsEnded(0.25f));
    }

    IEnumerator QuestionsEnded(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Flowchart.ExecuteBlock(correctAnswers > 3 ? "ScoredAbove3" : "ScoredBelow3");
    }

    public void ResetGameValues()
    {
        // Reset Game Values
        questionIndex = 0;
        correctAnswers = 0;
        noOfQuestionsAnswered = 0;
        evidenceCollectedText.text = $"Evidence Collected: {correctAnswers}";
        SetQuestionBox();
    }

    public void ExecuteAfterVideo()
    {
        Flowchart.ExecuteBlock("ExecuteAfterVideo");
    }

    public void RestartLevel()
    {
        OverallGameManager.overallGameManager.ReloadScene();
    }

    public void LoadNextLevel(int sceneNumber)
    {
        OverallGameManager.overallGameManager.LoadNextScene(sceneNumber);
    }

    public void PlaySfx(string soundName)
    {
        SoundManager.soundManager.PlaySFX(soundName);
    }

    public void StopAllSfx()
    {
      SoundManager.soundManager.StopAllSFX();  
    }
    

}
