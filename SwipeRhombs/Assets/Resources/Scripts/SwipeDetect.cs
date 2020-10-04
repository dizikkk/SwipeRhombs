using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SwipeDetect : MonoBehaviour
{
    public static SwipeDetect _swipeDetectInst;

    private float speed = 5000f;

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
                        _rb.velocity = new Vector2(0f, speed * Time.fixedDeltaTime);
                    }
                    else if (fingerDown.y - fingerUp.y < 0 && downMove && isMove)
                    {
                        _rb.velocity = new Vector2(0f, -speed * Time.fixedDeltaTime);
                    }
                    fingerUp = fingerDown;
                }

                else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
                {
                    if (fingerDown.x - fingerUp.x > 0 && rightMove && isMove)
                    {
                        _rb.velocity = new Vector2(speed * Time.fixedDeltaTime, 0f);
                    }
                    else if (fingerDown.x - fingerUp.x < 0 && leftMove && isMove)
                    {
                        _rb.velocity = new Vector2(-speed * Time.fixedDeltaTime, 0f);
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
                transform.DOMove(new Vector3(collision.transform.position.x, collision.transform.position.y, -1f), 0.2f);
                _rb.velocity = new Vector2(0f, 0f);

                if (NR.leftDir == 1)
                    leftMove = true;
                if (NR.rightDir == 1)
                    rightMove = true;
                if (NR.upDir == 1)
                    upMove = true;
                if (NR.downDir == 1)
                    downMove = true;
            }

            if (collision.tag == "FinishRhomb")
            {
                transform.DOMove(new Vector3(collision.transform.position.x, collision.transform.position.y, -1f), 0.2f);
                _rb.velocity = new Vector2(0f, 0f);
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
