using UnityEngine;

public class TouchScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "TouchableCube")
                {
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}
