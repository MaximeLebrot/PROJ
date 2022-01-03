using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//HASH STRINGS
public abstract class MenuSettings : MonoBehaviour {
 
    protected Dictionary<int, UIMenuItem> menuOptions;

    private FadeGroup fadeGroup;

    private FontChanger fontChanger;

    private Selectable topButtonReference;
    
    public void Initialize() {

        gameObject.SetActive(true);
        fadeGroup = GetComponent<FadeGroup>();
        fontChanger = GetComponent<FontChanger>();
        
        //Just in case
        foreach (CanvasGroup canvasGroup in GetComponentsInChildren<CanvasGroup>())
            canvasGroup.alpha = 0;
        
        menuOptions = new Dictionary<int, UIMenuItem>();

        List<UIMenuItem> childOptions = GetComponentsInChildren<UIMenuItem>().ToList();

        foreach (UIMenuItem menuItem in childOptions) {
            menuItem.GenerateID();
            menuOptions.Add(menuItem.ID, menuItem);
        }
            
        
        SubMenuInitialize();

        //fontChanger.GatherAllTextComponents();
        
        gameObject.SetActive(false);
    }
    

    protected virtual void SubMenuInitialize() {}
    
    public abstract void SetMenuItems(SettingsData settingsData);

    public abstract void ApplyItemValues(ref SettingsData settingsData);
    protected UIMenuItem ExtractMenuItem(string menuName) => menuOptions[menuName.GetHashCode()];
    
    public void FadeMenu(FadeMode fadeMode, Action onDone) {
        StartCoroutine(fadeGroup.Fade(fadeMode, onDone));
    }
    public void SelectTopButton()
    {
        if (!topButtonReference)
        {
           topButtonReference = GetComponentInChildren<Selectable>();
        }
        topButtonReference.Select();
    }
}
