using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CountTurnUIText : MonoBehaviour
{
    public static CountTurnUIText Instance;

    [SerializeField] private TextMeshProUGUI countTurnText;

    [SerializeField] private CanvasGroup canvasGroup;

    private float timeToShow;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoseLevelFromTurn.Instance.adClicked += ShowCountTurn;
    }

    public void ShowCountTurn(float timeToShow)
    {
        StartCoroutine(ShowCountTurnCor(timeToShow));
    }

    public void HideCountTurn()
    {
        canvasGroup.DOFade(0f, 1f);
    }

    public void UpdateCountTurn(int turnCount)
    {
        countTurnText.text = turnCount.ToString();
    }

    IEnumerator ShowCountTurnCor(float timeToShow)
    {
        yield return new WaitForSeconds(timeToShow);
        TaskManager._taskManagerInst.UpdateTurnCount();
        DoTweenManager._DoTweenManagerInst.MenuCanvas.DOFade(1f, 1f);
        canvasGroup.DOFade(1f, 1f);
    }
}