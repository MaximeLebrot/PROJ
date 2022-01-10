using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class MenuSettings : MonoBehaviour {
 
    protected Dictionary<Type, UIMenuItemBase> menuOptions;

    private FadeGroup fadeGroup;
    
    private Selectable topButtonReference;
    
    public void Initialize() {

        gameObject.SetActive(true);
        fadeGroup = GetComponent<FadeGroup>();

        //Just in case
        foreach (CanvasGroup canvasGroup in GetComponentsInChildren<CanvasGroup>())
            canvasGroup.alpha = 0;
        
        menuOptions = new Dictionary<Type, UIMenuItemBase>();

        List<UIMenuItemBase> childOptions = GetComponentsInChildren<UIMenuItemBase>().ToList();

        foreach (UIMenuItemBase menuItem in childOptions) {
            menuItem.Initialize();
            menuOptions.Add(menuItem.GetType(), menuItem);
        }
        
        SubMenuInitialize();
    }

    public void InvokeFirstRead() {
        foreach (UIMenuItemBase uiItem in menuOptions.Values) 
            uiItem.DemandFirstRead();
    }
    
    public UIMenuItemBase GetOption<T>() => menuOptions[typeof(T)];
    
    public async Task ActivatePage() => await fadeGroup.InitiateFade(FadeMode.FadeIn);

    public async Task DisablePage() => await fadeGroup.InitiateFade(FadeMode.FadeOut);

    public bool HasMenuItem<T>() => menuOptions.ContainsKey(typeof(T));
    
    protected virtual void SubMenuInitialize() {}
    
    public abstract void SetMenuItems(SettingsData settingsData);

    public abstract void ApplyItemValues(ref SettingsData settingsData);
 
    public void SelectTopButton()
    {
        if (!topButtonReference)
        {
           topButtonReference = GetComponentInChildren<Selectable>();
        }
        topButtonReference.Select();
    }
    
}

