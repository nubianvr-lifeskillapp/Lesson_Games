using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
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
    [SerializeField]
    private RectTransform parentTransform;

    [SerializeField]
    private Bubble bubble;
    private List<Bubble> bubbles = new List<Bubble>();
    private int bubbleIndex = 0;

    private int correctPoints = 0;

    [SerializeField]
    private Button shareButton;
    [SerializeField]
    private Button dontShareButton;

    //NB: These variables must be set before using the SetSendMessage function....
    Vector2 setPosition = Vector2.zero;
    string question = "";

    //NB: This variable must be set before using ActivateShareButtos function...
    bool showButtons = false;

    // Start is called before the first frame update
    void Start()
    {
        //Sender sends...
        setPosition = senderSpawnLocation;
        question = questions[questionIndex].textQuestion;
        SetSendMessage();

        //Activate Buttons...
        showButtons = true;
        ActivateShareButtons();
    }

    private void SetSendMessage()
    {
        bubbles.Add(Instantiate<Bubble>(bubble, parentTransform));
        TMP_Text messageText = bubbles[bubbleIndex].transform.GetChild(0).GetComponent<TMP_Text>();
        messageText.text = question;
        bubbles[bubbleIndex].GetComponent<RectTransform>().anchoredPosition = setPosition;
        bubbleIndex++;
    }

    private void ActivateShareButtons()
    {
        shareButton.gameObject.SetActive(showButtons);
        dontShareButton.gameObject.SetActive(showButtons);
    }

    private void MoveAllBubbles()
    {
        foreach (Bubble bubbleRef in bubbles)
        {
            bubbleRef.upValue += 200.0f;
            bubbleRef.MoveBubbleUp(bubbleRef.upValue);
        }
    }

    public void CheckResponse(bool condition)
    {
        // Deactivate Buttons...
        showButtons = false;
        ActivateShareButtons();

        MoveAllBubbles();

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

        if(questionIndex < questions.Length)
        {
            // Move bubble up...
            Invoke(nameof(MoveAllBubbles), 3.0f);

            // Sender sends...
            setPosition = senderSpawnLocation;
            question = questions[questionIndex].textQuestion;
            Invoke(nameof(SetSendMessage), 3.0f);

            // Activate Buttons...
            showButtons = true;
            Invoke(nameof(ActivateShareButtons), 4.0f);
        }
    }
}
