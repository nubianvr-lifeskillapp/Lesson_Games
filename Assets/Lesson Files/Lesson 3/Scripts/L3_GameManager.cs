using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
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
    public static bool isReloaded = false;

    [SerializeField]
    public GameObject endScreenUI;

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
        if (isReloaded == true)
        {
            replayDialogUI.ShowDialog();
            Debug.Log("Showing Replay Dialog");
            EnableButtonsActive(false);
            EnableButtonsInteractable(false);
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
        totalScore += (optionObjects[index].optionPoints * 100);
        selectedAnswersCount++;
        Debug.Log("Selected Answers:  " + selectedAnswersCount);
        Debug.Log("Total Score:  " + totalScore);

        if (!(selectedAnswersCount < expectedSelectedAnswers))
        {
            //EnableButtons(false, true);
            EnableButtonsInteractable(false);
            StartCoroutine(nameof(LevelFinished));
        }
    }
    private IEnumerator LevelFinished()
    {
        yield return new WaitForSeconds(1.0f);
        //EnableButtons(false, false);
        EnableButtonsActive(false);
        //Tween in message...
        if(!isReloaded)
            dialogUI.ShowDialog();
        else
        {
            if(totalScore >= 50)
                endDialogUI.SetMessage("Wow, your password strength is high, very nice. You definetley made use of all the tips you learnt. Remember a strong password helps to keep all your private info safe from prying eyes.");
            else
                endDialogUI.SetMessage("Your password strength is low, maybe try again, try and use the tips we just learnt about. Remember a strong password helps to keep all your private info safe from prying eyes.");

            endDialogUI.ShowDialog();
        }
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
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isReloaded = true;
    }
    public void Replay()
    {
        if(totalScore < 50)
        {
            ResetScene();
        }
        else
        {
            endScreenUI.SetActive(true);
        }
    }
}

