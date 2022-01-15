using NewCamera;

[System.Serializable]
public class OneSwitchCameraBehaviour : BaseCameraBehaviour
{
    public override void ManipulatePivotTarget(CustomInput input) {
        PivotTarget.rotation = PivotTarget.parent.rotation;
    }
}
