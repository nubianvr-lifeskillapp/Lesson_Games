using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetLevelLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetLevelLoad()
    {
        OverallGameManager.overallGameManager.GetSceneToLoad();
    }
}
