using Player.Pickup;
using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Player.LIDAR {
    public class gunScript : MonoBehaviour {
        // the main mechanic, shooting nodes.
        
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        public GameObject gun;
        
        private InputAction clickAction;
        private InputAction spreadAction; // not implemented
        
        public int numberOfRays = 20;
        public float maxAngle = 30f;
        public GameObject dotPrefab;

        private bool isShooting;
                
        private EventInstance shootingSound;

        public bool canShoot = true;
        
        [Header("DEBUG")]
        public int totalNodeCount;
        
        private void Start() {
            // get inputs and instantiate sound system
            clickAction = InputSystem.actions.FindAction("ClickAction");
            spreadAction = InputSystem.actions.FindAction("LineAction");

            // shootingSound = AudioManager.instance.CreateInstance(FMODEvents.instance.gunShoot);
        }

        private void Update() {
            // logic for how stuff should happen when the uer presses buttons.
            if (canShoot) {
                int shootType = 0;
                bool didShootThisUpdate = false;
                
                if (clickAction.ReadValue<float>() > 0f) {
                    didShootThisUpdate = true;
                    shootType = 0;
                }
                
                isShooting = didShootThisUpdate;
                UpdateSound();

                if (!didShootThisUpdate) return;
                
                // actually shoot in circular pattern.
                Transform gunT = gun.transform;

                for (int i = 0; i < numberOfRays; i++) {
                    Vector3 direction = GetRandomDirectionInCone(gunT.forward, maxAngle);
                    Ray ray = new Ray(gunT.position, direction);

                    if (Physics.Raycast(ray, out RaycastHit hit)) {
                        GetSpeciality(hit);
                    }
                }
            }
        }
        
        Vector3 GetRandomDirectionInCone(Vector3 forward, float maxAngleDegrees) {
            // to shoot out from the  gun im using a cone shape, so it's like <
            float maxAngleRad = maxAngleDegrees * Mathf.Deg2Rad;

            float angle = Random.Range(0f, maxAngleRad);
            float azimuth = Random.Range(0f, 2f * Mathf.PI);

            Vector3 localDirection = new Vector3(
                Mathf.Sin(angle) * Mathf.Cos(azimuth),
                Mathf.Sin(angle) * Mathf.Sin(azimuth),
                Mathf.Cos(angle)
            );

            return Quaternion.LookRotation(forward) * localDirection;
        }


        void GetSpeciality(RaycastHit hit) {
            // figure out whether it's a power cube thing
            Vector3 position = hit.point;

            HoldableItem hli = hit.transform.GetComponent<HoldableItem>();

            if (hli is not null) {
                hli.interactionNodes += 1;
                return;
            }

            InstancedNodeManager.instance.AddInstance(position, Color.white);
            totalNodeCount += 1;
        }

        private void UpdateSound() {
            // play the gun sound.
            if (isShooting) {
                PLAYBACK_STATE state;
                shootingSound.getPlaybackState(out state);

                if (state == PLAYBACK_STATE.STOPPED) {
                    RuntimeManager.AttachInstanceToGameObject(shootingSound, transform.gameObject, transform.GetComponent<Rigidbody>());
                    shootingSound.start();
                }
            } else {
                shootingSound.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
    }
}

