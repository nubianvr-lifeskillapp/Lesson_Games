using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishSurvey : MonoBehaviour
{
  public void FinishSurveyBtn()
  {
    if (SceneManager.GetActiveScene().buildIndex == 12)
    {
      OverallGameManager.overallGameManager.LoadNextScene(1);
    }
    else if (SceneManager.GetActiveScene().buildIndex == 13)
    {
      OverallGameManager.overallGameManager.LoadNextScene(11);
    }

  }
}
