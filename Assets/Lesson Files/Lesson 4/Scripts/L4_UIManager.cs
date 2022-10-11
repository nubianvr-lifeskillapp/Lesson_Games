using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class L4_UIManager : MonoBehaviour
{
    [Header("Tween Properties")]
    [SerializeField]
    private Vector2 currentPostFrameAlteredSize = new Vector2();
    [SerializeField]
    private float tweenTime = 0.5f;



    [Header("Layout Properties")]

    [SerializeField]
    private PostFrameClass[] postFrames;
    [SerializeField]
    private CanvasGroup overlay;

    [SerializeField] private Button continueBtn;
    

    [Header("Post Frame Prefab")] [SerializeField]
    private GameObject postFrame;

    private PostFrameClass currentPostFrame;

    private GameObject newPostFrameInstance;

    private int PlayerPoints = 0; 
    
    // Start is called before the first frame update
    void Start()
    {
        continueBtn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnableOverlay()
    {
        overlay.gameObject.SetActive(true);
        overlay.DOFade(1, 0.5f);
        overlay.interactable = true;
    }

    private void DisableOverlay()
    {
        overlay.interactable = false;
        overlay.DOFade(0, 0.5f);
        
        Invoke("HideOverLay", 0.6f);
    }

    private void HideOverLay()
    {
        overlay.gameObject.SetActive(false);
    }

    public void ScalePostFrame(PostFrameClass @object)
    {
        if (@object.publicImageReviewed) return;
        currentPostFrame = @object;
        newPostFrameInstance = Instantiate(postFrame, @currentPostFrame.gameObject.transform.position, @currentPostFrame.gameObject.transform.rotation);
        var postRect = newPostFrameInstance.GetComponent<RectTransform>();
        var postManager = newPostFrameInstance.GetComponent<PostFrameClass>();
        postManager.SetImage(@object.postFrameImage.sprite);
        postRect.sizeDelta = new Vector2(100,100);
        postRect.anchorMin = new Vector2(0.5f,0.61f);
        postRect.anchorMax = new Vector2(0.5f,0.61f);
        EnableOverlay();
        newPostFrameInstance.transform.parent = overlay.transform;
        postRect.DOAnchorPos(new Vector2(0,0),  tweenTime);
        postRect.DOSizeDelta(currentPostFrameAlteredSize, tweenTime);
        
    }
    

    private void CheckBtnPressed()
    {
        PlayerPoints += currentPostFrame.postFrame.imagePoint;
        currentPostFrame.publicImageReviewed = true;
        DisableOverlay();
        Destroy(newPostFrameInstance);
        CheckAllPost();
        
    }

    private void CheckAllPost()
    {
        for (int i = 0; i < postFrames.Length; i++)
        {
            if (!postFrames[i].publicImageReviewed)
            {
                return;
            }
            
        }
        continueBtn.gameObject.SetActive(true);
        
    }

    public void KeepBtnPressed()
    {
        CheckBtnPressed();
        currentPostFrame.gameObject.GetComponent<Image>().DOColor(new Color(0.2f, 0.2f, 0.2f, 255.0f), 0.75f);
    }

    public void RemoveBtnPressed()
    {
        CheckBtnPressed();
        currentPostFrame.gameObject.SetActive(false);
    }
}
