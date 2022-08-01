using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyKnifeParticle());
    }

    /// <summary>
    /// Destroy particles for knife
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyKnifeParticle()
    {
        yield return new WaitForSecondsRealtime(3f);
        Destroy(this.gameObject);
    }
}
