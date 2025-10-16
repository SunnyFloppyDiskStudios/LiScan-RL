using UnityEngine;
using DG.Tweening;
using LIDAR;
using UnityEngine.InputSystem;

public class TutorialFlow : MonoBehaviour {
    public Transform cam;
    
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

    private bool cPosSet;
    private Vector3 cPos;

    void Start() {
        X = InputSystem.actions.FindAction("X");
        Shoot = InputSystem.actions.FindAction("ClickAction");
        
        ToggleGUI(beginObject, true);
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
                ToggleGUI(objectiveObject, true);
                ShowGUIFancy(objectiveText, 3);
                
                break;
            
            case 3:
                if (X.WasPressedThisFrame()) {
                    HideGUIFancy(objectiveText, 4);
                    gs.canShoot = true;
                }
                break;
            
            case 4:
                ToggleGUI(controlObject.transform, true);
                ShowGUIFancy(shootGUI, 5);
                break;
            
            case 5:
                if (Shoot.WasPressedThisFrame()) {
                    HideGUIFancy(shootGUI, 6);
                }
                break;
            
            case 6:
                ShowGUIFancy(moveGUI, 7);
                break;
            
            case 7:
                var offset = 2f;
                
                if (!cPosSet) { 
                    cPos = cam.position;
                }

                if (cam.position.x > cPos.x + offset ||
                    cam.position.y > cPos.y + offset ||
                    cam.position.z > cPos.z + offset) {
                    HideGUIFancy(moveGUI, 8);
                }
                break;
        }
    }

    private void ShowGUIFancy(Transform gui, int s) {
        state = s;
        gui.DOScale(new Vector3(-0.1f, 0.1f, 1f), 0.2f)
            .OnComplete(() => gui.DOScale(new Vector3(-1f, 0.1f, 1f), 0.5f)
                .OnComplete(() => gui.DOScale(new Vector3(-1f, 1f, 1f), 0.5f)));
    }

    private void HideGUIFancy(Transform gui, int s) {
        state = s;
        gui.DOScale(new Vector3(-1f, 0.1f, 1f), 0.2f)
            .OnComplete(() => gui.DOScale(new Vector3(0f, 0.1f, 0f), 0.5f)
                .OnComplete(() => gui.DOScale(Vector3.zero, 0.5f)));
    }

    private void ToggleGUI(Transform gui, bool active) {
        gui.gameObject.SetActive(active);
    }
}

