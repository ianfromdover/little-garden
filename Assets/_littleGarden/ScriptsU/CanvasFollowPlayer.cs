using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace _littleGarden.ScriptsU
{
    /// <summary>
    /// Smoothly follows one of the chosen playerApi tracking targets
    /// Attach this script to the Ui which is following the player
    /// </summary>
    [UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
    public class CanvasFollowPlayer : UdonSharpBehaviour
    {
        [SerializeField] private VRCPlayerApi.TrackingDataType trackingTarget;
        
        [Header("Following Settings")]
        [SerializeField] private bool lookAtPlayer; // if you want the canvas to always look at the player
        [SerializeField] private float posSpdMultiplier = 1;
        [SerializeField] private float rotSpdMultiplier = 1;
        private const float POSITION_SMOOTH_TIME = 0.2f;
        private const float ROTATE_SPEED = 0.05f;
        
        private VRCPlayerApi playerApi; // the local player
        private Vector3 velocity = Vector3.zero;
        private bool isInEditor;

        private void Start()
        {
            playerApi = Networking.LocalPlayer;
            isInEditor = playerApi == null; // PlayerApi will be null in editor
        }

        private void LateUpdate()
        {
            // PlayerApi data will only be valid in game so we don't run the update if we're in editor
            if (isInEditor)
                return;
            
            VRCPlayerApi.TrackingData trackingData = playerApi.GetTrackingData(trackingTarget);
            
            Transform t = transform;
            t.position = Vector3.SmoothDamp(t.position, 
                                                  trackingData.position, 
                                                  ref velocity,
                                                  POSITION_SMOOTH_TIME / posSpdMultiplier);

            Quaternion targetRot = trackingData.rotation; // follow rotation of hand
            if (lookAtPlayer) // rotate to face head instead
            {
                Vector3 headPos = playerApi.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
                Vector3 relativePos = -(headPos - t.position); // negative makes the canvas flip the right way
                targetRot = Quaternion.LookRotation(relativePos, new Vector3(0, 1, 0));
            }

            t.rotation = Quaternion.Slerp(t.rotation, targetRot, ROTATE_SPEED * rotSpdMultiplier);
        }

        public void ToggleFacePlayer()
        {
            lookAtPlayer = !lookAtPlayer;
        }
    }
}
