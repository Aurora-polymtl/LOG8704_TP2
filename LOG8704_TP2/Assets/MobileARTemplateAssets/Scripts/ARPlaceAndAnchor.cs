using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARAnchorManager))]
public class ARPlaceAndAnchor : MonoBehaviour
{
    private ARRaycastManager _raycastManager;
    private ARAnchorManager _anchorManager;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
        _anchorManager = GetComponent<ARAnchorManager>();
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began) return;

        Vector2 touchPos = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        }

        /*if (_raycastManager.Raycast(touchPos, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hit = s_Hits[0];
            Pose hitPose = hit.pose;

            // père le ARPlane sur lequel on a touché
            ARPlane plane = hit.trackable as ARPlane;

            // hor directement sur ce plan
            ARAnchor anchor = _anchorManager.AttachAnchor(plane, hitPose);

           
            if (anchor == null)
            {
                Debug.LogWarning("AttachAnchor a échoué, création d’un GameObject normal à la place.");
                GameObject fallback = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fallback.transform.position = hitPose.position;
                fallback.transform.localScale = Vector3.one * 0.1f;
                return;
            }

         
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(anchor.transform, false);
            cube.transform.localPosition = Vector3.zero;
            cube.transform.localScale = Vector3.one * 0.1f;
        }*/
    }
}
