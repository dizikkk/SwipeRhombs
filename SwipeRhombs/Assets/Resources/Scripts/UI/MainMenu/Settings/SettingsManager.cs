using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    [SerializeField] private List<GameObject> languageRhombs;

    private string curText;

    private void Awake()
    {
        Instance = this;
    }   

    private void Start()
    {
        SelectLanguageColorOnStart();
    }

    public void SelectLanguageColor(int index)
    {
        for (int i = 0; i < languageRhombs.Count; i++)
        {
            var selectLanguage = languageRhombs[i].transform.Find("LanguageSelect");

            if (i == index)
                selectLanguage.transform.gameObject.SetActive(true);
            else
                selectLanguage.transform.gameObject.SetActive(false);
        }
    }

    public void SelectLanguageColorOnStart()
    {
        if (PlayerPrefs.HasKey("language"))
        {
            curText = PlayerPrefs.GetString("language");
            if (curText == "rus")
            {
                var selectLanguage = languageRhombs[0].transform.Find("LanguageSelect");
                selectLanguage.transform.gameObject.SetActive(true);
            }

            if (curText == "eng")
            {
                var selectLanguage = languageRhombs[1].transform.Find("LanguageSelect");
                selectLanguage.transform.gameObject.SetActive(true);
            }
        }
        else
        {
            var selectLanguage = languageRhombs[1].transform.Find("LanguageSelect");
            selectLanguage.transform.gameObject.SetActive(true);
        }
    }
}