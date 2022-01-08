using UnityEngine;

namespace NewCamera
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Camera/Camera Behaviours/Puzzle Behaviour", fileName = "Puzzle Behaviour")]
    public class PuzzleCameraBehaviour : BaseCameraBehaviour {
        
        [SerializeField] public BehaviourData _3x3;
        [SerializeField] private BehaviourData _5x5;
        [SerializeField] private BehaviourData _7x7;
        [SerializeField] private BehaviourData _9x9;
        
        private readonly Vector3 eulerRotation = new Vector3(50, 0, 0);
        private Puzzle puzzle;
        
        public void InitializePuzzleCamera(Puzzle puzzle) {
            this.puzzle = puzzle;

            switch (puzzle.GetGrid().Size) {
                case 3:
                    behaviourValues = _3x3;
                    break;
                case 5:
                    behaviourValues = _5x5;
                    break;
                case 7:
                    behaviourValues = _7x7;
                    break;
                case 9:
                    behaviourValues = _9x9;
                    break;
            }
            
        }

        public override Vector3 ExecuteMove(Vector3 calculatedOffset) {
            return thisTransform.position = Vector3.SmoothDamp(thisTransform.position, pivotTarget.position + puzzle.transform.localRotation * behaviourValues.Offset, ref referenceVelocity, behaviourValues.FollowSpeed);
        }

        public override Quaternion ExecuteRotate() {
            return Quaternion.Lerp(thisTransform.rotation, Quaternion.Euler(puzzle.transform.localEulerAngles + eulerRotation), Time.deltaTime);
        }
        
    }
}
