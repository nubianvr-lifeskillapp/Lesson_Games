using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
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

    //NB: These variables must be set before using the SetSendMessage function....
    Vector2 setPosition = Vector2.zero;
    string question = "Hello \n. Test";

    //NB: This variable must be set before using ActivateShareButtos function...
    bool showButtons = false;

    // Start is called before the first frame update
    void Start()
    {
        //Sender sends...
        setPosition = senderSpawnLocation;
        question = questions[questionIndex].questionText;
        SetSendMessage();

        //Activate Buttons...
        showButtons = true;
        ActivateShareButtons();
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

    private void MoveAllBubbles()
    {
        foreach (Bubble bubbleRef in bubbles)
        {
            bubbleRef.upValue += 100.0f;
            bubbleRef.MoveBubbleUp(bubbleRef.upValue);
        }
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

        Debug.Log("Correct Points: " + correctPoints);

        // Move to next question object...
        questionIndex++;

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
    }
}
