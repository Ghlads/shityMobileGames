using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStopDetection : MonoBehaviour
{
    [SerializeField]
    private BossHandler bossHandler;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bossHandler.StopBoss();
        }
    }

}
