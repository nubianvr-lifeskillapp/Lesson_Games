using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class L3_UIManager : MonoBehaviour
{
    //Main Properties...
    [SerializeField]
    private L3_GameManager gameManager;
    public TMP_Text[] optionTexts;
    public Button[] optionButtons;
    public Slider progressBar;
    [SerializeField]
    private Image lockHandle;
    [SerializeField]
    private Image fingerprint;
    [SerializeField]
    private Sprite[] fingerprintStates;
    [HideInInspector]
    public bool canSelect = true;

    // Start is called before the first frame update
    void Start()
    {
        progressBar.value = 0;
       
        fingerprint.gameObject.SetActive(false);

        //if (gameManager.isReloaded == true)
        //{
        //    gameManager.replayDialogUI.ShowDialog();
        //    Debug.Log("Showing Replay Dialog");
        //    gameManager.isReloaded = false;
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncrementProgressBar(float value)
    {
        float currentProgressBarValue = progressBar.value;
        Tween tween = progressBar.DOValue(currentProgressBarValue + value, 0.5f);
        Invoke(nameof(SetLockState), 0.6f);
    }

    private void SetLockState()
    {
        print("Progress Bar Value: " + progressBar.value);
        if (progressBar.value >= 0.25f)
        {
            lockHandle.rectTransform.DOAnchorPosY(190, 0.5f);
        }
        if (progressBar.value >= 0.5f)
        {
            fingerprint.gameObject.SetActive(true);
            Tween tween = fingerprint.rectTransform.DOSizeDelta(new Vector2(190, 200), 0.5f);
        }
        if (progressBar.value >= 0.75f)
        {
            fingerprint.sprite = fingerprintStates[0]; 
            
        }
        if (progressBar.value >= 1.0f)
        {
            fingerprint.sprite = fingerprintStates[1];
        }
        canSelect = true;
    }

}
