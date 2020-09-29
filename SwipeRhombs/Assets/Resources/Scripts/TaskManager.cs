using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager _taskManagerInst;

    [SerializeField] private int countFinishRhomb;
    [SerializeField] private int needFinishRhombToWin;

    public int CountFinishRhomb { get => countFinishRhomb; set => countFinishRhomb = value; }

    // Start is called before the first frame update
    void Start()
    {
        _taskManagerInst = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (countFinishRhomb == needFinishRhombToWin)
            StartCoroutine("FinishLevel");
    }

    IEnumerator FinishLevel()
    {
        yield return new WaitForSeconds(1.5f);
        LevelManager._levelManagerInst.SwipeLevel = true;
    }
}
