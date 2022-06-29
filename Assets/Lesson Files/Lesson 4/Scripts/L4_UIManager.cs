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
        Invoke(nameof(ShowPostLayout), tweenTime);
    }

    private void ShowPostLayout()
    {
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
        postFrames[postIndex].GetComponent<Image>().color = script.images[script.currentIndex].color;
        HidePostLayout();
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
