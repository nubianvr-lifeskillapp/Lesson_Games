using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Bubble : MonoBehaviour
{
    //Main Properties...
    
    private new RectTransform transform;
    [HideInInspector]
    public float upValue = -50.0f;

    public Sprite SenderBubble;

    public Sprite ReceiverBubble;

    public TMP_Text message;

    public HorizontalLayoutGroup HorizontalLayout;

    // Start is called before the first frame update
    void Start()
    {
        transform = gameObject.GetComponent<RectTransform>();
        MoveBubbleUp(upValue);
    }

    public void MoveBubbleUp(float value)
    {
        transform.DOAnchorPosY(value, 0.75f);
    }
}