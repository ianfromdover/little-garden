using UdonSharp;
using UnityEngine;

namespace _littleGarden.ScriptsU
{
    /// <summary>
    /// Fires off an event
    /// Only works locally.
    /// </summary>
    [UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
    public class ToggleMirror : UdonSharpBehaviour
    {
        /*
        [SerializeField] private CanvasFollowPlayer canvScript;
        public override void Interact()
        {
            canvScript.ToggleFacePlayer();
        }
        */
    }
}
