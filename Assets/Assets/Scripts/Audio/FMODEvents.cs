using UnityEngine;
using FMODUnity;

namespace Player.Audio {
    public class FMODEvents : MonoBehaviour {
        // controls all the events. an event is a sound that i want to play, like the shooting sound.
        public static FMODEvents instance { get; private set; }
    
        [field: SerializeField] public EventReference gunShoot { get; private set; }
    
        private void Awake() {
            if (instance is null) {
                Debug.LogError("more than 1 fmod event thingy");
            }
            instance = this;
        }

    }
}

