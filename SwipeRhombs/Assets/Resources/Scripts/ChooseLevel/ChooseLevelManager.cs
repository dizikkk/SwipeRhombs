using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;

public class ChooseLevelManager : MonoBehaviour
{
    public static ChooseLevelManager Instance;

    [SerializeField] private List<GameObject> connectLines;
    [SerializeField] private List<GameObject> levelRhombs;

    private void Awake()
    {
        Instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideChooseLevel()
    {
        gameObject.SetActive(false);
    }

    public void ShowChooseLevel()
    {
        gameObject.SetActive(true);
    }

    public void MoveConnectLines(int curLevel)
    {
        var indexLevel = curLevel;
        indexLevel++;
        if (curLevel < connectLines.Count)
        {
            if (indexLevel % 5 != 0)
            {
                if (indexLevel >= 6 && indexLevel <= 9)
                    connectLines[curLevel].transform.localPositionTransition_X(connectLines[curLevel].transform.position.x - 5f, 0f);
                else
                    connectLines[curLevel].transform.localPositionTransition_X(connectLines[curLevel].transform.position.x + 5f, 0f);
            }
            else
            {
                connectLines[curLevel].transform.localPositionTransition_Y(connectLines[curLevel].transform.position.y - 5f, 0f);
            }
        }
        PlayerPrefs.Save();
    }

    public void AccessToLevelRhombs(int numbOfAccessRhomb)
    {
        var levelRhombCollider = levelRhombs[numbOfAccessRhomb].GetComponent<PolygonCollider2D>();
        levelRhombCollider.enabled = true;
        PlayerPrefs.Save();
    }
}