using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDoorhandlerToList : MonoBehaviour
{

    private void OnEnable()
    {
        Doorhandler doorHandler = GetComponent<Doorhandler>();
        if (!CheckIfIsInList(doorHandler))
        {
            ScoreHandler.doorList.Add(doorHandler); 
        }
    }

    private bool CheckIfIsInList(Doorhandler target)
    {
        for (int i = 0; i < ScoreHandler.doorList.Count; i++)
        {
            if (ScoreHandler.doorList[i] == target)
            {
                return true;
            }
        }
        return false;
    }
}
