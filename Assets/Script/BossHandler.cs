using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandler : MonoBehaviour
{
    [SerializeField]
    private ScoreHandler scoreHandler;
    private float speed = 0;
    private float futurSpeed;

    public void SetSpeed(float newSpeed)
    {
        futurSpeed = newSpeed;
    }
    public void SetCurrentSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public void Comming()
    {
        speed = futurSpeed;
    }

    public void StopBoss()
    {
        speed = 0;
        scoreHandler.SetPunching();
    }


    private void FixedUpdate()
    {
        this.transform.position += new Vector3(0, 0, -1 * speed);
    }
}
