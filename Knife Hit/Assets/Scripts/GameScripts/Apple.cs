using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [Tooltip("Apples elements")]
    [SerializeField] private GameObject[] apple;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckForCollision(collision);
    }

    /// <summary>
    /// Checking for apple collision
    /// </summary>
    /// <param name="collision">Other collision</param>
    private void CheckForCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "spawnedKnife")
        {
            ChangeApplePosition();
        }
        else if (collision.gameObject.tag == "knife")
        {
            AnimateApple();
        }
    }

    /// <summary>
    /// Destroy animation of apple
    /// </summary>
    private void AnimateApple()
    {
        for (int i = 0; i < apple.Length; i++)
        {
            apple[i].SetActive(true);
            apple[i].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 50f);
        }
        apple[0].GetComponent<Rigidbody2D>().AddForce(Vector2.left * 30f);
        apple[1].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 30f);
    }

    /// <summary>
    /// Changing apple position
    /// </summary>
    private void ChangeApplePosition()
    {
        int degree = Random.Range(0, 360);
        float newXPos = 2f * Mathf.Cos(degree);
        float newYPos = 2f * Mathf.Sin(degree);
        transform.position = new Vector3(newXPos, newYPos + 1f);
        Vector3 dir = new Vector3(0f, 2f, 0f) - this.transform.position;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}