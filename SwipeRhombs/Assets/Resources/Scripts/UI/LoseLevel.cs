using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseLevel : MonoBehaviour
{
    public static LoseLevel Instance;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject retryBtn;
    private bool isMoving;

    public bool IsMoving { get => isMoving; set => isMoving = value; }

    private void Awake()
    {
        Instance = this;
        IsMoving = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
            SwipeDetect._swipeDetectInst.IsMove = false;
        else
            SwipeDetect._swipeDetectInst.IsMove = true;
    }

    public void LevelLose()
    {
        isMoving = false;
        StartCoroutine("LoseLevelCorroutine");
    }

    IEnumerator LoseLevelCorroutine()
    {
        yield return new WaitForSeconds(1f);
        DoTweenManager._DoTweenManagerInst.LoseLevelFromCount();
    }
}
