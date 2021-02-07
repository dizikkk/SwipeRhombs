using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Transition;

public class CollisionRhombDetect : MonoBehaviour
{
    [SerializeField] private GameObject KillRhomb;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CollisionRhombDetect>())
        {
            transform.localPositionTransition(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0f);
            SwipeDetect._swipeDetectInst.IsMove = false;
            KillRhomb.transform.DOScale(new Vector2(1f, 1f), 0.5f);
            StartCoroutine("RestartCor");
        }
    }

    IEnumerator RestartCor()
    {
        yield return new WaitForSeconds(0.5f);
        DoTweenManager._DoTweenManagerInst.Retry();
    }
}
