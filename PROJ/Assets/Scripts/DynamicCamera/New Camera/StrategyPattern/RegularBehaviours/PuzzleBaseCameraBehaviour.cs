using UnityEngine;

namespace NewCamera
{
    [System.Serializable]
    public class PuzzleBaseCameraBehaviour : BaseCameraBehaviour {
        
        private readonly Vector3 eulerRotation = new Vector3(50, 0, 0);
        private Transform puzzle;
        
        public PuzzleBaseCameraBehaviour(Transform transform, Transform target, BehaviourData behaviourValues) : base(transform, target, behaviourValues) {}
        
        public void AssignRotation(Transform puzzleRotation) => puzzle = puzzleRotation;

        public override Vector3 ExecuteMove(Vector3 calculatedOffset) {
            return thisTransform.position = Vector3.SmoothDamp(thisTransform.position, target.position + puzzle.localRotation * behaviourValues.Offset, ref referenceVelocity, behaviourValues.FollowSpeed);
        }

        public override Quaternion ExecuteRotate() {
            return Quaternion.Lerp(thisTransform.rotation, Quaternion.Euler(puzzle.localEulerAngles + eulerRotation), Time.deltaTime);
        }
    }
}
