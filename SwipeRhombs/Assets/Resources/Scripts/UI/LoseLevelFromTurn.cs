using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class LoseLevelFromTurn : MonoBehaviour
{
    public static LoseLevelFromTurn Instance;
    public delegate void AdClicked(float timeToTurnShow = 1f);
    public AdClicked adClicked;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private CanvasGroup adOfferCanvasGroup;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeForAd;
    private bool isTimerOn;
    private float timeLeft;

    public CanvasGroup CanvasGroup { get => canvasGroup; set => canvasGroup = value; }
    public bool IsTimerOn { get => isTimerOn; set => isTimerOn = value; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeForAd;
        canvasGroup.interactable = false;
    }

    public void SetStartTime()
    {
        timeLeft = timeForAd;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerOn)
        {
            int intToText = (int)timeLeft;
            adOfferCanvasGroup.DOFade(1f, 0f);
            canvasGroup.interactable = true;
            timeLeft -= Time.deltaTime;
            timerText.text = intToText.ToString();

            if (timeLeft <= 0)
            {
                isTimerOn = false;
                timeLeft = timeForAd;
                DoTweenManager._DoTweenManagerInst.RestartLevelFromCountTurn();
            }
        }
    }

    public void OnAdClick()
    {
        canvasGroup.DOFade(0f, 1f).OnComplete(DoTweenManager._DoTweenManagerInst.ShowLevel);
        canvasGroup.interactable = false;
        timeLeft = timeForAd;
        isTimerOn = false;
        adClicked?.Invoke(1f);
    }
}