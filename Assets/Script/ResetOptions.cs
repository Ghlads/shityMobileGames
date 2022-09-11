using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOptions : MonoBehaviour
{
    [Header("ObjectToReset")]
    public List<GameObject> allObject;
    private List<Vector3> originPos;
    private int listLength;

    public bool isReset;  

    private void Start()
    {
        originPos = new List<Vector3>();
        listLength = allObject.Count;
        for (int i = 0; i < listLength; i++)
        {
            originPos.Add(allObject[i].transform.position);
        }
    }

    public void Restart()
    {
        for (int i = 0; i < listLength; i++)
        {
            allObject[i].transform.position = originPos[i];
        }
        isReset = true;
    }
}
