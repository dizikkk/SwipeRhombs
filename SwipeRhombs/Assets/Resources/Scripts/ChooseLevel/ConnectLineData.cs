using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectLineData : MonoBehaviour
{
    public static ConnectLineData Instance;

    string connectLinePosXKey = "connectLinePosXKey";
    string connectLinePosYKey = "connectLinePosYKey";

    [SerializeField] private int saveIndex;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        connectLinePosXKey = "connectLinePosXKey";
        connectLinePosYKey = "connectLinePosYKey";
        if (PlayerPrefs.HasKey(connectLinePosXKey + saveIndex) && PlayerPrefs.HasKey(connectLinePosYKey + saveIndex))
        {
            var connectLinePosX = PlayerPrefs.GetFloat(connectLinePosXKey + saveIndex);
            var connectLinePosY = PlayerPrefs.GetFloat(connectLinePosYKey + saveIndex);
            transform.position = new Vector3(connectLinePosX, connectLinePosY, 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveConnectLineData()
    {
        StartCoroutine("SaveData");
    }

    IEnumerator SaveData()
    {
        yield return new WaitForSeconds(0.5f);
        connectLinePosXKey = "connectLinePosXKey";
        connectLinePosYKey = "connectLinePosYKey";
        PlayerPrefs.SetFloat(connectLinePosXKey + saveIndex, transform.position.x);
        PlayerPrefs.SetFloat(connectLinePosYKey + saveIndex, transform.position.y);
        Debug.LogError("Saved!" + transform.name + transform.position.x);
    }
}
