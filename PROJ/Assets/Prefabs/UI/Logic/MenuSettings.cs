using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//HASH STRINGS
public abstract class MenuSettings : MonoBehaviour {
 
    protected Dictionary<int, UIMenuItem> menuOptions;

    private FadeGroup fadeGroup;
    
    private Selectable topButtonReference;
    
    public void Initialize() {

        gameObject.SetActive(true);
        fadeGroup = GetComponent<FadeGroup>();

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

    public void ActivatePage(Action onDone) => fadeGroup.InitiateFade(FadeMode.FadeIn, onDone);

    public void DeactivatePage(Action action) => fadeGroup.InitiateFade(FadeMode.FadeOut, action);

    protected virtual void SubMenuInitialize() {}
    
    public abstract void SetMenuItems(SettingsData settingsData);

    public abstract void ApplyItemValues(ref SettingsData settingsData);
    protected UIMenuItem ExtractMenuItem(string menuName) => menuOptions[menuName.GetHashCode()];
    
    public void SelectTopButton()
    {
        if (!topButtonReference)
        {
           topButtonReference = GetComponentInChildren<Selectable>();
        }
        topButtonReference.Select();
    }
}
