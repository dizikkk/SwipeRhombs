using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager _taskManagerInst;

    [SerializeField] private int countFinishRhomb;
    [SerializeField] private int needFinishRhombToWin;

    private SetChangeLevelText changeLevelText;

    public int CountFinishRhomb { get => countFinishRhomb; set => countFinishRhomb = value; }

    // Start is called before the first frame update
    void Start()
    {
        changeLevelText = transform.gameObject.GetComponent<SetChangeLevelText>();
        _taskManagerInst = this;
        StartCoroutine("SetChangeLevelText");
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
}