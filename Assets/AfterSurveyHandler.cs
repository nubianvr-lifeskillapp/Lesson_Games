using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterSurveyHandler : MonoBehaviour
{
    public void SurveyComplete()
    {
        StartCoroutine(SurveyCompleteWaitTask());
    }

    IEnumerator SurveyCompleteWaitTask()
    {
        yield return new WaitForSeconds(2.5f);
        if (SceneManager.GetActiveScene().buildIndex == 13)
        {
            OverallGameManager.overallGameManager.playerData.preTestDone = true;
            DataPersistanceManager.instance.SaveStudentGame();
            OverallGameManager.overallGameManager.LoadNextScene(1);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 14)
        {
            OverallGameManager.overallGameManager.playerData.postTestDone = true;
            DataPersistanceManager.instance.SaveStudentGame();
            OverallGameManager.overallGameManager.LoadNextScene(12);
        }
    }
}
