using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//SAVE HASHES
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

    public abstract void UpdateSettings(SettingsData settingsData);

    public abstract void SaveSettings(ref SettingsData settingsData);
    protected UIMenuItem ExtractMenuItem(string menuName) => menuOptions[menuName.GetHashCode()];
}
