using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BonusFruit", menuName ="Objects / Bonus Fruits", order = 51)]
public class Fruits : ScriptableObject
{
    [Header("View settings")]
    [Tooltip("Fruit sprite")]
    public GameObject fruitPrefab;

    [Header("Values")]
    [Tooltip("Chance to spawn")]
    public int chanceToSpawn;
}
