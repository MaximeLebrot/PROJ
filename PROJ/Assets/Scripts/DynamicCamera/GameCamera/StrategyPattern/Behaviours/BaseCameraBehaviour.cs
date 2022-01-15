using System;
using UnityEngine;

namespace NewCamera {
        
    [Serializable]
    [CreateAssetMenu(menuName = "Camera/Camera Behaviours/Base Behaviour", fileName = "Base Behaviour")]
    public class BaseCameraBehaviour : ScriptableObject {
        
        [SerializeField] protected BehaviourData behaviourValues;
        
        protected Vector3 ReferenceVelocity;
        protected Quaternion PreviousRotation;
        protected Transform ThisTransform;
        protected Transform PivotTarget;
        protected Transform CharacterModel;
        
        public Vector3 DefaultCameraPosition => PivotTarget.position + PivotTarget.localRotation * behaviourValues.Offset;

        //"Constructor"
        public virtual void InjectReferences(Transform transform, Transform pivotTarget, Transform characterModel) {
            ThisTransform = transform;
            PivotTarget = pivotTarget;
            CharacterModel = characterModel;
        }

        public virtual void EnterBehaviour() {}

        public virtual Vector3 ExecuteMove(Vector3 calculatedOffset) {
            return Vector3.SmoothDamp(ThisTransform.position, PivotTarget.position + calculatedOffset, ref ReferenceVelocity, behaviourValues.FollowSpeed);
        }

        public virtual Quaternion ExecuteRotate() {
            
            Quaternion targetRotation = Quaternion.LookRotation((PivotTarget.position - ThisTransform.position) + PivotTarget.forward * behaviourValues.CameraLookAhead);
            
            return Quaternion.Slerp(ThisTransform.rotation, targetRotation, Time.deltaTime * behaviourValues.RotationSpeed);
        }

        public virtual Vector3 ExecuteCollision(GlobalCameraSettings data) {
            
            Vector3 collisionOffset = PivotTarget.rotation * behaviourValues.Offset;
            
            if (Physics.SphereCast(PivotTarget.position, data.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, data.CollisionMask))
                collisionOffset = collisionOffset.normalized * hitInfo.distance;

            return collisionOffset;
        }

        public virtual void ManipulatePivotTarget(CustomInput input) {

            Vector3 desiredRotation = PreviousRotation.eulerAngles;

            if (input.aim != Vector2.zero) {

                desiredRotation = PivotTarget.localEulerAngles + (Vector3)input.aim;

                desiredRotation = PreventCircleReset(desiredRotation);

                desiredRotation.x = Mathf.Clamp(desiredRotation.x, behaviourValues.ClampValues.x, behaviourValues.ClampValues.y);
            }
            else {
                PivotTarget.localRotation = PreviousRotation;
                return;
            }
            
            PivotTarget.localEulerAngles = desiredRotation;
            PreviousRotation = PivotTarget.localRotation;
        }

        
        protected T BehaviourData<T>() where T : BehaviourData => behaviourValues as T;

        protected Vector3 PreventCircleReset(Vector3 input) {
            if (input.x > 180)
                input.x -= 360;
            if (input.y > 180)
                input.y -= 360;

            return input;
        }
        
        public void ResetRotation() {
            PreviousRotation = CharacterModel.rotation;
            PivotTarget.localRotation = PreviousRotation;
        }
    }
}