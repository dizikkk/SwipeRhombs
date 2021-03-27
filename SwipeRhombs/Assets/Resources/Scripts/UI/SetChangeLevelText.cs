using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChangeLevelText : MonoBehaviour
{
    public static SetChangeLevelText Instance;

    private string currentText;
    [SerializeField] private string[] allTexts;

    private void Awake()
    {
        Instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeTextLanguage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTextLanguage()
    {
        if (LanguageManager.Instance.CurrentLanguage() == "rus")
        {
            currentText = "";
            currentText = allTexts[0];
        }
        else if (LanguageManager.Instance.CurrentLanguage() == "eng")
        {
            currentText = "";
            currentText = allTexts[1];
        }
    }

    public string ChangeLevelText()
    {
        return currentText;
    }
}