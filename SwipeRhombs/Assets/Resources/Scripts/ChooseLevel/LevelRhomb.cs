using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Lean.Transition;

public class LevelRhomb : MonoBehaviour
{
    public static LevelRhomb Instance;
    [SerializeField] private GameObject connectLine;
    [SerializeField] private float unlockPositionX;
    [SerializeField] private float unlockPositionY;
    private Transform _levelRhombColor;
    private int _levelNumb;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _levelRhombColor = gameObject.transform.Find("LevelRhombWhite");
        _levelNumb = int.Parse(gameObject.transform.Find("Text (TMP)").GetComponent<TextMeshPro>().text) - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        _levelRhombColor.GetComponent<SpriteRenderer>().DOFade(1f, 0f);
    }

    private void OnMouseExit()
    {
        _levelRhombColor.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
    }

    public void SetConnectLineToUnlockPosition()
    {
        if (connectLine != null)
        {
            connectLine.transform.localPositionTransition_X(unlockPositionX, 0f);
            connectLine.transform.localPositionTransition_Y(unlockPositionY, 0f);
        }
    }

    private void OnMouseUpAsButton()
    {
        DoTweenManager._DoTweenManagerInst.HideLevel();
        StartCoroutine("StartLevel");
    }

    public IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(1f);
        ChooseLevelManager.Instance.HideChooseLevel();
        LevelManager._levelManagerInst.StartLevel(_levelNumb);
    }
}