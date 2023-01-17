using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    public void EndAndResetPlayer()
    {
        OverallGameManager.overallGameManager.playerData.currentLesson = 1;
        OverallGameManager.overallGameManager.playerData.currentPlayThroughNumber += 1;
        DataPersistanceManager.instance.SaveStudentGame();
        OverallGameManager.overallGameManager.LoadNextScene(1);
    }
}
