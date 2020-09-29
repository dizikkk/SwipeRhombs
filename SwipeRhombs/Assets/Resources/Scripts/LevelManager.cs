using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _levelManagerInst;

    private GameObject curObjLevel;

    [SerializeField] private GameObject[] levels;

    public bool SwipeLevel { get; set; }
    [SerializeField] private int curLvl;

    [SerializeField] private float spawnPointX;

    private Vector2 topRightCorner;
    private Vector2 edgeVector;

    // Start is called before the first frame update
    void Start()
    {
        _levelManagerInst = this;
        //topRightCorner = new Vector2(1, 1);
        //edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        //spawnPointX = edgeVector.x * 2;
        curObjLevel = Instantiate(levels[curLvl], levels[curLvl].transform.position, Quaternion.identity);
        //curObjLevel.transform.DOMoveX(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

        if (curLvl == 3)
            Camera.main.orthographicSize = 75;

        if (SwipeLevel)
        {
            curObjLevel.SetActive(false);
            Destroy(curObjLevel);
            curLvl++;
            if (curLvl < levels.Length)
                curObjLevel = Instantiate(levels[curLvl], levels[curLvl].transform.position, Quaternion.identity);
            SwipeLevel = false;
        }

        if (curLvl < levels.Length)
            DoTweenManager._DoTweenManagerInst.HideFreeTrialText();
        else
            DoTweenManager._DoTweenManagerInst.ShowFreeTrialText();
    }

    public void RestartLevel()
    {
        Destroy(curObjLevel);
        curObjLevel = Instantiate(levels[curLvl], levels[curLvl].transform.position, Quaternion.identity);
    }
}
