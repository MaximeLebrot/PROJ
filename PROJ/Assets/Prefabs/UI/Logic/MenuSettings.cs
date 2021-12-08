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
        
        gameObject.SetActive(false);
    }

    public abstract void UpdateMenuItems(SettingsData settingsData);

    public abstract void ExtractMenuItemValues(ref SettingsData settingsData);
    protected UIMenuItem ExtractMenuItem(string menuName) => menuOptions[menuName.GetHashCode()];
}
