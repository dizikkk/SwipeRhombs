using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _levelManagerInst;

    public delegate void OnStartLevel();
    public delegate void OnSwipeLevel();
    public event OnStartLevel startLevel;
    public event OnSwipeLevel swipeLevel;

    private GameObject curObjLevel;
    [SerializeField] private GameObject[] levels;

    private GameObject mainCamera;

    private int countOfAccessLevels;
    private int curLvl;

    public int CurLvl { get => curLvl; set => curLvl = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    void Start()
    {
        _levelManagerInst = this;
        DoTweenManager._DoTweenManagerInst.HideFreeTrialText();
        DoTweenManager._DoTweenManagerInst.ShowLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (ChooseLevelManager.Instance.Hider.activeSelf)
        {
            if (curLvl <= 4)
                mainCamera.transform.position = new Vector3(0f, -5f, -10f);
            else if (curLvl > 4)
                mainCamera.transform.position = new Vector3(0f, 0f, -10f);
        } 
        else mainCamera.transform.position = new Vector3(0f, 0f, -10f);
    }

    public void StartLevel(int curLevel)
    {
        curObjLevel = Instantiate(levels[curLevel], levels[curLevel].transform.position, Quaternion.identity);
        curLvl = curLevel;
        countOfAccessLevels = curLevel;
    }

    public void SwipeLevel()
    {
        if ((curLvl + 1) < levels.Length)
        {
            countOfAccessLevels++;
            if (ChooseLevelManager.Instance.CheckToAccessLevelRhomb(countOfAccessLevels))
            {
                ChooseLevelManager.Instance.AccessToLevelRhombs(countOfAccessLevels);
                ChooseLevelManager.Instance.MoveConnectLines(curLvl);
            }
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
        swipeLevel?.Invoke();
    }

    public void RestartLevel()
    {
        if (curLvl < levels.Length)
        {
            Destroy(curObjLevel);
            curObjLevel = Instantiate(levels[curLvl], levels[curLvl].transform.position, Quaternion.identity);
        }
    }
}