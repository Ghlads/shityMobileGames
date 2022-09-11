using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingInBox : MonoBehaviour
{
    [SerializeField]
    BossHandler bossHandler;
    [SerializeField]
    private bool isInfinte;
    private int nbOfDoor = 3, nbMaxDoor;

    public void ResetNbOfDoor()
    {
        nbOfDoor = 3;
    }

    public void UpdateNbDoor()
    {
        nbOfDoor ++;
    }

    public void SetNbDoor(int door)
    {
        if (door < 4)
        {
            nbMaxDoor = 4;
        }
        else
        {
            nbMaxDoor = door;
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DoorBox")
        {
            if (nbOfDoor < nbMaxDoor||isInfinte)
            {
                foreach (Transform child in other.gameObject.transform)
                {
                    child.GetComponent<DoorAction>().OnRespawn();
                }
                other.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                other.gameObject.SetActive(false);
                bossHandler.Comming();
            }
        }
    }
}
