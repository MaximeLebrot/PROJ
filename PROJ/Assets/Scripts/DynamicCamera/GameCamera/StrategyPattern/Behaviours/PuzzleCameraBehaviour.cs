using UnityEngine;

namespace NewCamera
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Camera/Camera Behaviours/Puzzle Behaviour", fileName = "Puzzle Behaviour")]
    public class PuzzleCameraBehaviour : BaseCameraBehaviour {
        
        private readonly Vector3 eulerRotation = new Vector3(50, 0, 0);
        private Transform puzzle;
        
        public void AssignRotation(Transform puzzleRotation) => puzzle = puzzleRotation;

        public override Vector3 ExecuteMove(Vector3 calculatedOffset) {
            return thisTransform.position = Vector3.SmoothDamp(thisTransform.position, pivotTarget.position + puzzle.localRotation * behaviourValues.Offset, ref referenceVelocity, behaviourValues.FollowSpeed);
        }

        public override Quaternion ExecuteRotate() {
            return Quaternion.Lerp(thisTransform.rotation, Quaternion.Euler(puzzle.localEulerAngles + eulerRotation), Time.deltaTime);
        }
    }
}
