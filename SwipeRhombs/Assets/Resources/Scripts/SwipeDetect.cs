using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Lean.Transition;

public class SwipeDetect : MonoBehaviour
{
    public static SwipeDetect _swipeDetectInst;

    [SerializeField] private float leftDist;
    [SerializeField] private float rightDist;
    [SerializeField] private float upDist;
    [SerializeField] private float downDist;

    private Vector2 startMousePos;
    private Vector2 endMousePos;
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField] private float SWIPE_THRESHOLD = 20f;

    private Rigidbody2D _rb;

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
        #region ANDROID
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
                        StartCoroutine("OnCountCorrutine");
                        isMove = false;
                    }
                    else if (fingerDown.y - fingerUp.y < 0 && downMove && isMove)
                    {
                        transform.localPositionTransition(new Vector3(transform.position.x, downDist, -2), 0.5f);
                        StartCoroutine("OnCountCorrutine");
                        isMove = false;
                    }
                    fingerUp = fingerDown;
                }

                else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
                {
                    if (fingerDown.x - fingerUp.x > 0 && rightMove && isMove)
                    {
                        transform.localPositionTransition(new Vector3(rightDist, transform.position.y, -2), 0.5f);
                        StartCoroutine("OnCountCorrutine");
                        isMove = false;
                    }
                    else if (fingerDown.x - fingerUp.x < 0 && leftMove && isMove)
                    {
                        transform.localPositionTransition(new Vector3(leftDist, transform.position.y, -2), 0.5f);
                        StartCoroutine("OnCountCorrutine");
                        isMove = false;
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
        #endregion

        #region WEBGL
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition;
            endMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endMousePos = Input.mousePosition;
            CheckToMoveFromMouse();
        }
        #endregion
    }

    public void OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CheckToMoveFromMouse()
    {
        float verticalMove()
        {
            return Mathf.Abs(startMousePos.y - endMousePos.y);
        }
        float horizontalValMove()
        {
            return Mathf.Abs(startMousePos.x - endMousePos.x);
        }

        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            if (startMousePos.y - endMousePos.y < 0 && upMove && isMove)
            {
                transform.localPositionTransition(new Vector3(transform.position.x, upDist, transform.position.z), 0.5f);
                StartCoroutine("OnCountCorrutine");
                isMove = false;
            }
            else if (startMousePos.y - endMousePos.y > 0 && downMove && isMove)
            {
                transform.localPositionTransition(new Vector3(transform.position.x, downDist, transform.position.z), 0.5f);
                StartCoroutine("OnCountCorrutine");
                isMove = false;
            }
        }

        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            if (startMousePos.x - endMousePos.x < 0 && rightMove && isMove)
            {
                transform.localPositionTransition(new Vector3(rightDist, transform.position.y, transform.position.z), 0.5f);
                StartCoroutine("OnCountCorrutine");
                isMove = false;
            }
            else if (startMousePos.x - endMousePos.x > 0 && leftMove && isMove)
            {
                transform.localPositionTransition(new Vector3(leftDist, transform.position.y, transform.position.z), 0.5f);
                StartCoroutine("OnCountCorrutine");
                isMove = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NodeRhomb>())
        {
            StartCoroutine("TimeToMove");

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
            StartCoroutine("StartGame");
        }

        if (collision.tag == "SettingsRhomb")
        {
            StartCoroutine("GameSettings");
        }
        
        if (collision.tag == "ToMainMenu")
        {
            StartCoroutine("ToMainMenu");
        }
    }

    private IEnumerator TimeToMove()
    {
        yield return new WaitForSeconds(0.1f);
        isMove = true;
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
        SceneManager.LoadScene(2);
    }

    IEnumerator GameSettings()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(3);
    }

    IEnumerator ToMainMenu()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    IEnumerator OnCountCorrutine()
    {
        yield return new WaitForSeconds(0.5f);
        if (TaskManager._taskManagerInst != null)
            TaskManager._taskManagerInst.IsTurn = true;
    }
}