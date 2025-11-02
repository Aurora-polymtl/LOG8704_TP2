using UnityEngine;
using UnityEngine.XR.Hands;

public class SimpleHandFollower : MonoBehaviour
{
    public XRHandTrackingEvents leftHandTrackingEvents;

    void OnEnable()
    {
        // On s’abonne à l’événement avec la bonne signature
        leftHandTrackingEvents.jointsUpdated.AddListener(OnJointsUpdated);
    }

    void OnDisable()
    {
        leftHandTrackingEvents.jointsUpdated.RemoveListener(OnJointsUpdated);
    }

    // ⚠️ Signature correcte !
    void OnJointsUpdated(XRHandJointsUpdatedEventArgs args)
    {
        // Exemple : récupérer la position du poignet
        if (args.hand.GetJoint(XRHandJointID.Wrist).TryGetPose(out Pose wristPose))
        {
            transform.position = wristPose.position;
            transform.rotation = wristPose.rotation;
        }
    }
}
