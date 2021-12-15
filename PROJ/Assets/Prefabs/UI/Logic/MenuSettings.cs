using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//HASH STRINGS
public abstract class MenuSettings : MonoBehaviour {
 
    protected Dictionary<int, UIMenuItem> menuOptions;

    public void Initialize() {

        gameObject.SetActive(true);
        menuOptions = new Dictionary<int, UIMenuItem>();

        List<UIMenuItem> childOptions = GetComponentsInChildren<UIMenuItem>().ToList();
        
        foreach (UIMenuItem menuItem in childOptions) 
            menuOptions.Add(menuItem.ID, menuItem);
        
        SubMenuInitialize();
        
        gameObject.SetActive(false);
    }

    protected virtual void SubMenuInitialize() {}
    
    public abstract void SetMenuItems(SettingsData settingsData);

    public abstract void ApplyItemValues(ref SettingsData settingsData);
    protected UIMenuItem ExtractMenuItem(string menuName) => menuOptions[menuName.GetHashCode()];


    
}
