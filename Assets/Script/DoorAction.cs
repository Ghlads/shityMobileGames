using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DoorAction : MonoBehaviour
{
    private int point;
    [SerializeField]
    private DoorAction otherDoor;
    [SerializeField]
    private TextMeshPro doorValue;
    [SerializeField]
    private GameObject doorGameObject;
    [SerializeField]
    private Material red, green;

    [SerializeField]
    private float disolveSpeed;
    [SerializeField]
    private ScoreHandler scoreHandler;

    private bool isUsable = true;


    public void SwitchIsUsale()
    {
        isUsable = !isUsable;
    }

    private IEnumerator DisolveDoor(float step,float target)
    {
        if(disolveSpeed<=0)
        {
            disolveSpeed = 0.01f;
        }
        float interpolation = -1f;
        while (interpolation < target) 
        {
            interpolation += step;
            doorGameObject.GetComponent<MeshRenderer>().material.SetFloat("_DisolveForce", interpolation);
            yield return new WaitForSeconds(disolveSpeed);
        }
        GetComponent<AudioSource>().Stop();
    }


    private void Start()
    {
        point = Random.Range(-5, 5);
        doorValue.text = point.ToString();
        if (point > 0)
        {
            doorGameObject.GetComponent<MeshRenderer>().material = green;
            doorGameObject.GetComponent<MeshRenderer>().material.SetFloat("_DisolveForce", -1f);
        }
        else 
        {
            doorGameObject.GetComponent<MeshRenderer>().material = red;
            doorGameObject.GetComponent<MeshRenderer>().material.SetFloat("_DisolveForce", -1f);
        }
    }
    public void Restart()
    {
        doorValue.gameObject.SetActive(true);
        isUsable = true;
        point = Random.Range(-5, 5);
        doorValue.text = point.ToString();
        if (point > 0)
        {
            doorGameObject.GetComponent<MeshRenderer>().material = green;
        }
        else 
        {
            doorGameObject.GetComponent<MeshRenderer>().material = red;
        }
    }
    public void OnRespawn()
    {
        doorValue.gameObject.SetActive(true);
        if (!isUsable)
        {
            SwitchIsUsale();
        }
        point = Random.Range(-10, 10);
        doorValue.text = point.ToString();
        if (point > 0)
        {
            doorGameObject.GetComponent<MeshRenderer>().material = green;
            doorGameObject.GetComponent<MeshRenderer>().material.SetFloat("_DisolveForce", -1f);
        }
        else
        {
            doorGameObject.GetComponent<MeshRenderer>().material = red;
            doorGameObject.GetComponent<MeshRenderer>().material.SetFloat("_DisolveForce", -1f);
        }
    }
    public int GetPoint()
    {
        if (isUsable)
        {
            GetComponent<AudioSource>().Play();
            otherDoor.SwitchIsUsale();
            doorValue.gameObject.SetActive(false);
            StartCoroutine(DisolveDoor(0.2f,2));
            return point;
        }
        else return 0;
    }

   /* public void Update()
    {
        if (scoreHandler.GetIsReset)
        {
            Restart();
        }
    }*/

}
