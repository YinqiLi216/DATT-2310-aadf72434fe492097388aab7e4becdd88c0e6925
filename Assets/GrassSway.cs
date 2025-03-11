using UnityEngine;

public class GrassRotateSway : MonoBehaviour
{
    public float swaySpeed = 1f;       
    public float maxAngle = 2f;         

    private float startTime;

    void Start()
    {
        startTime = Random.Range(0f, 2f * Mathf.PI);  
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * swaySpeed + startTime) * maxAngle;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
