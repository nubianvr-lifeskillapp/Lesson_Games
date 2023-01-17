using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L2_GameManager : MonoBehaviour
{
    [SerializeField]
    private SetObject_S[] questionSetImages;
    private static int questionSetIndex = 0;
    [SerializeField]
    private L2_BaseCarouselScript background;
    [SerializeField]
    private L2_BaseCarouselScript middleground;
    [SerializeField]
    private L2_BaseCarouselScript foreground;

    [HideInInspector]
    public int totalPoints = 0;
    
    public Flowchart flowchart;
    public static L2_GameManager gameManager;

    private L2_BaseCarouselScript[] scripts;
    //public SoundManager soundManager;

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

        //DontDestroyOnLoad(gameObject);

        //Set SetImages in the base carousel script of these objects...
        //The BaseCarousel Script depends on these lines of statements...
        background.setImages = questionSetImages[questionSetIndex];
        middleground.setImages = questionSetImages[questionSetIndex];
        foreground.setImages = questionSetImages[questionSetIndex];
    }

    private void Start()
    {
        scripts = new L2_BaseCarouselScript[3] {background,middleground,foreground};
        OverallGameManager.overallGameManager.playerData.currentLesson = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectImage(RectTransform image)
    {
        L2_BaseCarouselScript temp = image.GetComponent<L2_BaseCarouselScript>();
        if (temp)
        {
            totalPoints += temp.GetImagePoint();
            flowchart.SetIntegerVariable("pictureScore", totalPoints);
            Debug.Log(totalPoints);
        }
    }

    public void LoadNextScene(int scene)
    {
        OverallGameManager.overallGameManager.LoadNextScene(scene);
    }

    public void TryAgainBlock()
    {
        EnbaleBaseCarouselScript();
        flowchart.ExecuteBlock("Try Creating A ProfPic Again");
    }

    public void ReloadScene()
    {
        OverallGameManager.overallGameManager.ReloadScene();
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

    public void StartCheckScoreBlock()
    {
        flowchart.ExecuteBlock("Check Score");
    }

    public void DisableBaseCarouselScript(L2_BaseCarouselScript script)
    {
        script.enabled = false;
        Debug.Log("Base Carousel Script Disabled...");
    }

    public void EnbaleBaseCarouselScript()
    {
        totalPoints = 0;
        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].enabled = true;
            scripts[i].GoToIndex(0);
            scripts[i].canvas.interactable = true;
        }
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
