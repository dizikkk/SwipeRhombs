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

    [SerializeField] private Image bckLevelPanel;

    [SerializeField] private GameObject freeTrialTextGO;
    [SerializeField] private GameObject chooseLevelHider;

    private bool isMenuOpen;

    public GameObject FreeTrialTextGO { get => freeTrialTextGO; set => freeTrialTextGO = value; }
    public Image BckLevelPanel { get => bckLevelPanel; set => bckLevelPanel = value; }
    public TextMeshProUGUI ChangeLevelText { get => changeLevelText; set => changeLevelText = value; }

    // Start is called before the first frame update

    private void Awake()
    {
        _DoTweenManagerInst = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SlideMenu()
    {
        if (!isMenuOpen)
        {
            if (chooseLevelHider.activeSelf == true)
                OpenMenu();
            else
                OpenChooseLevelMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    #region ChooseLevelMenu
    private void OpenChooseLevelMenu()
    {
        menu.DOAnchorPos(new Vector2(75f, -75f), 0.5f);
        slideBtn.DORotate(new Vector3(0f, 0f, -180f), 0.5f);
        isMenuOpen = true;
    }
    #endregion

    private void OpenMenu()
    {
        menu.DOAnchorPos(new Vector2(190f, -75f), 0.5f);
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
    }

    public void HideLevel()
    {
        CloseMenu();
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
        RetryHider();
        yield return new WaitForSeconds(0.7f);
        LevelManager._levelManagerInst.RestartLevel();
        CloseMenu();
    }
    #endregion
}
