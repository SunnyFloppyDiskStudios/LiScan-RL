using UnityEngine;
using DG.Tweening;
using Player.LIDAR;
using UnityEngine.InputSystem;

namespace Player.Flow {
    public class TutorialFlow : MonoBehaviour {
        // controls the UI's for tutorials in the game. and the main menu but that's irrelevant.
        
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
        
        private Transform cam;

        private bool cPosSet;
        private Vector3 cPos;

        void Start() {
            // get stuff
            X = InputSystem.actions.FindAction("X");
            Shoot = InputSystem.actions.FindAction("ClickAction");
            cam = Camera.main.transform;
            
            ToggleGUI(beginObject, true);
            
            gun.localScale = Vector3.zero;
        }
        
        void Update() {
            // q: why use a switch/case?
            // a: because it's cleaner, efficient, more readable, etc.
            
            switch (state) {
                case 0:
                    // user just joined. show the play screen.
                    gs.canShoot = false;
                    ShowGUIFancy(beginGUI, 1);
                    break;
                
                case 1:
                    if (X.WasPressedThisFrame()) {
                        // user activated game. show the objective.
                        HideGUIFancy(beginGUI, 2);
                        ToggleGUI(beginObject, false);
                    }
                    break;
                
                case 2:
                    // show the objective.
                    ToggleGUI(objectiveObject, true);
                    ShowGUIFancy(objectiveText, 3);
                    
                    break;
                
                case 3:
                    // user seen objective. allow them to play.
                    if (X.WasPressedThisFrame()) {
                        HideGUIFancy(objectiveText, 4);
                        gs.canShoot = true;
                        gun.DOScale(Vector3.one, 0.2f);
                    }
                    break;
                
                case 4:
                    // show how to shoot
                    ToggleGUI(controlObject.transform, true);
                    ShowGUIFancy(shootGUI, 5);
                    break;
                
                case 5:
                    // hide how to shoot
                    if (Shoot.WasPressedThisFrame()) {
                        HideGUIFancy(shootGUI, 6);
                    }
                    break;
                
                case 6:
                    // show how to move (really just telling them that they *can* move)
                    ShowGUIFancy(moveGUI, 7);
                    break;
                
                case 7:
                    // hide they can move. bit more complicated because it's not an input action from the xr controller.
                    var offset = 2f;
                    
                    if (!cPosSet) { 
                        cPos = cam.position;
                    }
                    
                    // hey that layout looks funny... it'S AIIIII NOOOO IT'S AIII NOOOOOOOOO
                    // no actually it's just more readable. not ai.
                    if (cam.position.x > cPos.x + offset ||
                        cam.position.y > cPos.y + offset ||
                        cam.position.z > cPos.z + offset) {
                        HideGUIFancy(moveGUI, 8);
                    }
                    break;
            }
        }
        // Tweening as functions. It's wayyyyyyy easier because i dont have to write this on EVERY switch statement.
        private void ShowGUIFancy(Transform gui, int s) {
            state = s;
            gui.DOScale(new Vector3(-0.1f, 0.1f, 1f), 0.2f)
                .OnComplete(() => gui.DOScale(new Vector3(-1f, 0.1f, 1f), 0.25f)
                    .OnComplete(() => gui.DOScale(new Vector3(-1f, 1f, 1f), 0.25f)));
        }

        private void HideGUIFancy(Transform gui, int s) {
            state = s;
            gui.DOScale(new Vector3(-1f, 0.1f, 1f), 0.2f)
                .OnComplete(() => gui.DOScale(new Vector3(0f, 0.1f, 0f), 0.25f)
                    .OnComplete(() => gui.DOScale(Vector3.zero, 0.25f)));
        }

        private void ToggleGUI(Transform gui, bool active) {
            gui.gameObject.SetActive(active);
        }
    }
}

