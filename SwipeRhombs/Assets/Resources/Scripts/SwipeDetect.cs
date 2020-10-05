using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Lean.Transition;

public class SwipeDetect : MonoBehaviour
{
    public static SwipeDetect _swipeDetectInst;

    float startTime;
    private float leftDist;
    private float rightDist;
    private float upDist;
    private float downDist;
    private float speed = 5000f;
    [SerializeField] private float SWIPE_THRESHOLD = 20f;

    private Vector2 fingerDown;
    private Vector2 fingerUp;

    private Rigidbody2D _rb;

    private bool detectSwipeOnlyAfterRelease = false;
    [SerializeField] private bool isMove;
    private bool leftMove;
    private bool rightMove;
    private bool upMove;
    private bool downMove;

    public bool IsMove { get => isMove; set => isMove = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _swipeDetectInst = this;
        isMove = true;
    }

    private void Start()
    {

    }
    
    void FixedUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;

                if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
                {
                    if (fingerDown.y - fingerUp.y > 0 && upMove && isMove)
                    {
                        transform.localPositionTransition(new Vector3(transform.position.x, upDist, -1), 0.5f);
                    }
                    else if (fingerDown.y - fingerUp.y < 0 && downMove && isMove)
                    {
                        transform.localPositionTransition(new Vector3(transform.position.x, downDist, -1), 0.5f);
                    }
                    fingerUp = fingerDown;
                }

                else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
                {
                    if (fingerDown.x - fingerUp.x > 0 && rightMove && isMove)
                    {
                        transform.localPositionTransition(new Vector3(rightDist, transform.position.y, -1), 0.5f);
                    }
                    else if (fingerDown.x - fingerUp.x < 0 && leftMove && isMove)
                    {
                        transform.localPositionTransition(new Vector3(leftDist, transform.position.y, -1), 0.5f);
                    }
                    fingerUp = fingerDown;
                }
            }

            float verticalMove()
            {
                return Mathf.Abs(fingerDown.y - fingerUp.y);
            }
            float horizontalValMove()
            {
                return Mathf.Abs(fingerDown.x - fingerUp.x);
            }
        }
    }

    public void OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NodeRhomb>())
        {
            NodeRhomb NR = collision.GetComponent<NodeRhomb>();

            leftDist = NR.LeftDist;
            rightDist = NR.RightDist;
            upDist = NR.UpDist;
            downDist = NR.DownDist;

            if (NR.LeftDir == 1)
                leftMove = true;
            if (NR.RightDir == 1)
                rightMove = true;
            if (NR.UpDir == 1)
                upMove = true;
            if (NR.DownDir == 1)
                downMove = true;
        }

        if (collision.tag == "StartGameRhomb")
        {
            transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, -1f);
            _rb.velocity = new Vector2(0f, 0f);
            StartCoroutine("StartGame");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        leftMove = false;
        rightMove = false;
        upMove = false;
        downMove = false;
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
