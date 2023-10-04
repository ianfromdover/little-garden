using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace _littleGarden.ScriptsU
{
    /// <summary>
    /// Allows players to telekinetically move any obj on the Pickup layer
    /// like in Blade and Sorcery
    ///
    /// if state: free
    /// 1. fire raycast from -y of tracked hand, 5x every second
    /// 2. raycast hits an obj on pickup layer
    /// 3. bind obj and change state to grabbing
    ///
    /// else state: grabbing
    /// 1. calc tgt pos, using init dist from palm and palm's fwd
    /// 2. move obj to pos with inertia (AddForce proportional to dist, instead of setting transform)
    /// effect: if i throw, should have inertia
    ///
    /// ----------
    /// extension: sync so its not only local
    ///     how to sync gameObj transforms?
    /// next extension: joystick to move fwd and bkwd
    /// [] think of method of interaction
    /// </summary>
    public class HandTelekinesis : UdonSharpBehaviour
    {
        [Header("Debug View")]
        [SerializeField] private GameObject objSelected;
        [SerializeField] private GameObject objBeingGrabbed;
        
        [Header("Settings")]
        [SerializeField] private VRCPlayerApi.TrackingDataType trackingTarget; // assume RH
        [SerializeField] private float range = 5.0f;
        [SerializeField] private float rayCastFreq = 0.2f;
        
        private VRCPlayerApi playerApi; // the local player
        
        // for calculate whether to pull closer or push further
        [SerializeField] private bool isGrabbing = false; // aka free state
        private float initialObjDist;
        private float currObjDist;
        private Transform currTrf; // ref to obj
        private Rigidbody rb;
        private Vector3 targetPos; // where the player wants to move the obj to
        
        private int layerMask = 1 << 13; // layer 13: pickup
        private float freqCounter = 0;
        private bool isInEditor;

        /*
        private void Start()
        {
            playerApi = Networking.LocalPlayer;
            isInEditor = playerApi == null; // PlayerApi will be null in editor
        }
        private void Update()
        {
            // PlayerApi data will only be valid in game so we don't run the update if we're in editor
            if (isInEditor || isGrabbing)
                return;
            
            // timer to reduce raycast shots for performance
            freqCounter += Time.deltaTime;
            if (freqCounter < rayCastFreq)
                return;

            freqCounter = 0;
            if (!raycastFindObj()) // hand is free and no item found
                return;
            
            DrawSelection();
        }

        private void FixedUpdate()
        {
            if (!isGrabbing)
                return;
            CalcTarget();
            Move();
        }

        private bool raycastFindObj()
        {
            VRCPlayerApi.TrackingData trackingData = playerApi.GetTrackingData(trackingTarget);
            
            RaycastHit hit; // is this the first hit or many hits?
            if (!Physics.Raycast(Vector3.one, Vector3.forward, out hit, range, layerMask))
                return false;
            
            currTrf = hit.transform;
            rb = currTrf.GetComponent<Rigidbody>();
            targetPos = currTrf.position;
            initialObjDist = hit.distance;
            isGrabbing = true;
            if (rb == null)
                Debug.LogError($"HandTelekinesis: No RigidBody on " +
                               $"pickup {currTrf.gameObject.name} found! Moving will break");
            return true;
        }

        // Draws icon on screenspace in relation to hud
        private void DrawSelection()
        {
            // get center of pickup
            // find worldspace pos of center to eye, scale to equal size
            // put sprite there
        }

        private void CalcTarget()
        {
            targetPos = Vector3.one;
            // obj pos + vec3(tgt dist * direction from hand)
        }
        
        private void Move()
        {
            // AddForceAtPosition should be called within FixedUpdate.
            // Applying it from Update might result in the force being applied multiple
            // times per physics step, with unpredictable results.
        }

        // wire this to trigger when grabbing hand releases (event driven)
        private void Release()
        {
            isGrabbing = false;
            currTrf = null;
            targetPos = Vector3.zero; // does this cause bugs?
            initialObjDist = 0f;
        }
        */
    }
}
