using UnityEngine;

public class ForceARMode : MonoBehaviour {
    void Start() {
        OVRManager.instance.isInsightPassthroughEnabled = true;
        OVRManager.instance.trackingOriginType = OVRManager.TrackingOrigin.FloorLevel;
        OVRManager.instance.enableMixedReality = true;
        OVRManager.instance.shouldBoundaryVisibilityBeSuppressed = true;
    }
}