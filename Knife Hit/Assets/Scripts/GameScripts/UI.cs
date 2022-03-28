using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    [Header("Prefabs")]

    [Tooltip("Apple score text")]
    public Text applesScoreText;

    [Tooltip("Stage score Text")]
    [SerializeField] private Text stageScoreText;

    [Tooltip("Knife score text")]
    public Text knifeScoreText;

    [Tooltip("Apple score prefab")]
    public Score appleScore;

    [Tooltip("Knife score prefab")]
    public Score knifeScore;

    [Tooltip("Stage score prefab")]
    public Score stageScore;

    [Tooltip("Knifes prefabs")]
    [SerializeField] private GameObject[] healthKnifes;

    [Tooltip("Knife script")]
    [SerializeField] private Knife knife;

    [Tooltip("Restart button prefab")]
    [SerializeField] private Button restartButton;

    [Tooltip("Main menu button")]
    [SerializeField] private Button menuButton;

    [Tooltip("Fail menu canvas")]
    [SerializeField] private GameObject failMenu;

    [Tooltip("Record stage value")]
    [SerializeField] private Text recordStage;

    [Tooltip("Record score value")]
    [SerializeField] private Text recordScore;

    [Tooltip("Log prefab")]
    [SerializeField] private GameObject log;

    [Tooltip("Log elements")]
    [SerializeField] private GameObject logBoom;

    [Tooltip("Log destroy sound")]
    [SerializeField] AudioClip logDestroy;

    [Header("Values")]
    [Tooltip("bool is knife in log")]
    public bool knifeInLog = false;

    private int countOfKnifes = 0, reverseCount = 0;
    private bool flag = false;
    private void Awake()
    {
        Vibration.Init();
        LoadData();
        SpawnHealthKnifes();
        stageScoreText.text = stageScore.scoreValue.ToString();
        applesScoreText.text = appleScore.scoreValue.ToString();
        knifeScoreText.text = knifeScore.scoreValue.ToString();
        reverseCount = countOfKnifes-1;
    }

    private void Update()
    {
        ColorHealthKnifes();
        SetNewLevel();
        SaveData();
    }

    /// <summary>
    /// Reloaded lvl
    /// </summary>
    private void SetNewLevel()
    {
        if (reverseCount < 0 && !flag)
        {
            Vibration.Vibrate(150);
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(logDestroy);
            CheckForApple();
            OffHealthKnifes();
            knife.GetComponent<Knife>().OffKnifesScript();
            logBoom.SetActive(true);
            log.GetComponent<Renderer>().enabled = false;
            flag = true;
            ChangeStageScore();
            StartCoroutine(LoadWinningScene());
        }
    }

    /// <summary>
    /// Check for apple collision
    /// </summary>
    private void CheckForApple()
    {
        GameObject apple;
        if (apple = GameObject.FindGameObjectWithTag("apple"))
        {
            apple.AddComponent<Rigidbody2D>();
            apple.GetComponent<Collider2D>().enabled = false;
        }
    }

    /// <summary>
    /// Plus one to change score
    /// </summary>
    private void ChangeStageScore()
    {
        stageScore.scoreValue += 1;
        if (stageScore.scoreValue > stageScore.recordValue)
        {
            stageScore.recordValue = stageScore.scoreValue;
        }
    }

    /// <summary>
    /// Enable off health knifes
    /// </summary>
    public void OffHealthKnifes()
    {
        GameObject[] healthKnifes = GameObject.FindGameObjectsWithTag("hKnife");
        for (int i = 0; i < healthKnifes.Length; i++)
        {
            healthKnifes[i].SetActive(false);
        }
    }

    /// <summary>
    /// Enable final score menu
    /// </summary>
    public void EnableFailMenu()
    {
        StartCoroutine(SetButtonActive(restartButton, 2f));
        StartCoroutine(SetButtonActive(menuButton, 2f));
        recordStage.text = stageScore.recordValue.ToString();
        recordScore.text = knifeScore.recordValue.ToString();
        failMenu.SetActive(true);
    }

    /// <summary>
    /// Set button active after time
    /// </summary>
    /// <param name="button">Just button</param>
    /// <param name="time">Delay time</param>
    /// <returns></returns>
    public IEnumerator SetButtonActive(Button button, float time)
    {
        button.interactable = false;
        button.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(time);
        button.interactable = true;
    }

    /// <summary>
    /// Loading scene after delay
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadWinningScene()
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Color knifes to black color
    /// </summary>
    private void ColorHealthKnifes()
    {
        if (knifeInLog && reverseCount >= 0)
        {
            healthKnifes[reverseCount].GetComponent<Image>().color = Color.black;
            reverseCount--;
            knifeInLog = false;
        }
    }

    /// <summary>
    /// Spawn health knifes on the start
    /// </summary>
    private void SpawnHealthKnifes()
    {
        countOfKnifes = Random.Range(5,10);
        for (int i = 0; i < countOfKnifes; i++)
        {
            healthKnifes[i].SetActive(true);
        }
    }

    /// <summary>
    /// Set apple score to the scoreboard
    /// </summary>
    public void SetAppleScore()
    {
        appleScore.scoreValue++;
        applesScoreText.text = appleScore.scoreValue.ToString();
    }

    /// <summary>
    /// Plus one to score
    /// </summary>
    public void EditKnifeScore()
    {
        knifeScore.scoreValue += 1;
        if (knifeScore.scoreValue > knifeScore.recordValue)
        {
            knifeScore.recordValue = knifeScore.scoreValue;
        }
        knifeScoreText.text = knifeScore.scoreValue.ToString();
    }
    
    /// <summary>
    /// Save game data
    /// </summary>
    private void SaveData()
    {
        PlayerPrefs.SetInt("SavedScore", knifeScore.recordValue);
        PlayerPrefs.SetInt("SavedStage", stageScore.recordValue);
        PlayerPrefs.SetInt("SavedApple", appleScore.scoreValue);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Load saved data
    /// </summary>
    private void LoadData()
    {
        if (PlayerPrefs.HasKey("SavedScore"))
        {
            knifeScore.recordValue = PlayerPrefs.GetInt("SavedScore");
            stageScore.recordValue = PlayerPrefs.GetInt("SavedStage");
            appleScore.scoreValue = PlayerPrefs.GetInt("SavedApple");
        }
    }
}
