using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Fungus;
using Sirenix.Utilities;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class L6_GameManager : MonoBehaviour
{
    [SerializeField]
    private Questions[] questions;
    private int questionIndex = 0;

    [SerializeField]
    private Vector2 senderSpawnLocation;
    [SerializeField]
    private Vector2 userSpawnLocation;
    [FormerlySerializedAs("parentTransform")]
    [SerializeField]
    private RectTransform leftParentTransform;

    [FormerlySerializedAs("bubble")]
    [SerializeField]
    private Bubble strangerBubble;

    public ScrollRect ScrollRect;
    [SerializeField] private Bubble playerBubble;
    private List<Bubble> bubbles = new List<Bubble>();
    private int bubbleIndex = 0;

    private int correctPoints = 0;

    [SerializeField]
    private Button shareButton;
    [SerializeField]
    private Button dontShareButton;

    private bool messageFromSender = true;
    public Flowchart Flowchart;
    public Button continueButton;

    //NB: These variables must be set before using the SetSendMessage function....
    Vector2 setPosition = Vector2.zero;
    string question = "Hello \n. Test";

    //NB: This variable must be set before using ActivateShareButtons function...
    bool showButtons = false;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.soundManager.PlaySFX("BackgroundMusic");
        OverallGameManager.overallGameManager.playerData.currentLesson = SceneManager.GetActiveScene().buildIndex;
        StartLesson();
    }

    private void StartLesson()
    {
        //Sender sends...
        setPosition = senderSpawnLocation;
        question = questions[questionIndex].questionText;
        SetSendMessage();

        //Activate Buttons...
        showButtons = true;
        ActivateShareButtons();
        continueButton.gameObject.SetActive(false);
    }

    private void SetSendMessage()
    {
        if (messageFromSender)
        {
            bubbles.Add(Instantiate<Bubble>(strangerBubble, leftParentTransform));
            TMP_Text messageText = bubbles[bubbleIndex].message;
            bubbles[bubbleIndex].CanvasGroup.DOFade(1, 0.75f);
            StartCoroutine(DialogText(0.05f, messageText));
            bubbleIndex++;
        }
        else
        {
            bubbles.Add(Instantiate<Bubble>(playerBubble, leftParentTransform));
            TMP_Text messageText = bubbles[bubbleIndex].message;
            bubbles[bubbleIndex].CanvasGroup.DOFade(1, 0.75f);
            StartCoroutine(DialogText(0.05f, messageText));
            bubbleIndex++;
        }

        AddContentViewHeight();
        ScrollToBottom(ScrollRect);
    }

    private void AddContentViewHeight()
    {
        if (leftParentTransform.transform.childCount > 3)
        {
            var height = leftParentTransform.rect.height;
            leftParentTransform.sizeDelta = new Vector2(0, height + 150);
        }
    }

    public void ScrollToBottom(ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }

    private void ActivateShareButtons()
    {
        shareButton.gameObject.SetActive(showButtons);
        dontShareButton.gameObject.SetActive(showButtons);
    }

    private IEnumerator DialogText(float timePerChar, TMP_Text messageBox)
    {
        foreach (char c in question)
        {
            //Debug.Log(c);
            yield return new WaitForSeconds(timePerChar);
            messageBox.text += c;
        }
    }

    IEnumerator DestroyAllBubbles()
    {
        yield return new WaitForSeconds(0.5f);
        Flowchart.ExecuteBlock(Flowchart.GetBooleanVariable("firstTime") ? "WatchVideo" : "CheckPoints");


        foreach (Transform child in ScrollRect.content.transform)
        {
            Destroy(child.gameObject);
        }
        
        questionIndex = 0;
        bubbleIndex = 0;
        correctPoints = 0;
        messageFromSender = true;
        ScrollRect.content.sizeDelta = new Vector2(ScrollRect.content.sizeDelta.x,150);
        StartLesson();
    }

    public void OnContinueButtonPressed()
    {
        continueButton.gameObject.SetActive(false);
        StartCoroutine(DestroyAllBubbles());
        bubbles.Clear();
    }

    public void CheckPlayerPointScore()
    {
        Flowchart.ExecuteBlock(Flowchart.GetIntegerVariable("playerPoint") < 4 ? "ScoreBelow4" : "ScoreAbove4");
    }

    public void ExecuteAfterVideo()
    {
        Flowchart.ExecuteBlock("AfterVideo");
    }

    public void CheckResponse(bool condition)
    {
        // Deactivate Buttons...
        showButtons = false;
        ActivateShareButtons();
        messageFromSender = false;
        //MoveAllBubbles();

        // User response logic...
        if (condition == true)
        {
            setPosition = userSpawnLocation;
            question = questions[questionIndex].trueAnswerText;
            SetSendMessage();
        }
        else
        {
            setPosition = userSpawnLocation;
            question = questions[questionIndex].falseAnswerText;
            SetSendMessage();
        }

        // Check if selection is correct....
        if (condition == questions[questionIndex].isClickTrue)
            correctPoints++;
        
        Flowchart.SetIntegerVariable("playerPoint",correctPoints);

        Debug.Log("Correct Points: " + correctPoints);

        // Move to next question object...
        questionIndex++;
        Debug.Log(questionIndex);

        if (questionIndex < questions.Length)
        {
            messageFromSender = true;
            // Move bubble up...
            //Invoke(nameof(MoveAllBubbles), 3.0f);

            // Sender sends...
            setPosition = senderSpawnLocation;
            question = questions[questionIndex].questionText;
            Invoke(nameof(SetSendMessage), 3.0f);

            // Activate Buttons...
            showButtons = true;
            Invoke(nameof(ActivateShareButtons), 4.0f);
        }
        else
        {
            Flowchart.ExecuteBlock("ShowContinueBtn");
        }
    }


    public void RestartLesson()
    {
        OverallGameManager.overallGameManager.ReloadScene();
    }

    public void NextLesson(int sceneNumber)
    {
        OverallGameManager.overallGameManager.LoadNextScene(sceneNumber);
    }

    public void PlaySFX(string soundName)
    {
        SoundManager.soundManager.PlaySFX(soundName);
    }

    public void StopAllSFX()
    {
        SoundManager.soundManager.StopAllSFX();
    }
}
