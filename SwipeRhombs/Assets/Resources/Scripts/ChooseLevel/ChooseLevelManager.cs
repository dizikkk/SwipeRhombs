using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;

public class ChooseLevelManager : MonoBehaviour
{
    public static ChooseLevelManager Instance;

    [SerializeField] private List<GameObject> connectLines;
    [SerializeField] private List<GameObject> levelRhombs;
    [SerializeField] private GameObject hider;

    private string accessLevelNumKey;

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
        hider.SetActive(true);
        for (int i = 0; i < levelRhombs.Count; i++)
        {
            levelRhombs[i].SetActive(false);
        }
    }

    public void ShowChooseLevel()
    {
        hider.SetActive(false);
        for (int i = 0; i < levelRhombs.Count; i++)
        {
            levelRhombs[i].SetActive(true);
        }
    }

    public void MoveConnectLines(int curLevel)
    {
        var connectLineData = connectLines[curLevel].GetComponent<ConnectLineData>();
        var indexLevel = curLevel;
        indexLevel++;
        if (curLevel < connectLines.Count)
        {
            if (indexLevel % 5 != 0)
            {
                if (connectLines[curLevel].transform.position.x % 10 != 0)
                {
                    if (indexLevel >= 6 && indexLevel <= 9)
                        connectLines[curLevel].transform.localPositionTransition_X(connectLines[curLevel].transform.position.x - 5f, 0f);
                    else
                        connectLines[curLevel].transform.localPositionTransition_X(connectLines[curLevel].transform.position.x + 5f, 0f);
                }
            }
            else
            {
                if (connectLines[curLevel].transform.position.y % 10 != 0)
                    connectLines[curLevel].transform.localPositionTransition_Y(connectLines[curLevel].transform.position.y - 5f, 0f);
            }
        }
        connectLineData.SaveConnectLineData();
    }
    
    public void LoadAccessLevelsData()
    {
        accessLevelNumKey = "accessLevelNumKey";
        var accessNumLevels = PlayerPrefs.GetInt(accessLevelNumKey) + 1;
        for (int i = 0; i < accessNumLevels; i++)
        {
            var levelRhombCollider = levelRhombs[i].GetComponent<PolygonCollider2D>();
            levelRhombCollider.enabled = true;
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
        var levelRhombCollider = levelRhombs[numbOfAccessRhomb].GetComponent<PolygonCollider2D>();
        levelRhombCollider.enabled = true;
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