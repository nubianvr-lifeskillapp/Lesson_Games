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
    private BaseCarouselScript background;
    [SerializeField]
    private BaseCarouselScript middleground;
    [SerializeField]
    private BaseCarouselScript foreground;

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
        BaseCarouselScript temp = image.GetComponent<BaseCarouselScript>();
        if (temp)
        {
            totalPoints += temp.GetImagePoint();
            Debug.Log(totalPoints);
        }
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

    public void DisableBaseCarouselScript(BaseCarouselScript script)
    {
        script.enabled = false;
        Debug.Log("Base Carousel Script Disabled...");
    }

}
