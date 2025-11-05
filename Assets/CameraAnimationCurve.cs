using UnityEngine;

public class CameraAnimationCurve : MonoBehaviour
{
    public float frequency = 15f;
    public float duration = 3;
    float xNosie;
    float yNoise;
    Quaternion NoiseRotation;


    //[SerializeField] private Vector3 originalPosition;
    //[SerializeField] private Quaternion originaRotation;
    [SerializeField] private float currentTime;

    public AnimationCurve animationCurve;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //originaRotation = transform.rotation;
            currentTime = 0;
            Debug.Log("Space was pressed");
        }

        Shake();

    }

    void Shake()
    {
        float math = currentTime / duration;
        float power = animationCurve.Evaluate(math);
        if (currentTime < duration)
        {
            xNosie = Mathf.PerlinNoise(0, Time.time * frequency) * 2 - 1;
            yNoise = Mathf.PerlinNoise(1, Time.time * frequency) * 2 - 1;
            NoiseRotation = Quaternion.Euler(new Vector3(xNosie, yNoise) * power);
            transform.rotation = NoiseRotation;
            currentTime += Time.deltaTime;
        }
        else
        {
            transform.localRotation = NoiseRotation;
        }
        
    }
}
