using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LanguageRhomb : MonoBehaviour
{
    [SerializeField] private Transform settingsRhombColor;
    [SerializeField] private string language;
    [SerializeField] private int index;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        settingsRhombColor.GetComponent<SpriteRenderer>().DOFade(1f, 0f);
    }

    private void OnMouseExit()
    {
        settingsRhombColor.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
    }

    private void OnMouseUpAsButton()
    {
        LanguageManager.Instance.AcceptLanguage(index);
        SettingsManager.Instance.SelectLanguageColor(index);
    }
}
