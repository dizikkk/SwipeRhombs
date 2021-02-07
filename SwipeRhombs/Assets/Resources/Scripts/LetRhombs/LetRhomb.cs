using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;
using DG.Tweening;

public class LetRhomb : MonoBehaviour
{
    private NodeRhomb _nodeRhombScript;
    private LetRhomb _letRhomb;

    public List<LetRhombValues> listOfValues = new List<LetRhombValues>(); 

    [SerializeField] private GameObject rhombToUnlet;
    [SerializeField] private GameObject connectLine;

    private Transform letRhombChild;
    private Transform connectLineChild;

    [SerializeField] private Vector3 connectLinePos;

    private void Awake()
    {
        _letRhomb = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        letRhombChild = transform.Find("LetRhombAnim");
        letRhombChild.position = new Vector3(transform.position.x, transform.position.y, -1f);
        connectLineChild = connectLine.transform.Find("ConnectLineChild");
        connectLineChild.position = new Vector3(connectLine.transform.position.x, connectLine.transform.position.y, -1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>() == rhombToUnlet.GetComponent<Collider2D>())
        {
            UnlockWays();
            DoTween();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>() == rhombToUnlet.GetComponent<Collider2D>())
        {
            _letRhomb.enabled = false;   
        }
    }

    private void UnlockWays()
    {
        for (int i = 0; i < listOfValues.Count; i++)
        {
            _nodeRhombScript = listOfValues[i].unletRhomb.GetComponent<NodeRhomb>();
            _nodeRhombScript.LeftDir = listOfValues[i].leftDir;
            _nodeRhombScript.RightDir = listOfValues[i].rightDir;
            _nodeRhombScript.UpDir = listOfValues[i].upDir;
            _nodeRhombScript.DownDir = listOfValues[i].downDir;

            _nodeRhombScript.LeftDist = listOfValues[i].leftDist;
            _nodeRhombScript.RightDist = listOfValues[i].rightDist;
            _nodeRhombScript.UpDist = listOfValues[i].upDist;
            _nodeRhombScript.DownDist = listOfValues[i].downDist;
        }
    }

    private void DoTween()
    {
        connectLineChild.transform.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
        connectLine.transform.DOLocalMove(connectLinePos, 1f);
        letRhombChild.transform.GetComponent<SpriteRenderer>().DOFade(0f, 1f);
    }
}
