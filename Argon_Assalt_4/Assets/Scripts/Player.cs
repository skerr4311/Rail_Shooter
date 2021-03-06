﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float Speed = 40f;
    [Tooltip("In m")] [SerializeField] float xRange = 15f;
    [Tooltip("In m")] [SerializeField] float yRange = 10f;

    [Header("Screen-Position-Based")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-Throw-Based")]
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float controlRollFactor = -50f;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    void OnPlayerDeath() //called by string referance
    {
        isControlEnabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * Speed * Time.deltaTime;
        float yOffset = yThrow * Speed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateGuns(true);
        } else
        {
            ActivateGuns(false);
        }
    }

    private void ActivateGuns(bool isActive)
    {
        foreach(GameObject gun in guns)
        {
            var emmissionModule = gun.GetComponent<ParticleSystem>().emission;
            emmissionModule.enabled = isActive;
        }
    }
}
