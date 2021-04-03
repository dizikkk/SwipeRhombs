using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager _taskManagerInst;

    [SerializeField] private int countFinishRhomb;
    [SerializeField] private int needFinishRhombToWin;
    [SerializeField] private int turnCount;
    [SerializeField] private float countOfColumnOnLevel;

    private bool isTurn;

    private const float NORMAL_CAMERA_DISTANCE = 45f;

    private SetChangeLevelText changeLevelText;

    public delegate void OnTurn();
    public event OnTurn onTurn;

    public int CountFinishRhomb { get => countFinishRhomb; set => countFinishRhomb = value; }
    public int TurnCount { get => turnCount; set => turnCount = value; }
    public bool IsTurn { get => isTurn; set => isTurn = value; }

    // Start is called before the first frame update
    void Start()
    {
        _taskManagerInst = this;

        SetCameraDistance();

        CountTurnUIText.Instance.UpdateCountTurn(turnCount);
        CountTurnUIText.Instance.ShowCountTurn(2f);

        changeLevelText = transform.gameObject.GetComponent<SetChangeLevelText>();
        StartCoroutine("SetChangeLevelText");

        onTurn += ChangeTurnCount;
        LoseLevelFromTurn.Instance.adClicked += AddTurns;
    }

    void Update()
    {
        if (countFinishRhomb == needFinishRhombToWin && turnCount >= 0)
            FinishLevel();

        if (isTurn)
        {
            onTurn?.Invoke();
            isTurn = false;
        }
    }

    private void ChangeTurnCount()
    {
        turnCount--;
        UpdateTurnCount();
    }

    public void UpdateTurnCount()
    {
        CountTurnUIText.Instance.UpdateCountTurn(turnCount);

        if (turnCount <= 0 && countFinishRhomb != needFinishRhombToWin)
            LoseLevel.Instance.LevelLose();
    }

    public void FinishLevel()
    {
        Debug.LogError("finish");
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

    public void AddTurns(float a)
    {
        turnCount += 5;
    }

    private void SetCameraDistance()
    {
        var mainCamera = Camera.main;

        if (countOfColumnOnLevel == 3)
            mainCamera.orthographicSize = NORMAL_CAMERA_DISTANCE;
        else if (countOfColumnOnLevel == 4)
            mainCamera.orthographicSize = 50f;
        else if (countOfColumnOnLevel == 5)
            mainCamera.orthographicSize = 65f;
        else if (countOfColumnOnLevel == 6)
            mainCamera.orthographicSize = 75f;
    }
}