using System.Collections;
using System.Collections.Generic;
using NewCamera;
using UnityEngine;

namespace NewCamera
{
    public class PuzzleBaseCameraBehaviour : BaseCameraBehaviour {
        
        private readonly Vector3 eulerRotation = new Vector3(50, 0, 0);
        private Transform puzzle;
        
        public PuzzleBaseCameraBehaviour(Transform transform, Transform target, OffsetAndCameraSpeed values) : base(transform, target, values) {}
        
        public void AssignRotation(Transform puzzleRotation) => this.puzzle = puzzleRotation;

        public override Vector3 ExecuteMove(Vector3 calculatedOffset)
        {
            return thisTransform.position = Vector3.SmoothDamp(thisTransform.position, target.position + puzzle.localRotation * calculatedOffset, ref referenceVelocity, .5f);
        }

        public override Quaternion ExecuteRotate()
        {
            Vector3 rotation = puzzle.localEulerAngles + eulerRotation;
            
            return Quaternion.Euler(rotation);
        }
    }
    
}
