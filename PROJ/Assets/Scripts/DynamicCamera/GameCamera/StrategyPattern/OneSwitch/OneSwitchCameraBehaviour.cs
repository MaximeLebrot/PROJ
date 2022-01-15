using NewCamera;

[System.Serializable]
public class OneSwitchCameraBehaviour : BaseCameraBehaviour
{
    public override void ManipulatePivotTarget(CustomInput input) {
        pivotTarget.rotation = pivotTarget.parent.rotation;
    }
}
