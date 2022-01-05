//=======AUTO GENERATED CODE=========//
//=======Tool Author: Jonathan Haag=========//

public class MouseSensitivity : MenuSlider {
    
    public static float Sensitivity { get; private set; }

    protected override void ExecuteAdditionalLogic() => Sensitivity = slider.value;
}
