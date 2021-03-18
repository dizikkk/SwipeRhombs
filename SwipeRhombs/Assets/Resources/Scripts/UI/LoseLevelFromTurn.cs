using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoseLevelFromTurn : MonoBehaviour
{
    public static LoseLevelFromTurn Instance;
    public delegate void AdClicked();
    public AdClicked adClicked;

    [SerializeField] private CanvasGroup canvasGroup;

    public CanvasGroup CanvasGroup { get => canvasGroup; set => canvasGroup = value; }

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

    public void OnAdClick()
    {
        canvasGroup.DOFade(0f, 1f);
        LoseLevel.Instance.IsMoving = true;
        adClicked?.Invoke();
    }
}