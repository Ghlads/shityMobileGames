using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingIcon;
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private int wheelSpeed;
    private int rotationSpeed = 0;

    public void StartLoading()
    {
        loadingIcon.SetActive(true);
        loadingScreen.SetActive(true);
        rotationSpeed = wheelSpeed;
    }
    public void StopLoading()
    {
        rotationSpeed = 0;
        loadingIcon.SetActive(false);
        loadingScreen.SetActive(false);
    }

    private void FixedUpdate()
    {
        loadingIcon.transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }
}
