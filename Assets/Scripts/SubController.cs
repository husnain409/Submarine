using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubController : MonoBehaviour
{
    //Reference for controlling turbine Animatons
    private SubAnimations submarineAnimations;
    public Animator finsAnim;

    //Movement variables
    private Rigidbody rb;
    private float currentSpeed;
    [Tooltip("Amount from which speed will change")]
    public float speedChangeAmount;
    [Tooltip("Set the maximum forward speed the submarine can achieve")]
    public float maxForwardSpeed;
    [Tooltip("Set the maximum backspeed speed the submarine can achieve")]
    public float maxBackwardSpeed;
    [Tooltip("Minimum speed before reaching zero")]
    public float minSpeed;


    //turning variabeles
    [Tooltip("Turning speed of subamrine")]
    public float turnSpeed;


    //Rising Lowering variabels
    [Tooltip("Rising and lowering speed of submarine")]
    public float riseSpeed;


    //Stabilazations, Handling the rotation of submarine model
    [Tooltip("This will keep the submarine face forward while turning")]
    public float stabalization;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        submarineAnimations = GetComponent<SubAnimations>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementFuntion();
        TurningFunction();
        Rising_LoweringFunction();
        StablizationFunction();
    }

    //All the movement code is within this function, changing variables value from editor can change the max/min speed of the different submarines
    void MovementFuntion() {
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed += speedChangeAmount;
            submarineAnimations.Spin(1);
            finsAnim.enabled = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentSpeed -= speedChangeAmount;
            submarineAnimations.Spin(-1);
            finsAnim.enabled = true;
        }
        else if (Mathf.Abs(currentSpeed) <= minSpeed)
        {
            currentSpeed = 0;
            submarineAnimations.Spin(0);
            finsAnim.enabled = false;
        } else if (currentSpeed != 0) {
            submarineAnimations.Spin(currentSpeed / Mathf.Abs(currentSpeed) / 2);
        }
        currentSpeed = Mathf.Clamp(currentSpeed, -maxBackwardSpeed, maxForwardSpeed);
        rb.AddForce(transform.forward * currentSpeed);
    }


    //Turning functionality, Varables handle the turnspeed for different submarines
    void TurningFunction() {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(transform.up * turnSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(transform.up * -turnSpeed);
        }
    }

    //Rising and lowering functionality, variables values can change the amount easily
    void Rising_LoweringFunction() {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(transform.up * riseSpeed);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            rb.AddForce(transform.up * -riseSpeed);
        }
    }

    //During turning, returnign the additional rotational values to zero so the submarine keeps facing forward
    void StablizationFunction() {
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.Euler(new Vector3(0, rb.rotation.eulerAngles.y, 0)), stabalization));
    }
}
