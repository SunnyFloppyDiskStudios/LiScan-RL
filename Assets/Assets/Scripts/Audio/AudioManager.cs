using UnityEngine;
using FMODUnity;
using FMOD.Studio;

namespace Player.Audio {
    public class AudioManager : MonoBehaviour {
        // creates a class which controls all the FMOD audio in the game.
    
        public static AudioManager instance { get; private set; }

        private void Awake() {
            // make sure there are oonly 1 manageer. otherwise there can be side effects like more sounds.
            if (instance is null) {
                Debug.LogError("more than 1 audiomanager");
            }
            instance = this;
        }

        public void PlayOneShot(EventReference sound, Vector3 worldPos) {
            // one shot (noway) is a sound that is only played once. (although i think im using this wrong...)
            RuntimeManager.PlayOneShot(sound, worldPos);
        }

        public EventInstance CreateInstance(EventReference eventRef) {
            EventInstance evI = RuntimeManager.CreateInstance(eventRef);
            return evI;
        }
    }
}
