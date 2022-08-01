using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    private HingeJoint2D hingeJoint2D;
    private JointMotor2D jointMotor2D;
    void Awake()
    {
        hingeJoint2D = GetComponent<HingeJoint2D>();
        jointMotor2D = new JointMotor2D();
        StartCoroutine("RotateLog");
    }

    /// <summary>
    /// Rotate log with random velocity
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateLog() 
    {
        yield return new WaitForFixedUpdate();

        while (true)
        {
            jointMotor2D.motorSpeed = Random.Range(-250,250);
            jointMotor2D.maxMotorTorque = 10000f;
            hingeJoint2D.motor = jointMotor2D;
            yield return new WaitForSecondsRealtime(Random.Range(2,5));
        }
    }
}

