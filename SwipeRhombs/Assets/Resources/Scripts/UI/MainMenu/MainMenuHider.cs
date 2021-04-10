using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuHider : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup.alpha == 0f)
            canvasGroup.alpha = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.DOFade(0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
