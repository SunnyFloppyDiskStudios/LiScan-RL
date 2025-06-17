using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace LIDAR {
    public class gunScript : MonoBehaviour {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        public GameObject gun;
        
        private InputAction clickAction;
        private InputAction spreadAction;
        
        public int numberOfRays = 20;
        public float maxAngle = 30f;

        private bool isShooting;
                
        private EventInstance shootingSound;
        
        private void Start() {
            clickAction = InputSystem.actions.FindAction("ClickAction");
            spreadAction = InputSystem.actions.FindAction("LineAction");

            shootingSound = AudioManager.instance.CreateInstance(FMODEvents.instance.gunShoot);
        }

        private void Update() {
            int shootType = 0;
            bool didShootThisUpdate = false;
            
            if (clickAction.ReadValue<float>() > 0f) {
                didShootThisUpdate = true;
                shootType = 0;
            }

            if (spreadAction.ReadValue<float>() > 0f) {
                didShootThisUpdate = true;
                shootType = 1;
            }
            
            isShooting = didShootThisUpdate;
            UpdateSound();

            if (!didShootThisUpdate) return;
            switch (shootType) {
                case 0: {
                    Transform gunT = gun.transform;

                    for (int i = 0; i < numberOfRays; i++) {
                        Vector3 direction = GetRandomDirectionInCone(gunT.forward, maxAngle);
                        Ray ray = new Ray(gunT.position, direction);

                        if (Physics.Raycast(ray, out RaycastHit hit)) {
                            GetSpeciality(hit);
                        }
                    }

                    break;
                } 
                case 1: {
                    Transform gunT = gun.transform;

                    int totalLines = 5;
                    float lineSpacing = 0.2f;
                    float horizontalSpread = 1.0f;

                    Vector3 baseForward = gunT.forward;
                    Vector3 baseRight = gunT.right;
                    Vector3 baseUp = gunT.up;

                    Vector3 origin = gunT.position + baseForward * 1.0f;

                    for (int line = 0; line < totalLines; line++) {
                        int nodesInLine = Random.Range(3, 11);
                        float lineOffsetY = -line * lineSpacing;
                        Vector3 lineCenter = origin + baseUp * lineOffsetY;

                        for (int i = 0; i < nodesInLine; i++) {
                            float t = (nodesInLine == 1) ? 0f : (i / (float)(nodesInLine - 1));
                            float xOffset = (t - 0.5f) * horizontalSpread + Random.Range(-0.05f, 0.05f);

                            Vector3 rayOrigin = lineCenter + baseRight * xOffset;
                            Vector3 rayDirection = -baseUp;

                            Ray ray = new Ray(rayOrigin, rayDirection);
                            if (Physics.Raycast(ray, out RaycastHit hit)) {
                                GetSpeciality(hit);
                            }
                        }
                    }
                    break;
                }
            }
        }
        
        Vector3 GetRandomDirectionInCone(Vector3 forward, float maxAngleDegrees) {
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
            Vector3 position = hit.point;

            InstancedNodeManager.instance.AddInstance(position, hit.transform.GetComponent<Renderer>().material.color);
        }

        private void UpdateSound() {
            if (isShooting) {
                PLAYBACK_STATE state;
                shootingSound.getPlaybackState(out state);

                if (state == PLAYBACK_STATE.STOPPED) {
                    RuntimeManager.AttachInstanceToGameObject(shootingSound, transform, transform.GetComponent<Rigidbody>());
                    shootingSound.start();
                }
            } else {
                shootingSound.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
    }
}