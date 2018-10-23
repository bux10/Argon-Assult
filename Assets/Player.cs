using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [Tooltip( "Movement Speed In ms^-1" )][SerializeField] float speed = 10f;
    [Tooltip( "Horizontal Range of Movement In meters" )] [SerializeField] float xRange = 5f;
    [Tooltip( "Vertical Range of Movement In meters" )] [SerializeField] float yRange = 2.5f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;

    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlYawFactor = 10f;

    [SerializeField] float controlRollFactor = -30f;

    float xThrow, yThrow;

	void FixedUpdate ()
    {
        processTranlation();
        processRotation();
        shoot();
    }
    void shoot()
    {
        if (CrossPlatformInputManager.GetButton("Fire1")) print("PEW!");
    }

    void processTranlation()
    {
        //Get input from keyboard or controller
        xThrow = CrossPlatformInputManager.GetAxis( "Horizontal" );
        yThrow = CrossPlatformInputManager.GetAxis( "Vertical" );

        //Calculate offset per frame
        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        //Clamp movement to edge of screen as indicated by xRange & yRange
        float clampedXPOS = Mathf.Clamp( transform.localPosition.x + xOffset, -xRange, xRange );
        float clampedYPOS = Mathf.Clamp( transform.localPosition.y + yOffset, -yRange, yRange );

        //Move ship to new position
        transform.localPosition = new Vector3( clampedXPOS, clampedYPOS, transform.localPosition.z );
    }

    void processRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor + xThrow * controlYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler( pitch, yaw, roll );
    }
}
