using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject start;

    public void OpenSettings()
    {
        start.SetActive(false);
    }

    public void CloseSettings()
    {
        start.SetActive(true);
        ScoreHandler.instance.SaveAllData();
    }
}
