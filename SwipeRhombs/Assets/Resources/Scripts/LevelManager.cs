using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Diagnostics;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _levelManagerInst;

    private GameObject curObjLevel;
    [SerializeField] private GameObject[] levels;

    [SerializeField] private int curLvl;

    // Start is called before the first frame update
    void Start()
    {
        _levelManagerInst = this;
        DoTweenManager._DoTweenManagerInst.HideFreeTrialText();
        curObjLevel = Instantiate(levels[curLvl], levels[curLvl].transform.position, Quaternion.identity);
        DoTweenManager._DoTweenManagerInst.ShowLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (curLvl == 3)
            Camera.main.orthographicSize = 75;
    }

    public void SwipeLevel()
    {
        curObjLevel.SetActive(false);
        Destroy(curObjLevel);
        curLvl++;
        DoTweenManager._DoTweenManagerInst.ShowLevel();
        if (curLvl < levels.Length)
            curObjLevel = Instantiate(levels[curLvl], levels[curLvl].transform.position, Quaternion.identity);
        else
            DoTweenManager._DoTweenManagerInst.ShowFreeTrialText();
    }

    public void RestartLevel()
    {
        Destroy(curObjLevel);
        curObjLevel = Instantiate(levels[curLvl], levels[curLvl].transform.position, Quaternion.identity);
    }
}
