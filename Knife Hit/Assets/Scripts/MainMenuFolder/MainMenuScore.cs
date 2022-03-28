using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScore : MonoBehaviour
{
    [Header("Stages")]

    [Tooltip("Stage score board text")]
    [SerializeField] private Text stageScore;

    [Tooltip("Stage scriptable object")]
    [SerializeField] private Score stageObject;

    [Header("Apple")]

    [Tooltip("Apple score board text")]
    [SerializeField] private Text appleScore;

    [Tooltip("Apple scriptable object")]
    [SerializeField] private Score appleObject;

    [Header("Knifes Score")]

    [Tooltip("Score value text")]
    [SerializeField] private Text scoreValue;

    [Tooltip("Score scriptable object")]
    [SerializeField] private Score scoreObject;

    [Tooltip("Main menu sound")]
    [SerializeField] private AudioClip mainMenu;

    private void Awake()
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(mainMenu);
        Destroy(GameObject.FindGameObjectWithTag("fonSound"));
    }
    private void Start()
    {
        LoadData();
        stageScore.text = stageObject.recordValue.ToString();
        appleScore.text = appleObject.scoreValue.ToString();
        scoreValue.text = scoreObject.recordValue.ToString();
    }

    /// <summary>
    /// Load saved data
    /// </summary>
    private void LoadData()
    {
        if (PlayerPrefs.HasKey("SavedScore"))
        {
            scoreObject.recordValue = PlayerPrefs.GetInt("SavedScore");
            stageObject.recordValue = PlayerPrefs.GetInt("SavedStage");
            appleObject.scoreValue = PlayerPrefs.GetInt("SavedApple");
        }
    }
}
