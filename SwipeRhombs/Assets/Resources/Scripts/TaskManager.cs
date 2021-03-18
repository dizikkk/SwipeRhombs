using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager _taskManagerInst;

    [SerializeField] private int countFinishRhomb;
    [SerializeField] private int needFinishRhombToWin;
    [SerializeField] private int turnCount;

    private SetChangeLevelText changeLevelText;

    public int CountFinishRhomb { get => countFinishRhomb; set => countFinishRhomb = value; }
    public int TurnCount { get => turnCount; set => turnCount = value; }

    // Start is called before the first frame update
    void Start()
    {
        _taskManagerInst = this;
        CountTurnUIText.Instance.UpdateCountTurn(turnCount);
        CountTurnUIText.Instance.ShowCountTurn();
        changeLevelText = transform.gameObject.GetComponent<SetChangeLevelText>();
        StartCoroutine("SetChangeLevelText");
        SwipeDetect._swipeDetectInst.onTurn += ChangeTurnCount;
        LoseLevelFromTurn.Instance.adClicked += AddTurns;
    }

    void Update()
    {
        if (countFinishRhomb == needFinishRhombToWin)
            FinishLevel();
    }

    private void ChangeTurnCount()
    {
        turnCount--;
        UpdateTurnCount();
    }

    public void UpdateTurnCount()
    {
        CountTurnUIText.Instance.UpdateCountTurn(turnCount);

        if (turnCount <= 0)
            LoseLevel.Instance.LevelLose();
    }

    public void FinishLevel()
    {
        DoTweenManager._DoTweenManagerInst.HideLevel();
        StartCoroutine(ChangeLevel());
    }

    IEnumerator SetChangeLevelText()
    {
        DoTweenManager._DoTweenManagerInst.ChangeLevelText.text = "";
        yield return new WaitForSeconds(0.1f);
        DoTweenManager._DoTweenManagerInst.ChangeLevelText.text = changeLevelText.ChangeLevelText();
    }

    IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(1f);
        LevelManager._levelManagerInst.SwipeLevel();
    }

    public void AddTurns()
    {
        turnCount += 5;
    }
}