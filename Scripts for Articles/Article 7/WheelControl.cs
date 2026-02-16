using UnityEngine;

namespace Tips.Part_7_End
{
    /// <summary>
    /// Keeps a visual wheel model in sync with its WheelCollider
    /// </summary>
    public class WheelControl : MonoBehaviour
    {
        public Transform wheelModel;

        [HideInInspector] public WheelCollider WheelCollider;

        // Create properties for the CarControl script
        // (You should enable/disable these via the 
        // Editor Inspector window)
        public bool steerable;
        public bool motorized;

        Vector3 position;
        Quaternion rotation;

        // Start is called before the first frame update
        private void Awake()
        {
            WheelCollider = GetComponent<WheelCollider>();
        }

        // Update is called once per frame
        void Update()
        {
            // Get the Wheel collider's world pose values and
            // use them to set the wheel model's position and rotation
            WheelCollider.GetWorldPose(out position, out rotation);
            wheelModel.transform.position = position;
            wheelModel.transform.rotation = rotation;
        }
    }
}
