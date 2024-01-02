using UnityEngine;

public class Oscillation : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float periodTime;
    float movementFactor = 0;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (periodTime <= Mathf.Epsilon) {return;}
        float frequency = 2 * Mathf.PI / periodTime;
        float rawSin = Mathf.Sin(frequency * Time.time);
        movementFactor = (rawSin + 1)/2;
        Vector3 offset = movementVector * movementFactor;
        Vector3 newPosition = startingPosition + offset;
        transform.position = newPosition;
    }
}
