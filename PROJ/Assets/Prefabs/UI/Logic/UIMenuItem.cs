using System;
using UnityEngine;

[Serializable]
public abstract class UIMenuItem<T> : UIMenuItemBase {
    public abstract T GetValue();
    public abstract void SetValue(T value);

    protected virtual void ExecuteAdditionalLogic() {}
}

[Serializable]
public abstract class UIMenuItemBase : MonoBehaviour {

    public virtual void Initialize() {}

    public virtual void DemandFirstRead() {}

}
