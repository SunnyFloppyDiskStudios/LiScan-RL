using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance { get; private set; }

    private void Awake() {
        if (instance == null) {
            Debug.LogError("more than 1 audiomanager");
        }
        instance = this;
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos) {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateInstance(EventReference eventRef) {
        EventInstance evI = RuntimeManager.CreateInstance(eventRef);
        return evI;
    }
}
