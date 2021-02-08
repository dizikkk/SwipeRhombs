using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class LevelRhomb : MonoBehaviour
{
    private Transform _levelRhombColor;
    private int _levelNumb;

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
