using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _levelManagerInst;

    private GameObject curObjLevel;
    [SerializeField] private GameObject[] levels;

    private int countOfAccessLevels;
    private int curLvl;

    public int CurLvl { get => curLvl; set => curLvl = value; }

    // Start is called before the first frame update
    void Start()
    {
        _levelManagerInst = this;
        DoTweenManager._DoTweenManagerInst.HideFreeTrialText();
        DoTweenManager._DoTweenManagerInst.ShowLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartLevel(int curLevel)
    {
        curObjLevel = Instantiate(levels[curLevel], levels[curLevel].transform.position, Quaternion.identity);
        curLvl = curLevel;
        countOfAccessLevels = curLevel;
    }

    public void SwipeLevel()
    {
        countOfAccessLevels++;
        if (ChooseLevelManager.Instance.CheckToAccessLevelRhomb(countOfAccessLevels))
        {
            ChooseLevelManager.Instance.AccessToLevelRhombs(countOfAccessLevels);
            ChooseLevelManager.Instance.MoveConnectLines(curLvl);
        }

        curObjLevel.SetActive(false);
        Destroy(curObjLevel);
        curLvl++;
        if (curLvl < levels.Length)
        {
            curObjLevel = Instantiate(levels[curLvl], levels[curLvl].transform.position, Quaternion.identity);
        }
        else
        {
            DoTweenManager._DoTweenManagerInst.ChangeLevelText.text = " ";
            DoTweenManager._DoTweenManagerInst.ShowFreeTrialText();
        }
    }

    public void RestartLevel()
    {
        Destroy(curObjLevel);
        curObjLevel = Instantiate(levels[curLvl], levels[curLvl].transform.position, Quaternion.identity);
    }
}