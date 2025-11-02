using UnityEngine;

public class ARCube : MonoBehaviour
{
    private Rigidbody rb;
    private bool isRotating = false;
    private float rotationDuration = 1.5f;
    private float timer = 0f;
    public Vector3 torqueForce = new Vector3(0, 300f, 0); 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;
        rb.isKinematic = true;   
    }

    void Update()
    {
        if (isRotating)
        {
            timer += Time.deltaTime;
            if (timer >= rotationDuration)
            {
                isRotating = false;
                timer = 0f;
                rb.isKinematic = true;
            }
        }
    }

    public void ActivateRotationAndJump()
    {
        rb.isKinematic = false;
        rb.useGravity = false;

        rb.constraints = RigidbodyConstraints.FreezePositionX |
                 RigidbodyConstraints.FreezePositionY |
                 RigidbodyConstraints.FreezePositionZ |
                 RigidbodyConstraints.FreezeRotationX |
                 RigidbodyConstraints.FreezeRotationZ;

        rb.AddTorque(Vector3.up * 5f, ForceMode.Impulse);

        isRotating = true;
        timer = 0f;
    }
}
