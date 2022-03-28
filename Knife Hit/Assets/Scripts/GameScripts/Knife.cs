using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knife : MonoBehaviour
{
    [Header("Prefabs")]

    [Tooltip("Knife collider")]
    [SerializeField] private BoxCollider2D boxCollider2d;

    [Tooltip("Knife prefab")]
    [SerializeField] private GameObject knife;

    [Tooltip("Knifes class")]
    [SerializeField] private UI uiScript;

    [SerializeField] private AudioClip knifeHitLog;
    [SerializeField] private AudioClip knifeHitKnife;
    [SerializeField] private AudioClip appleDestoy;

    [Header("Values")]

    [Tooltip("Throwing speed")]
    [SerializeField] private float throwSpeed = 1300;

    private Rigidbody2D rigidbody2;
    private bool isGameOver = false, isTouch = false, isAppleDead = false;

    private void Start()
    {
        Vibration.Init();
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckForTouch();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EncounterLog(collision);
        EncounterKnifes(collision);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EncounterApple(collision);
    }

    /// <summary>
    /// Check for touch screan
    /// </summary>
    private void CheckForTouch()
    {
        if (Input.touchCount > 0 && !isTouch)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    ThrowingKnife();
                    isTouch = true;
                    break;
                case TouchPhase.Ended:
                    isTouch = false;
                    break;
            }
        }
    }

    /// <summary>
    /// Encounter log collision
    /// </summary>
    /// <param name="collision">Other collision</param>
    private void EncounterLog(Collision2D collision)
    {
        if (collision.gameObject.tag == "log" && !isGameOver)
        {
            Vibration.Vibrate(50);
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(knifeHitLog);
            //transform.GetChild(0).gameObject.SetActive(true);
            uiScript.knifeInLog = true;
            SpawnKnifes();
            JoinKnife(collision);
            this.enabled = false;
        }
    }

    /// <summary>
    /// Encounter knifes collision
    /// </summary>
    /// <param name="collision">Other collision</param>
    private void EncounterKnifes(Collision2D collision)
    {
        if ((collision.gameObject.tag == "spawnedKnife" ||
            collision.gameObject.tag == "knife") && !isGameOver)
        {
            
            Vibration.Vibrate(350);
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(knifeHitKnife);
            transform.GetChild(0).gameObject.SetActive(true); ;
            uiScript.knifeScore.scoreValue = 0;
            isGameOver = true;
            uiScript.GetComponent<UI>().EnableFailMenu();
            uiScript.OffHealthKnifes();
            rigidbody2.velocity = new Vector2(-10f, -10f);
            uiScript.stageScore.scoreValue = 0;
            this.enabled = false;
        }
    }

    /// <summary>
    /// Encounter apple on the log
    /// </summary>
    /// <param name="collision">Other collision</param>
    private void EncounterApple(Collider2D collision)
    {
        if (collision.gameObject.tag == "apple" && !isGameOver && !isAppleDead)
        {
            this.gameObject.GetComponent<AudioSource>().Stop();
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(appleDestoy);
            isAppleDead = true;
            //transform.GetChild(0).gameObject.SetActive(true);
            collision.gameObject.GetComponent<Renderer>().enabled = false;
            ThrowingKnife();
            uiScript.SetAppleScore();
           
        }
    }

    /// <summary>
    /// Connecting knife to log
    /// </summary>
    /// <param name="collision">log collision</param>
    private void JoinKnife(Collision2D collision) 
    {
        uiScript.EditKnifeScore();
        rigidbody2.velocity = new Vector2(0f, 0f);
        rigidbody2.bodyType = RigidbodyType2D.Kinematic;
        this.transform.SetParent(collision.collider.transform);
        boxCollider2d.offset = new Vector2(0f, -2f);
        boxCollider2d.size = new Vector2(1.19f, 2.54f);
    }

    /// <summary>
    /// Spawn new knife
    /// </summary>
    private void SpawnKnifes()
    {
        GameObject newKnife =  Instantiate(knife, new Vector3(0f, -3f, 0f), Quaternion.identity);
        newKnife.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
    }

    /// <summary>
    /// Throwing knifes to forward
    /// </summary>
    private void ThrowingKnife()
    {
        rigidbody2.AddForce(Vector2.up * throwSpeed);
    }

    /// <summary>
    /// Enable false to Knife script
    /// </summary>
    public void OffKnifesScript()
    {
        GameObject[] throwingKnife = GameObject.FindGameObjectsWithTag("knife");
        for (int i = 0; i < throwingKnife.Length; i++)
        {
            throwingKnife[i].GetComponent<Knife>().enabled = false;
            ChangeVelocity(throwingKnife[i]);
        }

        GameObject[] spawnedKnifes = GameObject.FindGameObjectsWithTag("spawnedKnife");
        for (int i = 0; i < spawnedKnifes.Length; i++)
        { 
            ChangeVelocity(spawnedKnifes[i]);
        }
    }

    /// <summary>
    /// changing object velocity
    /// </summary>
    /// <param name="array"></param>
    private void ChangeVelocity(GameObject element)
    {
        element.GetComponent<Collider2D>().enabled = false;
        element.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        if (element.transform.position.x > 0)
        {
            element.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1f, 10f), Random.Range(0, -10));
        }
        else 
        {
            element.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, -10f), Random.Range(0,-10)); 
        }
       
    }
}
