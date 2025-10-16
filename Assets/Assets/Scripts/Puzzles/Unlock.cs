using DG.Tweening;
using UnityEngine;

namespace Player.Puzzles {
    public class Unlock : MonoBehaviour {
        // when shot properly, activate the blocks
        
        public Renderer block1;
        public Renderer usb1;
        public Renderer block2;
        public Renderer usb2;
        public Renderer block3;
        public Renderer usb3;

        public Transform winGUI;
        private bool hasWon;

        void Update() {
            if (block1 is not null && usb1 is not null) {
                usb1.enabled = block1.enabled;
            }

            if (block2 is not null && usb2 is not null) {
                usb2.enabled = block2.enabled;
            }

            if (block3 is not null && usb3 is not null) {
                usb3.enabled = block3.enabled;
            }
            
            // woah. you win!
            if (usb1 is not null && usb2 is not null && usb3 is not null && 
                usb1.enabled && usb2.enabled && usb3.enabled && !hasWon) {
                hasWon = true;
                winGUI.DOScale(new Vector3(-0.1f, 0.1f, 1f), 0.2f)
                    .OnComplete(() => winGUI.DOScale(new Vector3(-1f, 0.1f, 1f), 0.5f)
                        .OnComplete(() => winGUI.DOScale(new Vector3(-1f, 1f, 1f), 0.5f)));
            }
        }
    }
}
