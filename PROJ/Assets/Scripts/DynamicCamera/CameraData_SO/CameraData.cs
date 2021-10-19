using UnityEngine;

[CreateAssetMenu(menuName = "DynamicCamera/CameraData", fileName = "Camera Data")]
public class CameraData : ScriptableObject {

    public Vector3 offset;
    public float movementSpeed;
    public float rotationSpeed;
    public float mouseSensitivity;

}
