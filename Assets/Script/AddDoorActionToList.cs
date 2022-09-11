using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddDoorActionToList : MonoBehaviour
{

    private void OnEnable()
    {
        DoorAction doorAction = GetComponent<DoorAction>();
        if (!CheckIfIsInList(doorAction))
        {
            ScoreHandler.doorActionList.Add(doorAction);
        }
    }

    private bool CheckIfIsInList(DoorAction target)
    {
        for (int i = 0; i < ScoreHandler.doorActionList.Count; i++)
        {
            if (ScoreHandler.doorActionList[i] == target)
            {
                return true;
            }
        }
        return false;
    }
}
