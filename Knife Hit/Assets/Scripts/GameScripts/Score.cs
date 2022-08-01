using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Score", menuName ="Objects / Score")]
public class Score : ScriptableObject
{
    [Header("Values")]

    [Tooltip("Score for UI")]
    public int scoreValue;

    [Tooltip("Record value")]
    public int recordValue;
}
