using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;

public class ChooseLevelManager : MonoBehaviour
{
    public static ChooseLevelManager Instance;

    [SerializeField] private List<GameObject> connectLines;
    [SerializeField] private List<LevelRhomb> levelRhombs;
    [SerializeField] private GameObject hider;

    private string accessLevelNumKey;

    public GameObject Hider { get => hider; set => hider = value; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("accessLevelNumKey"))
            LoadAccessLevelsData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideChooseLevel()
    {
        //hider.SetActive(true);
        for (int i = 0; i < levelRhombs.Count; i++)
        {
            levelRhombs[i].gameObject.SetActive(false);
        }
    }

    public void ShowChooseLevel()
    {
        hider.SetActive(false);
        for (int i = 0; i < levelRhombs.Count; i++)
        {
            levelRhombs[i].gameObject.SetActive(true);
        }
    }

    /*public void MoveConnectLines(int curLevel)
    {
        var connectLineData = connectLines[curLevel].GetComponent<ConnectLineData>();
        var indexLevel = curLevel;
        indexLevel++;
        if (curLevel < connectLines.Count)
        {
            if (indexLevel % 3 != 0)
            {
                if (connectLines[curLevel].transform.position.x % 5 != 0)
                {
                    if (levelRhombs[curLevel].GetComponent<LevelRhomb>().isLeftDirection())
                        connectLines[curLevel].transform.localPositionTransition_X(connectLines[curLevel].transform.position.x - 5f, 0f);
                    else
                        connectLines[curLevel].transform.localPositionTransition_X(connectLines[curLevel].transform.position.x + 5f, 0f);
                }
            }
            else
            {
                if (connectLines[curLevel].transform.position.y % 5 != 0)
                    connectLines[curLevel].transform.localPositionTransition_Y(connectLines[curLevel].transform.position.y - 5f, 0f);
            }
        }
        connectLineData.SaveConnectLineData();
    }*/
    
    public void LoadAccessLevelsData()
    {
        accessLevelNumKey = "accessLevelNumKey";
        var accessNumLevels = PlayerPrefs.GetInt(accessLevelNumKey) + 1;
        for (int i = 0; i < accessNumLevels; i++)
        {
            var levelRhomb = levelRhombs[i].GetComponent<LevelRhomb>();
            var levelRhombCollider = levelRhombs[i].GetComponent<PolygonCollider2D>();
            levelRhombCollider.enabled = true;

            if (i < accessNumLevels - 1)
                levelRhomb.SetConnectLineToUnlockPosition();
        }
    }

    public void SaveAccessLevelsData(int accessNumLevels)
    {
        accessLevelNumKey = "accessLevelNumKey";
        PlayerPrefs.SetInt(accessLevelNumKey, accessNumLevels);
        PlayerPrefs.Save();
    }

    public void AccessToLevelRhombs(int numbOfAccessRhomb)
    {
        var levelRhomb = levelRhombs[numbOfAccessRhomb]; ;
        levelRhomb.GetComponent<Collider2D>().enabled = true;
        SaveAccessLevelsData(numbOfAccessRhomb);
    }

    public bool CheckToAccessLevelRhomb(int numbOfAccessRhomb)
    {
        var levelRhombCollider = levelRhombs[numbOfAccessRhomb].GetComponent<PolygonCollider2D>();
        if (!levelRhombCollider.enabled)
            return true;
        else return false;
    }
}