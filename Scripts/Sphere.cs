using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField] float ballTimerAfterShot;
    [SerializeField] float totalForceXMultiplier;   
    [SerializeField] float totalForceYMultiplier;

    private Rigidbody sphereRB;

    private Vector3 perfectForce = new Vector3(-57f, 40f, 0f);

    float totalForceOnX;
    float totalForceOnY;

    private static bool isCheatActive = false;

    public int pointValue;

    public string color;


    private void Start()
    {
        ComponentGetter();
    }
    private void Update()
    {
        CheatActivation();
    }

    /* If the spawned sphere has no velocity, the force applied can be calculated on mouse drag.
     This way, the player is prevented from applying force to the sphere all the time.*/

    private void OnMouseDrag()
    {
        if(sphereRB.velocity == Vector3.zero)
        {
            CalculateTotalForce();
        }
    }

    /* The calculated force on mouse drag is applied to the sphere on mouse up.
     The sphere gets destroyed after the player applies force.*/
    private void OnMouseUp()
    {
        ApplyTotalForceOnBall();
    }

    /*Total force applied increases overtime resulting the player to apply more force overtime.
     Totalforce is reset after everytime player applies force to a sphere.*/
    void ResetTotalForce()
    {
        totalForceOnX = 0f;
        totalForceOnY = 0f;
    }

    /* The calculated force on mouse drag is applied to the sphere on mouse up. */
    void ApplyTotalForceOnBall()
    {
        sphereRB.AddForce(new Vector3(totalForceOnX * totalForceXMultiplier, -totalForceOnY * totalForceYMultiplier, 0f), ForceMode.Impulse);
        ResetTotalForce();
    }

    // Total force on mouse drag is calculated by mouse movement.
    void CalculateTotalForce()
    {
        totalForceOnX += Input.GetAxis("Mouse X");
        totalForceOnY += Input.GetAxis("Mouse Y");
    }

    // Gets the components necessary for this script.
    void ComponentGetter()
    {
        sphereRB = gameObject.GetComponent<Rigidbody>();
    }

    void CheatActivation()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.DownArrow))
        {
            isCheatActive = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && sphereRB.velocity == Vector3.zero && isCheatActive)
        {
            sphereRB.AddForce(perfectForce, ForceMode.Impulse);
        }
    }

    

    










}
