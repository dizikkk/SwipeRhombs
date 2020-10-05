using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRhomb : MonoBehaviour
{
    public static NodeRhomb _nodeRhombInst;

    [SerializeField] private int leftDir;
    [SerializeField] private int rightDir;
    [SerializeField] private int upDir;
    [SerializeField] private int downDir;

    [SerializeField] private float leftDist;
    [SerializeField] private float rightDist;
    [SerializeField] private float upDist;
    [SerializeField] private float downDist;

    public int LeftDir { get => leftDir; set => leftDir = value; }
    public int RightDir { get => rightDir; set => rightDir = value; }
    public int UpDir { get => upDir; set => upDir = value; }
    public int DownDir { get => downDir; set => downDir = value; }

    public float LeftDist { get => leftDist; set => leftDist = value; }
    public float RightDist { get => rightDist; set => rightDist = value; }
    public float UpDist { get => upDist; set => upDist = value; }
    public float DownDist { get => downDist; set => downDist = value; }

    // Start is called before the first frame update
    void Start()
    {
        _nodeRhombInst = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
