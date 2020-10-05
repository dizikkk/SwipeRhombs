using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollisionRhombDetect : MonoBehaviour
{
    Rigidbody2D _rb;

    [SerializeField] private GameObject KillRhomb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CollisionRhombDetect>())
        {
            _rb.velocity = Vector2.zero;
            SwipeDetect._swipeDetectInst.IsMove = false;
            KillRhomb.transform.DOScale(new Vector2(1f, 1f), 0.5f);
            StartCoroutine("RestartCor");
        }
    }

    IEnumerator RestartCor()
    {
        yield return new WaitForSeconds(1f);
        LevelManager._levelManagerInst.RestartLevel();
    }
}
