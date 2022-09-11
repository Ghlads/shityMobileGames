using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System.IO;

public class ScoreHandler : MonoBehaviour
{
    int doorScore = 5;
    int score = 0;
    
    //Door
    [Header("Door Variable")]
    [SerializeField]
    CheckingInBox checkingInBox;
    [SerializeField]
    float doorSpeed;
    [SerializeField]
    GameObject doorSpawn;
    public static List<Doorhandler> doorList = new List<Doorhandler>();
    public static List<DoorAction> doorActionList = new List<DoorAction>();

    //Assets
    [Header("SavesData Variable")]
    [SerializeField]
    private TextAsset bestScoreAsset;
    private string filePath;
    [SerializeField]
    char stopRead = '/';
    public List<VariableHolder> dataToSave = new List<VariableHolder>();

    //Boss
    [Header("Boss  Variable")]
    [SerializeField]
    private List<GameObject> bossSkin = new List<GameObject>();
    [SerializeField]
    private GameObject bossGameObject;
    [SerializeField]
    AudioSource bossSound;
    [SerializeField]
    BossHandler bossHandler;
    [SerializeField]
    private TextMeshPro bossHpGUI;
    private GameObject bossGO;

    //Player
    [Header("Player Variable")]
    [SerializeField]
    private List<GameObject> playerSkin = new List<GameObject>();
    [SerializeField]
    private Vector3 playerOriginPos;
    [SerializeField]
    MovementHandler movementHandler;
    private GameObject playerGameObject;
    [SerializeField]
    AudioSource playerSound;
    [SerializeField]
    private float playerSpeed;

    //UI
    [Header("UI Variable")]
    [SerializeField]
    private TextMeshPro doorScoreGUI;
    [SerializeField]
    private TextMeshProUGUI scoreGUI, bestScoreGUI, nameSkin;

    //GameEndig variable
    int pass = 0;
    [SerializeField]
    TextMeshProUGUI endText;
    [SerializeField]
    LoadingHandler loadingHandler;
    [SerializeField]
    GameObject losingScreen,startScreen,SettingsScreen;

    //Reset 
    [Header("Reset Variable")]
    [SerializeField]
    ResetOptions resetOptions;
    [SerializeField]
    private List<float> defaultValue;
    private bool isReset = false;
    //Singleton
    public static ScoreHandler instance;
    public bool asStart = false;

    public bool GetIsReset
    {
        get
        {
            return isReset;
        }
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void ReadDataFromFile(char borne)
    {
        string fullData = bestScoreAsset.text;
        string tempString = "";
        for( int i = 0, j = 0; i < fullData.Length-2; i++)
        {
            if (fullData[i].CompareTo(borne) == 0)
            {
                dataToSave[j].var = float.Parse(tempString);
                tempString = "";
                i++;
                j++;
            }
            if (i < fullData.Length)
            {
                tempString = tempString + fullData[i].ToString();
            }
        }
    }

    public void BossFight()
    {
        //1 = bosshp
        dataToSave[1].var -= doorScore;
        dataToSave[1].var = Mathf.Max(dataToSave[1].var, 0);
        bossHpGUI.text = dataToSave[1].var.ToString();
        if (0 == dataToSave[1].var)
        {
            bossGameObject.SetActive(false);
            Destroy(bossGO);
            endText.text = "You win !";
            dataToSave[2].var+= 3;
            dataToSave[1].var = (int)dataToSave[3].var;
            dataToSave[3].var += 10;
            dataToSave[4].var = (int)Mathf.Floor(Random.Range(0, bossSkin.Count));
            StopGame();
        }
        else
        {
            bossSound.Play();
            bossGO.GetComponent<AnimationHandler>().isPunching();
            StartCoroutine(WaitForBossPunch());
        }

    }

    IEnumerator WaitForBossPunch()
    {
        yield return new WaitForSeconds(18/30);
        bossGO.GetComponent<AnimationHandler>().isWaiting();
        playerGameObject.SetActive(false);
        endText.text = "You'll do better next time !";
        StopGame();
    }


    private void Start()
    {
        ReadDataFromFile(stopRead);
        filePath = AssetDatabase.GetAssetPath(bestScoreAsset);
        scoreGUI.text = "0";
        bestScoreGUI.text = dataToSave[0].var.ToString();
        bossHpGUI.text = dataToSave[1].var.ToString();
        checkingInBox.SetNbDoor((int)dataToSave[2].var);
        resetOptions.allObject.Add(playerSkin[(int)dataToSave[5].var]);
        SetBossAndPlayerSkin();
        doorScoreGUI.text = doorScore.ToString();
        asStart = true;
    }
    public void Restart()
    {
        //ResetVariable
        score = 0;
        doorScore = 5;
        pass = 0;
        playerGameObject.SetActive(true);
        bossGameObject.SetActive(true);
        SetBossAndPlayerSkin();
        doorScoreGUI.gameObject.SetActive(true);
        losingScreen.SetActive(false);
        endText.gameObject.SetActive(false);
        checkingInBox.ResetNbOfDoor();
        //Reset of basics
        ResetDoors(doorList);
        ResetDoorAction();
        scoreGUI.text = "0";
        bestScoreGUI.text = dataToSave[0].var.ToString();
        bossHpGUI.text = dataToSave[1].var.ToString();
        checkingInBox.SetNbDoor((int)dataToSave[2].var);
        isReset = true;
    }

    private void ResetDoorAction()
    {
        for(int i = 0;i < doorActionList.Count; i++)
        {
            doorActionList[i].Restart();
        }
    }

    private void SetBossAndPlayerSkin()
    {
        if (bossGO == null)
        {
            bossGO = Instantiate(bossSkin[(int)dataToSave[4].var], bossGameObject.transform);
            bossSound = bossGO.GetComponent<AudioSource>();
        }
        if (playerGameObject == null)
        {
            GameObject tempPlayer = Instantiate(playerSkin[(int)dataToSave[5].var]);
            playerGameObject = tempPlayer;
            playerGameObject.name = playerGameObject.name[..^7];
            nameSkin.text = playerGameObject.name;
            playerSound = playerGameObject.GetComponent<AudioSource>();
            playerGameObject.transform.position = playerOriginPos;
            movementHandler = playerGameObject.GetComponent<MovementHandler>();
            playerGameObject.GetComponent<CollisionDetectionDoor>().SetCheckingInBox(checkingInBox);
            playerGameObject.GetComponent<CollisionDetectionDoor>().SetScoreHandler(this);
            doorScoreGUI = playerGameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        }
    }

    public void LaunchGame()
    {
        doorScoreGUI.text = doorScore.ToString();
        HideStartUI();
        SetDoorsSpeed(doorList, doorSpeed);
        bossHandler.SetSpeed(doorSpeed);
        movementHandler.SetSpeed(playerSpeed);
        bossGO.GetComponent<AnimationHandler>().isWaiting();
        playerGameObject.GetComponent<AnimationHandler>().isRunning();
    }

    public void SetPunching()
    {
        movementHandler.SetSpeed(0);
        playerGameObject.GetComponent<AnimationHandler>().isPunching();
        StartCoroutine(SetSound());
        StartCoroutine(SetWaiting());
    }

    private IEnumerator SetSound()
    {
        for (int i = 0; i < 3; i++)
        {
            playerSound.Play();
            Debug.Log("play");
            yield return new WaitForSeconds(0.25f);
            playerSound.Stop();
            Debug.Log("stop");
        }
        playerSound.Play();
    }
    private IEnumerator SetWaiting()
    {
        yield return new WaitForSeconds(65 / 30);
        playerGameObject.GetComponent<AnimationHandler>().isWaiting();
        BossFight();
    }

    private  void HideStartUI()
    {
        SettingsScreen.SetActive(false);
        startScreen.SetActive(false);
    }
    private  void ShowStartUI()
    {
        SettingsScreen.SetActive(true);
        startScreen.SetActive(true);
    }

    private void ResetDoors(List<Doorhandler> doorList)
    {
        for (int i = 0; i < doorList.Count; i++)
        {
            doorList[i].gameObject.SetActive(true);
        }
    }

    private void SetDoorsSpeed(List<Doorhandler> doorList, float speed)
    {
        for (int i = 0; i < doorList.Count; i++)
        {
            doorList[i].SetSpeed(speed);
        }
    }

    public void PreviousSkin()
    {
        Destroy(playerGameObject);
        if (dataToSave[5].var == 0) 
        {
            dataToSave[5].var = playerSkin.Count-1;
        }
        else
        {
            dataToSave[5].var -= 1; 
        }
        playerGameObject = null;
        SetBossAndPlayerSkin();
    }

    public void NextSkin()
    {
        Destroy(playerGameObject);
        if (dataToSave[5].var == playerSkin.Count-1)
        {
            dataToSave[5].var = 0;
        }
        else
        {
            dataToSave[5].var += 1;
        }
        playerGameObject = null;
        SetBossAndPlayerSkin();
    }

    public void ResetProgression()
    {
        for(int i = 0; i < dataToSave.Count; i++)
        {
            dataToSave[i].var = defaultValue[i];
        }
        SaveAllData();
        ReadDataFromFile(stopRead);
    }

    private void StopGame()
    {
        if (pass < 1)
        {
            //Debug.Log("GameIsstop");
            playerGameObject.GetComponent<AnimationHandler>().isWaiting();
            scoreGUI.text = score.ToString();
            doorScoreGUI.gameObject.SetActive(false);
            losingScreen.SetActive(true);
            endText.gameObject.SetActive(true);
            movementHandler.SetSpeed(0);
            StartCoroutine(WaitDoorGoAway(2f));
            pass++;
            SaveAllData();
            StartCoroutine(ResetGame());
        }
    }

    public void SaveAllData()
    {
        string dataSave = ChaineDataToSave(dataToSave, stopRead);
        File.WriteAllText(filePath, dataSave);
    }

    private string ChaineDataToSave(List<VariableHolder> listStat,char stopChar)
    {
        string dataSaved = "";
        for (int i = 0; i < listStat.Count; i++)
        {
            dataSaved = dataSaved + listStat[i].var.ToString() + stopChar;
            Debug.Log(dataSaved);
        }
        return dataSaved;
    }

    private IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(2f);
        loadingHandler.StartLoading();
        Restart();
        resetOptions.Restart();
    }

    private void FixedUpdate()
    {
        if(doorScore < 0)
        {
            endText.text = "You Lose";
            score -= 1;
            SetDoorsSpeed(doorList, 0f);
            StopGame();   
        }
        else
        {
            NewBestScore();
        }
        if(isReset&& resetOptions.isReset)
        {
            StartCoroutine(Load());
            isReset = false;
            resetOptions.isReset = false;
        }
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(3f);
        loadingHandler.StopLoading();
        ShowStartUI();
    }

    private void NewBestScore()
    {
        if (score > dataToSave[0].var)
        {
            dataToSave[0].var = score;
            bestScoreGUI.text = scoreGUI.text;
        }
    }

    IEnumerator WaitDoorGoAway(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetDoorsSpeed(doorList, 0f);
        bossHandler.SetCurrentSpeed(0f);
    } 

    public void UpdateScore(int point)
    {
        doorScore = doorScore + point;
        score++;
        doorScoreGUI.text = doorScore.ToString();
        scoreGUI.text = score.ToString();
    }
}
