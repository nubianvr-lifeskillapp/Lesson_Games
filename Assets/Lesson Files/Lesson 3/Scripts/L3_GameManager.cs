using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Fungus;
using UnityEngine.SceneManagement;

public class L3_GameManager : MonoBehaviour
{
    //Main Properties...
    [SerializeField]
    private L3_UIManager uIManager;
    [SerializeField]
    private OptionObject[] optionObjects;
    [SerializeField]
    private int expectedSelectedAnswers = 0;
    private int selectedAnswersCount = 0;
    [HideInInspector]
    public float totalScore = 0;

    public  L_Dialog dialogUI;
    public L_Dialog replayDialogUI;
    public L_Dialog endDialogUI;
    public bool remakePassword = false;

    [SerializeField]
    public GameObject endScreenUI;

    public Flowchart flowchart;

    // Start is called before the first frame update
    void Start()
    {
        endScreenUI.SetActive(false);

        Debug.Log("Start");
        if (optionObjects.Length > 0 && uIManager.optionButtons.Length > 0)
        {
            for (int i = 0; i < uIManager.optionButtons.Length; i++)
            {
                uIManager.optionButtons[i].transform.GetComponentInChildren<TextMeshProUGUI>().text = optionObjects[i].optionText;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckSelection(int index)
    {
        if (!uIManager.canSelect)
            return;
        uIManager.canSelect = false;
        uIManager.IncrementProgressBar(optionObjects[index].optionPoints);
        uIManager.optionButtons[index].interactable = false;
        totalScore += (optionObjects[index].optionPoints);
        if (totalScore < 0)
            totalScore = 0;
        selectedAnswersCount++;
        Debug.Log("Selected Answers:  " + selectedAnswersCount);
        Debug.Log("Total Score:  " + totalScore);

        if (!(selectedAnswersCount < expectedSelectedAnswers))
        {
            //EnableButtons(false, true);
            //EnableButtonsInteractable(false);
            //StartCoroutine(nameof(LevelFinished));
            if (!remakePassword)
            {
                flowchart.ExecuteBlock("Lesson 3 SegueToVideoView");
                flowchart.SetFloatVariable("lockStrength", totalScore);
            }
            else

            {
                StartCoroutine(Delay(1));
                
                flowchart.SetFloatVariable("lockStrength", totalScore);
                
                flowchart.ExecuteBlock("CheckPasswordStrength");
                
            }

            
        }
    }

    public void RecreatePassword()
    {
        flowchart.ExecuteBlock("CreatePasswordAgain");
        ResetGameView();
    }

    public void RetryLesson()
    {
        OverallGameManager.overallGameManager.ReloadScene();
    }
    

    public void ResetGameView()
    {
        uIManager.progressBar.value = 0;
        uIManager.ResetGameplayView();
        flowchart.SetFloatVariable("lockStrength", 0);
        remakePassword = true;
        totalScore = 0;
        selectedAnswersCount = 0;
        EnableButtonsInteractable(true);
    }

    public void EnableButtonsInteractable(bool interactable)
    {
        foreach(Button button in uIManager.optionButtons)
        {
            button.interactable = interactable;
        }
    }
    public void EnableButtonsActive(bool active)
    {
        foreach (Button button in uIManager.optionButtons)
        {
            button.gameObject.SetActive(active);
        }
    }
    public void LoadNextScene(int scene)
    {
        OverallGameManager.overallGameManager.LoadNextScene(scene);
    }

    IEnumerator Delay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }
    public void PlaySfx(string soundName)
    {
        SoundManager.soundManager.PlaySFX(soundName);
    }

    public void StopAllSFX()
    {
        SoundManager.soundManager.StopAllSFX();
    }

}

