using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    public delegate void ChangeLanguage(string language);

    public event ChangeLanguage changeLanguage;

    [SerializeField] private string[] acceptedLanguage;

    private string curText;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ChangeLanguageCorroutine");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AcceptLanguage(int indexOfLanguage)
    {
        curText = acceptedLanguage[indexOfLanguage];
        changeLanguage?.Invoke(curText);
        PlayerPrefs.SetString("language", curText);
    }

    public string CurrentLanguage()
    {
        return curText;
    }

    IEnumerator ChangeLanguageCorroutine()
    {
        yield return new WaitForSeconds(0.1f);
        if (PlayerPrefs.HasKey("language"))
        {
            curText = PlayerPrefs.GetString("language");
            changeLanguage?.Invoke(curText);
        }
        else
        {
            curText = "eng";
            changeLanguage?.Invoke(curText);
        }
        PlayerPrefs.SetString("language", curText);
    }
}