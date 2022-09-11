using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionDoor : MonoBehaviour
{
    private ScoreHandler scoreHandler;
    private CheckingInBox checkingInBox;

    public void SetScoreHandler(ScoreHandler newScoreHandler)
    {
        scoreHandler = newScoreHandler;
    }

    public void SetCheckingInBox(CheckingInBox newCheckingInBox)
    {
        checkingInBox = newCheckingInBox;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            scoreHandler.UpdateScore(other.gameObject.GetComponent<DoorAction>().GetPoint());
            checkingInBox.UpdateNbDoor();
        }
    }
}
