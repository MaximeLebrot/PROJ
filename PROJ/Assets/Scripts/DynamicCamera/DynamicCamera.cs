using UnityEngine;

namespace DynamicCamera {
    
    public class DynamicCamera : MonoBehaviour {
        
        [SerializeField] private Transform target;
        [SerializeField] private CameraBehaviour cameraBehaviour;
        [SerializeField] private LayerMask layerMask;
        
        private Transform thisTransform;
        private SphereCollider collider;
        
        private void Awake() {
            Cursor.lockState = CursorLockMode.Locked;
            thisTransform = transform;
            collider = GetComponent<SphereCollider>();

        }
        
        private void LateUpdate() => cameraBehaviour.ExecuteBehaviour(thisTransform, target);

        private void Collision() {
            
            Vector3 CalculateNormalForce(Vector3 direction, Vector3 hitNormal ) {
                float dotProduct = Vector3.Dot(direction, hitNormal.normalized);

                if (dotProduct > 0)
                    dotProduct = 0;
        
                return -(dotProduct * hitNormal.normalized);
            }
            
            
            
            Physics.SphereCast(target.position, collider.radius, cameraBehaviour.cameraData.offset.normalized, out var hit,cameraBehaviour.cameraData.offset.magnitude, layerMask);

            if (!hit.collider) return;

            Debug.DrawLine(transform.position, hit.point, Color.blue);
            cameraBehaviour.cameraData.offset += CalculateNormalForce(cameraBehaviour.cameraData.offset, hit.normal);
        
            Collision();
        }
    }
    
    
    
}

