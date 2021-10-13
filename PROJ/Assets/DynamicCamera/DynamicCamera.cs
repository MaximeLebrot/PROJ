using UnityEngine;

namespace DynamicCamera {
    
    public class DynamicCamera : MonoBehaviour {

        [Header("Camera Follow Target")]
        [SerializeField] private Transform target;
        
        [Header("Settings")]
        [SerializeField] private Vector3 offset;
        [SerializeField] private float mouseSensitivity;
        [SerializeField][Range(0, 10)] private float speed;
        [SerializeField][Range(0, 10)] private float transitionSpeed;
        
        private CameraBehaviour cameraBehaviour;
        
        private Transform thisTransform;
        
        private void Awake() {
            cameraBehaviour = new FollowBehaviour(offset, mouseSensitivity, speed);
            Cursor.lockState = CursorLockMode.Locked;
            thisTransform = transform;

            Puzzle.PuzzleInit += ChangeBehaviour;
            

        }
        
        private void LateUpdate() {
            cameraBehaviour.Behave(thisTransform, target);

            if (Input.GetKeyDown(KeyCode.V)) {
                target = FindObjectOfType<PlayerController>().transform;
                cameraBehaviour = new FollowBehaviour(offset, mouseSensitivity, speed);
            }
            
        }

        
        private void ChangeBehaviour(Transform newTarget) {
            target = newTarget;
            cameraBehaviour = new PuzzleBehaviour(transitionSpeed);
        }

    }
    
}

