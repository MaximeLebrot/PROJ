using UnityEngine;

namespace DynamicCamera {
    
    public class DynamicCamera : MonoBehaviour {
        
        [SerializeField] private Transform target;
        [SerializeField] private LayerMask collisionMask;

        [SerializeField] private Vector3 offset;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float mouseSensitivity;
       
        private Vector2 input;
        
        private const string XRotationInputName = "Mouse Y";
        private const string YRotationInputName = "Mouse X";
        private new SphereCollider collider;
        
        private void Awake() {
            Cursor.lockState = CursorLockMode.Locked;
            collider = GetComponent<SphereCollider>();
        }
        
        private void LateUpdate() {
            GetInput();
            RotateCamera();
            MoveCamera();
        }
        
        private void GetInput() {
            input.x -= Input.GetAxis(XRotationInputName) * mouseSensitivity;
            input.y += Input.GetAxis(YRotationInputName) * mouseSensitivity;

            input.x = Mathf.Clamp(input.x, -80, 80);
        }
        
        private void RotateCamera() {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(input.x, input.y, 0), rotationSpeed * Time.time);
        }

        private void MoveCamera() {

            Vector3 collisionOffset = transform.rotation * offset;
            
            if (Physics.SphereCast(target.position, collider.radius, collisionOffset.normalized, out RaycastHit hitInfo, collisionOffset.magnitude, collisionMask)) {

                Vector3 projectOnPlane = Vector3.ProjectOnPlane(collisionOffset, hitInfo.normal);

                collisionOffset = projectOnPlane;

            }

            Debug.DrawLine(target.position, target.position + collisionOffset, Color.red);
            transform.position = Vector3.Slerp(transform.position, target.position + collisionOffset, movementSpeed * Time.deltaTime);    

        }
    }
    
    
    
}

