using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRhomb : MonoBehaviour
{
    [SerializeField] private GameObject rhombWithSameColor;

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
        if (collision.GetComponent<Collider2D>() == rhombWithSameColor.GetComponent<Collider2D>())
            TaskManager._taskManagerInst.CountFinishRhomb++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>() == rhombWithSameColor.GetComponent<Collider2D>())
            TaskManager._taskManagerInst.CountFinishRhomb--;
    }
}
