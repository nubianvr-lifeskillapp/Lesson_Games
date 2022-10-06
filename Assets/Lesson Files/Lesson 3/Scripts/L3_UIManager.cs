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
        SoundManager.soundManager.PlaySFX(value > 0 ? "PowerUp" : "PowerDown");

        float currentProgressBarValue = progressBar.value;
        progressBar.DOValue(currentProgressBarValue + value, 0.5f);
        StartCoroutine(value > 0 ? SetLockState(true, 0.6f) : SetLockState(false, 0.6f));
    }

    IEnumerator SetLockState(bool addoperator, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        print("Progress Bar Value: " + progressBar.value);
        switch (progressBar.value)
        {
            case 0f:
                progressBar.fillRect.GetComponent<Image>().DOColor(new Color(255/255,18/255,0/255,150),0.75f);
                lockHandle.rectTransform.DOAnchorPosY(addoperator ? 190 : 300, 0.5f);
                break;
            case 0.25f:
                if (addoperator)
                {
                    lockHandle.rectTransform.DOAnchorPosY( 190, 0.5f);
                    progressBar.fillRect.GetComponent<Image>().DOColor(new Color(255/255,18/255,0/255,150),0.75f);
                }
                else
                {
                    fingerprint.rectTransform.DOSizeDelta(new Vector2(0, 0), 0.5f);
                    progressBar.fillRect.GetComponent<Image>().DOColor(new Color(255/255,18/255,0/255,150),0.75f);
                }
                break;
            case 0.50f:
                if (addoperator)
                {
                    fingerprint.gameObject.SetActive(true);
                    fingerprint.rectTransform.DOSizeDelta(new Vector2(200, 200), 0.5f);
                    progressBar.fillRect.GetComponent<Image>().DOColor( new Color(255/255,255/255,0/255,150),0.75f);
                }
                else
                {
                    fingerprint.sprite = fingerprintStates[2];
                    progressBar.fillRect.GetComponent<Image>().DOColor( new Color(255/255,255/255,0/255,150),0.75f);
                }
                break;
            case 0.75f:
                progressBar.fillRect.GetComponent<Image>().DOColor(new Color(55/255,255/255,0/255,150),0.75f);
                fingerprint.sprite = fingerprintStates[0];
                break;
            case 1.0f:
                fingerprint.sprite = fingerprintStates[1];
                break;
        }
        
        canSelect = true;
       
    }

    public void ResetGameplayView()
    {
        lockHandle.rectTransform.DOAnchorPosY( 300, 0.5f);
        fingerprint.sprite = fingerprintStates[2];
        fingerprint.rectTransform.DOSizeDelta(new Vector2(0, 0), 0f);
    }

}
