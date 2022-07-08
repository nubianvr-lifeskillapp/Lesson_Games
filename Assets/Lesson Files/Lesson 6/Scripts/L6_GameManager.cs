using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class L6_GameManager : MonoBehaviour
{
    //Properties...
    [SerializeField]
    private Bubble bubblePrefab;
    [SerializeField]
    private List<Bubble> bubbles;
    [SerializeField]
    private RectTransform conversationScreen;
    [SerializeField]
    private Vector3 spawnLocation1;
    [SerializeField]
    private Vector3 spawnLocation2;

    [HideInInspector]
    public bool gameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        Bubble bubble = Instantiate<Bubble>(bubblePrefab, spawnLocation1, Quaternion.identity ,conversationScreen);
        //Instantiate<Bubble>()
        bubbles.Add(bubble);
        //bubbles[0].transform.position = spawnLocation1;
        bubbles[0].transform.DOMoveY(-200.0f, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
