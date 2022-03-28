using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBoom : MonoBehaviour
{
    [Tooltip("Power to force")]
    [SerializeField] private float powerBoom;

    [Tooltip("Log elements")]
    [SerializeField] private GameObject[] log;
    void Start()
    {
        LogBoomAnimate();
    }

    /// <summary>
    /// Destroy log
    /// </summary>
    private void LogBoomAnimate()
    {
        log[0].GetComponent<Rigidbody2D>().AddForce(Vector2.up * powerBoom);
        log[1].GetComponent<Rigidbody2D>().AddForce(Vector2.left * powerBoom);
        log[2].GetComponent<Rigidbody2D>().AddForce(Vector2.right * powerBoom);
    }
}
