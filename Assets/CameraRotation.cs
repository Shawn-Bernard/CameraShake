using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float shakePower = 0.5f;
    public float frequency = 25f;
    public float duration = 3;
    public float smoothTime = 3;

    bool timeToShake = false;

    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Quaternion originaRotation;
    [SerializeField] private float currentDuration;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            originaRotation = transform.rotation;
            currentDuration = duration;
            timeToShake = true;
            Debug.Log("Space was pressed");
        }

        Shake();

    }

    void Shake()
    {
        float distance = Quaternion.Angle(transform.localRotation, originaRotation);
        if (currentDuration > 0)
        {
            float xNosie = Mathf.PerlinNoise(0, Time.time * frequency) * 2 - 1;
            float yNoise = Mathf.PerlinNoise(1, Time.time * frequency) * 2 - 1;
            Quaternion NoiseRotation = Quaternion.Euler(new Vector3(xNosie, yNoise) * shakePower);
            transform.rotation =  NoiseRotation;

            currentDuration -= Time.deltaTime;
        }
        else if (distance < 5)
        {
            Quaternion smoothQuaternion = Quaternion.RotateTowards(transform.rotation, originaRotation, Time.deltaTime);
            transform.localRotation = smoothQuaternion;

        }
    }
}
