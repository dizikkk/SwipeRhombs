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

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoseLevelFromTurn.Instance.adClicked += ShowCountTurn;
    }

    public void ShowCountTurn()
    {
        StartCoroutine("ShowCountTurnCor");
    }

    public void HideCountTurn()
    {
        canvasGroup.DOFade(0f, 1f);
    }

    public void UpdateCountTurn(int turnCount)
    {
        countTurnText.text = turnCount.ToString();
    }

    IEnumerator ShowCountTurnCor()
    {
        yield return new WaitForSeconds(2f);
        TaskManager._taskManagerInst.UpdateTurnCount();
        canvasGroup.DOFade(1f, 1f);
    }
}