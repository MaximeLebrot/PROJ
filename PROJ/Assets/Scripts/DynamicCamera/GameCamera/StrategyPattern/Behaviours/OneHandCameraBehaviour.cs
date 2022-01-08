
using System.Security.Cryptography;
using NewCamera;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Camera/Camera Behaviours/One Hand Camera Behaviour", fileName = "One Hand Camera Behaviour")]
public class OneHandCameraBehaviour : BaseCameraBehaviour {

    public override void ManipulatePivotTarget(CustomInput input) => pivotTarget.rotation = characterModel.rotation;
}
