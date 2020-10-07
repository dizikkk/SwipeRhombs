using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager _taskManagerInst;

    [SerializeField] private string changeLevelText;

    [SerializeField] private int countFinishRhomb;
    [SerializeField] private int needFinishRhombToWin;

    public int CountFinishRhomb { get => countFinishRhomb; set => countFinishRhomb = value; }

    // Start is called before the first frame update
    void Start()
    {
        _taskManagerInst = this;
        DoTweenManager._DoTweenManagerInst.ChangeLevelText.text = changeLevelText;
    }

    // Update is called once per frame
    void Update()
    {
        if (countFinishRhomb == needFinishRhombToWin)
            FinishLevel();
    }

    public void FinishLevel()
    {
        DoTweenManager._DoTweenManagerInst.HideLevel();
        StartCoroutine(ChangeLevel());
    }

    IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(1f);
        LevelManager._levelManagerInst.SwipeLevel();
    }
}
