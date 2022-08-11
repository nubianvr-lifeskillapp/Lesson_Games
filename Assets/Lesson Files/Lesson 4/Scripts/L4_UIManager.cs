using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class L4_UIManager : MonoBehaviour
{
    [Header("Tween Properties")]
    private Vector2 currentPostFrameDefaultPosition;
    private Vector2 currentPostFrameDefaultSize;
    [SerializeField]
    private Vector2 currentPostFrameAlteredPosition = new Vector2();
    [SerializeField]
    private Vector2 currentPostFrameAlteredSize = new Vector2();
    [SerializeField]
    private float tweenTime = 0.5f;


    [Header("Layout Properties")]
    [SerializeField]
    private RectTransform[] postFrames;
    [SerializeField]
    private RectTransform[] postLayouts;
    private int postIndex = 0;
    [SerializeField]
    private Image overlay;
    [SerializeField]
    private Image background;

    private int socialFeedPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(RectTransform postFrame in postFrames)
        {
            postFrame.gameObject.SetActive(false);
        }
        foreach (RectTransform postLayout in postLayouts)
        {
            postLayout.gameObject.SetActive(false);
        }
        postFrames[postIndex].gameObject.SetActive(true);
        postLayouts[postIndex].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScalePostFrame(Image image)
    {
        currentPostFrameDefaultPosition = image.rectTransform.anchoredPosition;
        currentPostFrameDefaultSize = image.rectTransform.sizeDelta;
        image.rectTransform.DOSizeDelta(currentPostFrameAlteredSize, tweenTime);
        image.rectTransform.DOAnchorPos(currentPostFrameAlteredPosition, tweenTime);
        StartCoroutine(ShowPostLayout(image, tweenTime));
        //Invoke(nameof(ShowPostLayout), tweenTime);
    }

    IEnumerator ShowPostLayout(Image image,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        image.gameObject.SetActive(false);
        postLayouts[postIndex].gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        overlay.gameObject.SetActive(true);
        
    }
   

    private void HidePostLayout()
    {
        postLayouts[postIndex].gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        overlay.gameObject.SetActive(false);
    }

    public void SelectPost(L4_BaseCarouselScript script)
    {
        //To be changed to sprite...
        var postImage = postFrames[postIndex].GetComponent<Image>();
        var postLayout  = postLayouts[postIndex].GetComponent<PostLayoutImages>();
        var postLayoutImage = postLayout.images[script.currentIndex].gameObject.GetComponent<ImagePointClass>();
        postImage.color = script.images[script.currentIndex].color; //Remember to change this to sprites when the images are ready. 
        postImage.gameObject.SetActive(true);
        HidePostLayout();
        socialFeedPoints += postLayoutImage.imageScore;
        print("Current Social Feed Score: " + socialFeedPoints);
        //Disable "EventTrigger" component on current post frame...
        postFrames[postIndex].GetComponent<EventTrigger>().enabled = false;
        postFrames[postIndex].DOSizeDelta(currentPostFrameDefaultSize, tweenTime);
        postFrames[postIndex].DOAnchorPos(currentPostFrameDefaultPosition, tweenTime);
        Invoke(nameof(ShowNextPostFrame), tweenTime);
    }

    private void ShowNextPostFrame()
    {
        if (postIndex < postFrames.Length - 1)
        {
            postIndex++;
            postFrames[postIndex].gameObject.SetActive(true);
        }
    }
}
