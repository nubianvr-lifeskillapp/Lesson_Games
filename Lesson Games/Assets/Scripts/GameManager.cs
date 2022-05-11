using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Must be set to aid what scene this game manager would affect.")]
    [Range(1,9)]private int lessonIndex = 0;
    #region Lesson 2 Properties...
    [Header("Lesson Two Properties")]
    [SerializeField]
    private SetObject_S[] questionSetImages;
    private static int questionSetIndex = 0;
    [SerializeField]
    private BaseCarouselScript background;
    [SerializeField]
    private BaseCarouselScript middleground;
    [SerializeField]
    private BaseCarouselScript foreground;
    [HideInInspector]
    public int totalPoints = 0;

    public static GameManager gameManager;
    #endregion
    #region Lesson 5 Properties...
    [Header("Lesson Five Properties")]
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private TimeManager timeManager;
    [SerializeField]
    private UIManager uIManager;
    [SerializeField]
    private Questions[] allQuestions;

    public int noOfQuestions;
    private int questionIndex = 0;
    private int questionCount = 0;
    public int noOfQuestionsAnswered = 0;
    public int correctAnswers = 0;
    public int wrongAnswers = 0;
    public bool isLevelFinished = false;
    #endregion
    #region Lesson 7 Properties...
    //Properites
    [Header("Lesson Seven Properties")]
    //Reference to the UI manager...
    //[SerializeField]
    //private L7_UIManager uiManager;
    [SerializeField]
    private UIManager uiManager;
    //Array/List of all imageObjects present in the scene...
    [SerializeField]
    private Image[] imageObjects;
    //Reference to all popup scriptable objects associated with this scene...
    [SerializeField]
    private Popup[] allPopups;
    //Static variable...Total coins per click...
    public static int totalCoins = 0;
    //Variable that keeps track of popup layouts closed...
    public static int noOfPopupLayoutClosed = 0;
    private bool gameOver = false;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        SetRespectivePropertiesOnAwake();
    }

   
    private void Start()
    {
        SetrespectivePropertiesOnStart();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region Generic Functions...
    private void SetRespectivePropertiesOnAwake()
    {
        switch (lessonIndex)
        {
            case 2:
                #region Set Lesson 2 Properties...
                //Set SetImages in the base carousel script of these objects...
                //The BaseCarousel Script depends on these lines of statements...
                background.setImages = questionSetImages[questionSetIndex];
                middleground.setImages = questionSetImages[questionSetIndex];
                foreground.setImages = questionSetImages[questionSetIndex];
                #endregion
                break;

            default:
                break;
        }
    }

    private void SetrespectivePropertiesOnStart()
    {
        switch (lessonIndex)
        {
            case 5:
                uIManager.SetQuizElements(allQuestions[questionIndex]);
                Debug.Log("No of Questions: " + allQuestions.Length);
                questionCount = allQuestions.Length;
                noOfQuestions = allQuestions.Length;
                break;

            case 7:
                //Check if allPopups array length is less than or equal to zero, If it is then do not proceed with this excecution...
                if (allPopups.Length <= 0)
                    return;
                int i = 0;
                foreach (Image image in imageObjects)
                {
                    imageObjects[i].sprite = allPopups[i].popupImage;
                    Debug.Log(imageObjects[i].name);
                    i++;
                }
                break;

            default:
                break;
        }
    }

    public void SetNewSequence(int buildIndex)
    {
        MoveToNextSequence();
        SceneManager.LoadScene(buildIndex);
    }

    public void ReloadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    #endregion

    #region Lesson 2 Functions...
    public void SelectImage(BaseCarouselScript image)
    {
        if (image)
        {
            totalPoints += image.GetImagePoint();
            Debug.Log(totalPoints);
        }
    }
    public void MoveToNextSequence()
    {
        questionSetIndex++;
        if (questionSetIndex > (questionSetImages.Length - 1))
            questionSetIndex = 0;
        Debug.Log("Current Image Index: " + questionSetIndex);
        background.setImages = questionSetImages[questionSetIndex];
        middleground.setImages = questionSetImages[questionSetIndex];
        foreground.setImages = questionSetImages[questionSetIndex];
    }
    #endregion

    #region Lesson 5 Functions...
       public void CheckSelection(bool condition)
    {
        if (!(uIManager.TrueAnswerButton && uIManager.FalseAnswerButton))
            return;

        //If it is the right selection...
        if(allQuestions[questionIndex].isClickTrue == condition)
        {
            uIManager.ShowAffirmationText("Correct!");
            uIManager.ShowQuestionUI(false);
            player.Jump();
            correctAnswers++;
        }

        //If it is the wrong selection....
        else
        {
            uIManager.ShowAffirmationText("Incorrect!");
            uIManager.ShowQuestionUI(false);
            wrongAnswers++;
        }

        noOfQuestionsAnswered++;
        timeManager.bCanScaleUp = true;
        MoveToNextQuestion();
    }

    public void MoveToNextQuestion()
    {
        //Increase questionIndex...
        if (questionIndex < allQuestions.Length - 1)
        {
            questionIndex++;
            //Set Quiz UI Elements...
            uIManager.SetQuizElements(allQuestions[questionIndex]);
        }
          
        //Reset to first question index...
        else
        {
            OnLevelFinshed();
        }
    }
    IEnumerator ShowResultsMenu(bool condition)
    {
        yield return new WaitForSeconds(3.0f);
        uIManager.ShowResultsMenu(condition);
    }

    public void OnLevelFinshed()
    {
        isLevelFinished = true;
        StartCoroutine(ShowResultsMenu(true));
    }
    #endregion

    #region Lesson 7 Functions...
    public void SetTotalCoins(int index)
    {
        //Check the popup scriptable object to see if the boolean isHarmful is set to true, if it is then get the required coins/points off the total coins/points...
        if (allPopups[index].isHarmful)
        {
            totalCoins -= allPopups[index].coins;
            if (totalCoins < 0)
                totalCoins = 0;
        }
        else
            totalCoins += allPopups[index].coins;

        imageObjects[index].transform.parent.gameObject.SetActive(false);
        IncreaseNoOfPopupsClosed();

        if (uiManager)
            uiManager.SetCoinText(totalCoins);
    }

    public void IncreaseNoOfPopupsClosed()
    {
        noOfPopupLayoutClosed++;
        if (noOfPopupLayoutClosed == imageObjects.Length)
        {
            gameOver = true;
            Debug.Log("GameOver!");
            uiManager.ShowGameOverLayout();
        }
    }
    #endregion
}
