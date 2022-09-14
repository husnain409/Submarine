using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAnimations : MonoBehaviour
{
    public float turbineSpeed;
    public float turbineSmoothing;
    private float lastTurbineTurn;


    public Transform turbine;

    // Handling turbine rotation from script so it remains framerate independant and similar across all devices
    public void Spin(float direction) {
        float currentTurn = Mathf.MoveTowards(lastTurbineTurn, turbineSpeed * direction, turbineSmoothing * Time.deltaTime);
        turbine.Rotate(0,0,currentTurn);
        lastTurbineTurn = currentTurn;
    }
}
