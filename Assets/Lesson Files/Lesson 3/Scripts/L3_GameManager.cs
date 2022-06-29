using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // Start is called before the first frame update
    void Start()
    {
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
        uIManager.IncrementProgressBar(optionObjects[index].optionPoints);
        uIManager.optionButtons[index].interactable = false;
        totalScore += (optionObjects[index].optionPoints * 100);
        selectedAnswersCount++;
        Debug.Log("Selected Answers:  " + selectedAnswersCount);
        Debug.Log("Total Score:  " + totalScore);
        if (!(selectedAnswersCount < expectedSelectedAnswers))
        {
            foreach (Button button in uIManager.optionButtons)
            {
                button.interactable = false;
            }
        }
    }
}
