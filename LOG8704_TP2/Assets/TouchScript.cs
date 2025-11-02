using UnityEngine;
using UnityEngine.InputSystem; // Nouveau système d’entrée
using UnityEngine.UI; // au lieu de TMPro
using UnityEngine.EventSystems;

public class TouchScript : MonoBehaviour
{
    private Camera arCamera;
    public GameObject cubePrefab;
    public float spawnDistance = 1f;
    public float minDistance = 0.25f;
    public Text notificationText;

    void Start()
    {
        // Trouver la caméra AR active
        arCamera = Camera.main;
        if (arCamera == null)
        {
            arCamera = FindAnyObjectByType<Camera>();
        }
    }

    void Update()
    {
        Vector2? touchPosition = null;

#if UNITY_EDITOR
        // Mode ÉDITEUR : clic de souris
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            touchPosition = Mouse.current.position.ReadValue();
        }
#else
        // Mode APPAREIL MOBILE : toucher écran
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            var touch = Touchscreen.current.primaryTouch;
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.touchId.ReadValue()))
                return;
            touchPosition = touch.position.ReadValue();
        }
#endif

        // Si on a une position valide (clic ou toucher)
        if (touchPosition.HasValue)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;
            Ray ray = arCamera.ScreenPointToRay(touchPosition.Value);
            // Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 0.5f);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                Debug.Log($"Ray hit: {hit.transform.name}");

                if (hit.transform.CompareTag("TouchableCube"))
                {
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
                    }
                }

                else if (hit.transform.CompareTag("ARCube"))
                {
                    ARCube cube = hit.transform.GetComponent<ARCube>();
                    if (cube != null)
                    {
                        cube.ActivateRotationAndJump();
                    }
                }

                else
                {
                 
                    Vector3 spawnPos = ray.origin + ray.direction * 1f;

                    GameObject[] cubes = GameObject.FindGameObjectsWithTag("ARCube");
                    GameObject[] balls = GameObject.FindGameObjectsWithTag("ARTennisBall");
                    bool tooClose = false;
                    foreach (GameObject cube in cubes)
                    {
                        if (Vector3.Distance(spawnPos, cube.transform.position) < minDistance)
                        {
                            tooClose = true;
                            break;
                        }
                    }
                    if (!tooClose)
                    {
                        foreach (GameObject ball in balls)
                        {
                            float distance = Vector3.Distance(spawnPos, ball.transform.position);
                            if (distance < minDistance)
                            {
                                tooClose = true;
                                break;
                            }
                        }
                    }
                    if (tooClose)
                    {
                        ShowNotification("Un objet est trop proche");
                    }
                    else
                    {
                        GameObject newCube = Instantiate(cubePrefab, spawnPos, Quaternion.identity);
                        newCube.tag = "ARCube";

                        if (newCube.GetComponent<ARCube>() == null)
                            newCube.AddComponent<ARCube>();
                    }
                }
            }
            else
            {
                Debug.Log("Ray missed everything.");


                Vector3 spawnPos = ray.origin + ray.direction * 1f;

                GameObject[] cubes = GameObject.FindGameObjectsWithTag("ARCube");
                GameObject[] balls = GameObject.FindGameObjectsWithTag("ARTennisBall");
                bool tooClose = false;
                foreach (GameObject cube in cubes)
                {
                    if (Vector3.Distance(spawnPos, cube.transform.position) < minDistance)
                    {
                        tooClose = true;
                        break;
                    }
                }
                if (!tooClose)
                {
                    foreach (GameObject ball in balls)
                    {
                        float distance = Vector3.Distance(spawnPos, ball.transform.position);
                        if (distance < minDistance)
                        {
                            tooClose = true;
                            break;
                        }
                    }
                }
                if (tooClose)
                {
                    ShowNotification("Un objet est trop proche");
                }
                else
                {
                    GameObject newCube = Instantiate(cubePrefab, spawnPos, Quaternion.identity);
                    newCube.tag = "ARCube";

                    if (newCube.GetComponent<ARCube>() == null)
                        newCube.AddComponent<ARCube>();
                }
            }
        }
    }
    private void ShowNotification(string message)
    {
        StopAllCoroutines();
        StartCoroutine(ShowNotificationCoroutine(message, 2f));
    }

    private System.Collections.IEnumerator ShowNotificationCoroutine(string message, float duration)
    {
        notificationText.text = message;
        yield return new WaitForSeconds(duration);
        notificationText.text = "";
    }
}
