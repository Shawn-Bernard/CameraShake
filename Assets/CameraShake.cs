using System;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraShake : MonoBehaviour
{
    public float shakePower = 0.5f;
    public float frequency = 25f;
    public float duration = 3;
    public float smoothTime = 3;

    bool timeToShake = false;

    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private float currentDuration;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            originalPosition = transform.localPosition;
            currentDuration = duration;
            timeToShake = true;
            Debug.Log("Space was pressed");
        }

        Shake();

    }

    void Shake()
    {
        float distance = (transform.localPosition - originalPosition).magnitude;
        if (currentDuration > 0)
        {
            float xOffset = Mathf.PerlinNoise(0, Time.time * frequency) * 2 - 1;
            float yOffset = Mathf.PerlinNoise(1, Time.time * frequency) * 2 - 1;

            transform.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0f) * shakePower;

            currentDuration -= Time.deltaTime;
        }
        else if (distance < 5f)
        {
            Vector3 smoothVector = Vector3.SmoothDamp(transform.position, originalPosition, ref velocity, smoothTime);
            transform.localPosition = smoothVector;
            timeToShake = false;

        }
    }
}
