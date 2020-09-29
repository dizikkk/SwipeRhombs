using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeFingerAnim : MonoBehaviour
{
    private Animator anim;

    private bool startAnim;

    private float lastTime;
    [SerializeField] private float timeToAnim;

    void Start()
    {
        anim = GetComponent<Animator>();
        startAnim = true;
    }

    void Update()
    {
        if (Time.time - lastTime > timeToAnim)
        {
            startAnim = true;
            lastTime = Time.time;
        }

        if(startAnim)
        { 
            anim.SetBool("isAnim", true);
            startAnim = false;
        }
        else anim.SetBool("isAnim", false);

    }
}
