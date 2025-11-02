using UnityEngine;

public class ARCube : MonoBehaviour
{
    private Rigidbody rb;
    private bool isRotating = false;
    private float rotationDuration = 1.5f;
    private float timer = 0f;
    public Vector3 torqueForce = new Vector3(0, 300f, 0); // rotation sur y

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;   // cube flottant au départ
        rb.isKinematic = true;   // cube immobile
    }

    void Update()
    {
        if (isRotating)
        {
            timer += Time.deltaTime;
            if (timer >= rotationDuration)
            {
                // Fin de la rotation
                isRotating = false;
                timer = 0f;

                // Rebloquer le cube
                rb.isKinematic = true;
                //b.useGravity = false;
            }
        }
    }

    public void ActivateRotationAndJump()
    {
        // Débloquer le cube
        rb.isKinematic = false;
        rb.useGravity = false;

        // Appliquer rotation
        rb.constraints = RigidbodyConstraints.FreezePositionX |
                 RigidbodyConstraints.FreezePositionY |
                 RigidbodyConstraints.FreezePositionZ |
                 RigidbodyConstraints.FreezeRotationX |
                 RigidbodyConstraints.FreezeRotationZ;

        rb.AddTorque(Vector3.up * 5f, ForceMode.Impulse);

        // Démarrer le timer
        isRotating = true;
        timer = 0f;
    }
}
