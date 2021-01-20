using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;
using DG.Tweening;

public class LetRhomb : MonoBehaviour
{
    private NodeRhomb _nodeRhombScript;

    [SerializeField] private GameObject rhombToUnlet;
    [SerializeField] private GameObject unletRhomb;
    [SerializeField] private int leftDir;
    [SerializeField] private int rightDir;
    [SerializeField] private int upDir;
    [SerializeField] private int downDir;

    [SerializeField] private float leftDist;
    [SerializeField] private float rightDist;
    [SerializeField] private float upDist;
    [SerializeField] private float downDist;

    private Transform letRhombChild;
    [SerializeField] private GameObject connectLine;
    [SerializeField] private Vector3 connectLinePos;

    // Start is called before the first frame update
    void Start()
    {
        _nodeRhombScript = unletRhomb.GetComponent<NodeRhomb>();
        letRhombChild = transform.Find("LetRhombAnim");
        letRhombChild.position = new Vector3(transform.position.x, transform.position.y, -1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>() == rhombToUnlet.GetComponent<Collider2D>())
        {
            _nodeRhombScript.LeftDir = leftDir;
            _nodeRhombScript.RightDir = rightDir;
            _nodeRhombScript.UpDir = upDir;
            _nodeRhombScript.DownDir = downDir;

            _nodeRhombScript.LeftDist = leftDist;
            _nodeRhombScript.RightDist = rightDist;
            _nodeRhombScript.UpDist = upDist;
            _nodeRhombScript.DownDist = downDist;

            DoTween();
        }
    }

    private void DoTween()
    {
        Transform connectLineChild = connectLine.transform.Find("ConnectLineChild");
        connectLineChild.position = new Vector3(connectLine.transform.position.x, connectLine.transform.position.y, -1f);
        connectLineChild.transform.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
        connectLine.transform.DOLocalMove(connectLinePos, 1f);
        letRhombChild.transform.GetComponent<SpriteRenderer>().DOFade(0f, 1f);
    }
}
