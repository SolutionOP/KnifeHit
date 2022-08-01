using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalSound : MonoBehaviour
{
    [Tooltip("Sound manager")]
    [SerializeField] GameObject managerPrefab;
    private void Awake()
    {
        CheckForSoundManagerOnScene();
    }

    private void CheckForSoundManagerOnScene()
    {
        GameObject mySoundManager;
        if (!(mySoundManager = GameObject.FindGameObjectWithTag("fonSound")))
        {
            GameObject newSoundManager = Instantiate(managerPrefab);
            DontDestroyOnLoad(newSoundManager);
        }
    }
}
