using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float thrustConst = 5;
    [SerializeField] float rotateConst = 5;

    [SerializeField] AudioClip thrustSFX;

    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;
    [SerializeField] ParticleSystem mainThrustParticle;

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

        if (leftInput && rightInput) {
            leftThrustParticle.Play();
            rightThrustParticle.Play();
        } //This will reduce the priority of leftInput over rightInput

        else if (leftInput)
        {
            ApplyRotation(rotateConst);
            rightThrustParticle.Stop();
            if (!leftThrustParticle.isPlaying)
                leftThrustParticle.Play();
        }
        else if (rightInput)
        {
            ApplyRotation(-1 * rotateConst);
            leftThrustParticle.Stop();
            if (!rightThrustParticle.isPlaying)
                rightThrustParticle.Play();
        }
        else
        {
            leftThrustParticle.Stop();
            rightThrustParticle.Stop();
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
            if (!mainThrustParticle.isPlaying)
                mainThrustParticle.Play();
        }
        else
        {
            audioSource.Stop();
            mainThrustParticle.Stop();
        }
    }
}
