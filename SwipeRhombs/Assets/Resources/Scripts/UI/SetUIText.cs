using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetUIText : MonoBehaviour
{
    public static SetUIText Instance;

    [SerializeField] private string[] allTexts;
    private TextMeshProUGUI UItext;
    private TextMeshPro text;

    public enum ChooseText { UGUItext, TMPROtext}
    [SerializeField] private ChooseText chooseText;
    private string currentText;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;    
    }

    void Start()
    {
        UItext = transform.gameObject.GetComponent<TextMeshProUGUI>();
        text = transform.gameObject.GetComponent<TextMeshPro>();
        LanguageManager.Instance.changeLanguage += SetTextLanguage;

        if (chooseText == ChooseText.UGUItext)
            UItext.text = "";
        if (chooseText == ChooseText.TMPROtext)
            text.text = "";

        ChangeTextLanguage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTextLanguage(string language)
    {
        if (language == "rus")
        {
            text.text = allTexts[0];
        }
        else if (language == "eng")
            text.text = allTexts[1];
    }

    public void ChangeTextLanguage()
    {
        if (chooseText == ChooseText.UGUItext && UItext != null)
        {
            if (LanguageManager.Instance.CurrentLanguage() == "rus")
            {
                UItext.text = "";
                UItext.text = allTexts[0];
            }
            else if (LanguageManager.Instance.CurrentLanguage() == "eng")
            {
                UItext.text = "";
                UItext.text = allTexts[1];
            }
        }
        else if (chooseText == ChooseText.TMPROtext && text != null)
        {
            if (LanguageManager.Instance.CurrentLanguage() == "rus")
            {
                text.text = "";
                text.text = allTexts[0];
            }
            else if (LanguageManager.Instance.CurrentLanguage() == "eng")
            {
                text.text = "";
                text.text = allTexts[1];
            }
        }
    }

    public string ChangeLevelText()
    {
        currentText = text.text;
        return currentText;
    }
}