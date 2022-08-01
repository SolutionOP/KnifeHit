using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartItems : MonoBehaviour
{
    [Header("Prefabs")]
    [Tooltip("Knife prefab")]
    [SerializeField] private GameObject knife;

    [Tooltip("Log prefab")]
    [SerializeField] private GameObject log;

    [Tooltip("Bonus apple")]
    [SerializeField] Fruits apple;

    [Header("Values")]
    [Tooltip("Radius of log for knifes")]
    [SerializeField] private float radiusForKnifes = 1.7f;

    [Tooltip("Radius of log for fruits")]
    [SerializeField] private float radiusForFruits = 1.7f;

    private GameObject newKnife;
    private GameObject newFruit;

    private void Awake()
    {
        SpawnBonusFruits(apple, apple.chanceToSpawn);
        SpawnKnifes(3);
    }
   
    /// <summary>
    /// Spawn knifes on the log
    /// </summary>
    /// <param name="maxKnifes">Maximum count of knifes</param>
    private void SpawnKnifes(int maxKnifes)
    {
        for (int i = 0; i < Random.Range(1, maxKnifes+1); i++)
        { 
            float degree = Random.Range(0f, 360f);
            float yPos = radiusForKnifes * Mathf.Sin(degree);
            float xPos = radiusForKnifes * Mathf.Cos(degree);
            newKnife = Instantiate(knife, new Vector3(xPos, yPos + transform.position.y, 0f), Quaternion.identity);
            Vector3 dir = transform.position - newKnife.transform.position;
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90f;
            newKnife.transform.SetParent(log.GetComponent<Collider2D>().transform);
            newKnife.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    /// <summary>
    /// Spawn bonus fruits on the log
    /// </summary>
    /// <param name="bonusFruits">Fruit to spawn</param>
    /// <param name="chanceToSpawn">Chance to spawn</param>
    public void SpawnBonusFruits(Fruits bonusFruits, int chanceToSpawn)
    {
        int realChance = Random.Range(0,100);
        if (realChance <= chanceToSpawn) 
        {
            float degree = Random.Range(0f, 360f);
            float yPos = radiusForFruits * Mathf.Sin(degree);
            float xPos = radiusForFruits * Mathf.Cos(degree);
            newFruit = Instantiate(apple.fruitPrefab, new Vector3(xPos, yPos + transform.position.y), Quaternion.identity);
            Vector3 dir = transform.position - newFruit.transform.position;
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f);
            newFruit.transform.SetParent(log.GetComponent<Collider2D>().transform);
            newFruit.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}