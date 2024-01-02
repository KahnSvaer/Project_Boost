using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float thrustConst = 5;
    [SerializeField] float rotateConst = 5;

    [SerializeField] AudioClip thrustSFX;

    Rigidbody rb;
    AudioSource audioSource;

    float rotateVal, thrustVal;
    bool leftInput, rightInput, upInput;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = thrustSFX;
    }    

    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }    

    private void ProcessRotate()
    {
        leftInput  = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        rightInput = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (leftInput && rightInput) {} //This will reduce the priority of leftInput over rightInput

        else if (leftInput)
        {
            ApplyRotation(rotateConst);
        }
        else if (rightInput)
        {
            ApplyRotation(-1 * rotateConst);
        }
    }

    private void ApplyRotation(float rotateConst)
    {
        rb.freezeRotation = true;
        rotateVal = Time.deltaTime * rotateConst;
        transform.Rotate(Vector3.forward * rotateVal);
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }

    private void ProcessThrust()
    {
        upInput = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (upInput)
        {
            thrustVal = Time.deltaTime * thrustConst;
            rb.AddRelativeForce(Vector3.up*thrustVal);
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(thrustSFX);
        }
        else
        {
            audioSource.Stop();
        }
    }
}
