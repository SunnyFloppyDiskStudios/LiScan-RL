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
    public Transform leftController;

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
        
        ToggleGUI(beginObject, false);
    }
    
    void Update() {
        switch (state) {
            case 0:
                gs.canShoot = false;
                ShowGUIFancy(beginGUI, 1);
                break;
            
            case 1:
                if (X.WasPressedThisFrame()) {
                    HideGUIFancy(beginGUI, 2);
                    ToggleGUI(beginObject, false);
                }
                break;
            
            case 2:
                gs.canShoot = true;
                state = 3;
                break;
            
            case 3:
                ToggleGUI(controlObject.transform, true);
                ShowGUIFancy(shootGUI, 4);
                break;
            
            case 4:
                if (Shoot.WasPressedThisFrame()) {
                    HideGUIFancy(shootGUI, 5);
                }
                break;
            
            case 5:
                ShowGUIFancy(moveGUI, 6);
                break;
            
            case 6:
                
                
                break;
        }
    }

    private void ShowGUIFancy(Transform gui, int s) {
        gui.DOScale(new Vector3(-1f, 0.1f, 0f), 0.5f)
            .OnComplete(() => gui.DOScale(new Vector3(-1f, 1f, 1f), 0.5f)
                .OnComplete(() => state = s));
    }

    private void HideGUIFancy(Transform gui, int s) {
        gui.DOScale(new Vector3(-1f, 0.1f, 0f), 0.5f)
            .OnComplete(() => gui.DOScale(Vector3.zero, 0.5f)
                .OnComplete(() => state = s));
    }

    private void ToggleGUI(Transform gui, bool active) {
        gui.gameObject.SetActive(active);
    }
}

