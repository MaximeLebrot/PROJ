using UnityEngine;

namespace CameraBehaviours {

    [CreateAssetMenu(menuName = "Camera/Camera Behaviours/Puzzle Behaviour (no free movement)", fileName = "Puzzle Behaviour")]
    public class PuzzleBehaviour : CameraBehaviour {

        [SerializeField] private Vector3 eulerRotation;
        [SerializeField] private float maxSpeed;

        private Transform puzzleTransform;

        public void AssignRotation(Transform puzzleTransform) => this.puzzleTransform = puzzleTransform;

        protected override void Behave() {

            transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + puzzleTransform.localRotation * offset, ref velocity, cameraMovementSpeed, maxSpeed);

            transform.eulerAngles = puzzleTransform.localEulerAngles + eulerRotation;
        }
    }
}