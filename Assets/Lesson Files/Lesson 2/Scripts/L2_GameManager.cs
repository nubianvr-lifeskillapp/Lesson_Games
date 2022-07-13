using System.Collections;
using System.Collections.Generic;
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

    public static L2_GameManager gameManager;
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
            Debug.Log(totalPoints);
        }
    }

    public void LoadNextScene(int scene)
    {
        OverallGameManager.overallGameManager.LoadNextScene(scene);
    }

    public void ReloadScene(int buildIndex)
    {
        MoveToNextSequence();
        SceneManager.LoadScene(buildIndex);
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

    public void DisableBaseCarouselScript(L2_BaseCarouselScript script)
    {
        script.enabled = false;
        Debug.Log("Base Carousel Script Disabled...");
    }
    

}
