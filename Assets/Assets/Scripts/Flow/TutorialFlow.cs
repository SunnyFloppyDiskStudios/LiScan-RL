using UnityEngine;
using DG.Tweening;
using LIDAR;
using UnityEngine.InputSystem;

public class TutorialFlow : MonoBehaviour {
    public Transform beginObject;
    public Transform beginGUI;
    public Transform beginText;

    public Transform objectiveObject;
    public Transform objectiveText;
    
    public Transform gun;
    public Transform rightController;

    public gunScript gs;

    public Transform controlObject;
    public Transform shootGUI;
    public Transform moveGUI;

    public Transform winGUI;

    private int state;

    private InputAction X;
    private InputAction Shoot;

    void Start() {
        X = InputSystem.actions.FindAction("X");
        Shoot = InputSystem.actions.FindAction("ClickAction");
    }
    
    void Update() {
        if (state == 0) {
            gs.canShoot = false;
            ShowGUIFancy(beginGUI);

            state = 1;
        }

        if (state == 1) {
            if (X.WasPressedThisFrame()) {
                HideGUIFancy(beginGUI);
                ToggleGUI(beginObject, false);
                state = 2;
            }
        }

        if (state == 2) {
            gs.canShoot = true;
            state = 3;
        }

        if (state == 3) {
            
        }
    }

    private void ShowGUIFancy(Transform gui) {
        gui.DOScale(new Vector3(-1f, 0.1f, 0f), 0.5f)
            .OnComplete(() => gui.DOScale(new Vector3(-1f, 1f, 1f), 0.5f));
    }

    private void HideGUIFancy(Transform gui) {
        gui.DOScale(new Vector3(-1f, 0.1f, 0f), 0.5f)
            .OnComplete(() => gui.DOScale(Vector3.zero, 0.5f));
    }

    private void ToggleGUI(Transform gui, bool active) {
        if (active) {
            gui.gameObject.SetActive(true);
        } else {
            gui.gameObject.SetActive(false);
        }
        
    }
}

