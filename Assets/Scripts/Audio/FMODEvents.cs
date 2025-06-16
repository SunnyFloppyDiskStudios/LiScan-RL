using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour {
    public static FMODEvents instance { get; private set; }
    
    [field: SerializeField] public EventReference gunShoot { get; private set; }
    
    private void Awake() {
        if (instance == null) {
            Debug.LogError("more than 1 fmod event thingy");
        }
        instance = this;
    }

}
