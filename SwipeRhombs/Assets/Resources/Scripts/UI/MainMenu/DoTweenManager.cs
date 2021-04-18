using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class DoTweenManager : MonoBehaviour
{
    public static DoTweenManager _DoTweenManagerInst;

    [SerializeField] private RectTransform menu;
    [SerializeField] private RectTransform slideBtn;
    [SerializeField] private RectTransform retryBtn;
    [SerializeField] private RectTransform freeTrialTextRect;

    [SerializeField] private TextMeshProUGUI retryLevelText;
    [SerializeField] private TextMeshProUGUI changeLevelText;

    [SerializeField] private CanvasGroup menuCanvas;

    [SerializeField] private Image bckLevelPanel;

    [SerializeField] private GameObject freeTrialTextGO;
    [SerializeField] private GameObject chooseLevelHider;

    [SerializeField] private GameObject loseLevelFromCount;

    private bool isMenuOpen;

    public GameObject FreeTrialTextGO { get => freeTrialTextGO; set => freeTrialTextGO = value; }
    public Image BckLevelPanel { get => bckLevelPanel; set => bckLevelPanel = value; }
    public TextMeshProUGUI ChangeLevelText { get => changeLevelText; set => changeLevelText = value; }
    public CanvasGroup MenuCanvas { get => menuCanvas; set => menuCanvas = value; }

    // Start is called before the first frame update

    private bool IsLastLevel()
    {
        return true;
    }

    private void Awake()
    {
        _DoTweenManagerInst = this;
    }
    void Start()
    {
        LevelManager._levelManagerInst.isLastLevel += IsLastLevel;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SlideMenu()
    {
        if (!isMenuOpen)
        {
            if (chooseLevelHider.activeSelf == false || freeTrialTextGO.activeSelf)
                OpenChooseLevelMenu();
            else
                OpenMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    #region ChooseLevelMenu
    private void OpenChooseLevelMenu()
    {
        Debug.LogError("ChooseLevel");
        menu.DOAnchorPos(new Vector2(95f, -75f), 0.5f);
        slideBtn.DORotate(new Vector3(0f, 0f, -180f), 0.5f);
        isMenuOpen = true;
    }
    #endregion

    private void OpenMenu()
    {
        Debug.LogError("def");
        menu.DOAnchorPos(new Vector2(230f, -75f), 0.5f);
        slideBtn.DORotate(new Vector3(0f, 0f, -180f), 0.5f);
        isMenuOpen = true;
    }
 
    private void CloseMenu()
    {
        menu.DOAnchorPos(new Vector2(-50f, -75f), 0.5f);
        slideBtn.DORotate(Vector3.zero, 0.5f);
        isMenuOpen = false;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    #region Trial Text
    public void ShowFreeTrialText()
    {
        menuCanvas.DOFade(1f, 0f);
        freeTrialTextGO.SetActive(true);
        freeTrialTextRect.DOAnchorPos(new Vector2(0f, 0f), 1f);
    }

    public void HideFreeTrialText()
    {
        freeTrialTextGO.SetActive(false);
        freeTrialTextRect.DOAnchorPos(new Vector2(0f, -1200f), 0f);
    }
    #endregion

    #region ChangeLevel
    public void ShowLevel()
    {
        bckLevelPanel.DOFade(0f, 1f);
        LoseLevel.Instance.IsMoving = true;
    }

    public void HideLevel()
    {
        CloseMenu();
        CountTurnUIText.Instance.HideCountTurn();
        if (!IsLastLevel())
            menuCanvas.DOFade(0f, 1f);
        bckLevelPanel.DOFade(1f, 1f).OnComplete(ShowChangeLevelText);
        StartCoroutine(HideChangeLevelText());
    }

    IEnumerator HideChangeLevelText()
    {
        yield return new WaitForSeconds(2f);
        changeLevelText.DOFade(0f, 1f).OnComplete(ShowLevel);
    }

    public void ShowChangeLevelText()
    {
        changeLevelText.DOFade(1f, 1f);
    }
    #endregion

    #region Retry
    public void Retry()
    {
        StartCoroutine("RetryCorrutine");
    }

    public void RetryHider()
    {
        bckLevelPanel.DOFade(1f, 1f).OnComplete(ShowRetryLevelText);
        StartCoroutine(HideRetryLevelText());
    }

    public void ShowRetryLevelText()
    {
        retryLevelText.DOFade(1f, 1f);
    }

    IEnumerator HideRetryLevelText()
    {
        yield return new WaitForSeconds(2f);
        retryLevelText.DOFade(0f, 1f).OnComplete(ShowLevel);
    }

    IEnumerator RetryCorrutine()
    {
        retryBtn.DORotate(new Vector3(0f, 0f, -720f), 1f);
        CountTurnUIText.Instance.HideCountTurn();
        RetryHider();
        yield return new WaitForSeconds(0.7f);
        LevelManager._levelManagerInst.RestartLevel();
        CloseMenu();
    }
    #endregion

    #region LoseLevel
    public void LoseLevelFromCount()
    {
        CountTurnUIText.Instance.HideCountTurn();
        menuCanvas.DOFade(0f, 1f);
        bckLevelPanel.DOFade(1f, 1f);
        StartCoroutine("ShowLoseLevel");
    }

    IEnumerator ShowLoseLevel()
    {
        yield return new WaitForSeconds(1f);
        LoseLevelFromTurn.Instance.CanvasGroup.DOFade(1f, 1f);
        LoseLevelFromTurn.Instance.IsTimerOn = true;
    }

    public void RestartLevelFromCountTurn()
    {
        LoseLevelFromTurn.Instance.CanvasGroup.DOFade(0f, 1f).OnComplete(ShowLevel);
        LoseLevelFromTurn.Instance.CanvasGroup.interactable = false;
        CountTurnUIText.Instance.ShowCountTurn(1f);
        
        LevelManager._levelManagerInst.RestartLevel();
    }
    #endregion
}