//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;

//[RequireComponent(typeof(ARRaycastManager))]
//public class ARObjectPlacer : MonoBehaviour
//{
//    [SerializeField] private GameObject objectToPlace;  // Ton prefab
//    [SerializeField] private ARRaycastManager _arRaycastManager;

//    public List<GameObject> prefabList; // Drag and drop multiple prefabs (plantes) in Inspector
//    //private ARRaycastManager _arRaycastManager;
//    private GameObject selectedObject;

//    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

//    void Awake()
//    {
//        if (_arRaycastManager == null)
//            _arRaycastManager = GetComponent<ARRaycastManager>();
//    }
//}

//    void Update()
//    {
//        if (Input.touchCount == 0)
//            return;

//        Touch touch = Input.GetTouch(0);

//        Vector2 touchPosition = touch.position;

//        if (touch.phase == TouchPhase.Began)
//        {
//            // 1. Sélectionner l'objet si on en touche un
//            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
//            RaycastHit hitObject;
//            if (Physics.Raycast(ray, out hitObject))
//            {
//                Debug.Log("Update selectionne obj");
//                if (hitObject.collider != null)
//                {
//                    selectedObject = hitObject.collider.gameObject;
//                    return;
//                }
//            }

//            // 2. Sinon, placer un nouvel objet si on touche le sol
//            if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
//            {
//                Debug.Log("Update place obj");
//                Pose hitPose = hits[0].pose;

//                // Choisir un prefab aléatoire depuis la liste
//                //if (prefabList.Count > 0)
//                //{
//                    //int randomIndex = Random.Range(0, prefabList.Count);
//                GameObject newObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
//                //}
//            }
//        }
//        else if (touch.phase == TouchPhase.Moved && selectedObject != null)
//        {
//            // Déplacement de l'objet sélectionné
//            if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
//            {
//                Pose movePose = hits[0].pose;
//                selectedObject.transform.position = movePose.position;
//            }
//        }
//        else if (touch.phase == TouchPhase.Ended)
//        {
//            selectedObject = null;
//        }
//}
//}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARObjectPlacer : MonoBehaviour
{
    private ARRaycastManager _arRaycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        // Quand on touche l’écran
        if (touch.phase == TouchPhase.Began)
        {
            if (_arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                // Crée un cube à l’endroit touché
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = hitPose.position;
                cube.transform.localScale = Vector3.one * 0.1f; // taille du cube
            }
        }
    }
}
