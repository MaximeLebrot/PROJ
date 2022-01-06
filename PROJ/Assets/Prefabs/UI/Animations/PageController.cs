using System;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour {

    public List<MenuSettings> PageObjects { get; private set; }
    
    
    private MenuSettings currentActivePage;

    private Action onDone;

    private MenuSettings newPage;

    public event Action<bool> OnSuspendInput;

    //F�rl�t Jonathan /martin
    GameMenuController gameMenuController;

    public void Initialize() {
        
        gameMenuController = GetComponentInParent<GameMenuController>();
        
        PageObjects = new List<MenuSettings>();
        
        for (int i = 0; i < transform.childCount; i++) {

            transform.GetChild(i).gameObject.SetActive(true);
            
            transform.GetChild(i).TryGetComponent(out MenuSettings menuSettings);

            if (menuSettings != null) {
                menuSettings.Initialize();
                PageObjects.Add(menuSettings);
            }
            
            transform.GetChild(i).gameObject.SetActive(false);
            
        }

        UIButton.onButtonClicked += HandleButtonClicked;
        UIButton.onResetCalled += ResetPages;
    }


    public UIMenuItemBase FindRequestedOption<T>() {
        foreach (MenuSettings menuSetting in PageObjects) {

            if (menuSetting.HasMenuItem<T>()) {
                Debug.Log(menuSetting.HasMenuItem<T>());
                return menuSetting.GetOption<T>();
            }
                
        }

        return null;
    }
    
    
    private void HandleButtonClicked(UIButton clickedButton) {
        
        OnSuspendInput?.Invoke(true); //invoke with false in ActivatePage
        
        clickedButton.onMoveCallback += () => {
            SwitchPage(clickedButton.MenuSetting);
        };
    }
    

    public void ResetPages() {
        currentActivePage.DeactivatePage(DisableCurrentPage);
    }

    private void SwitchPage(MenuSettings page) {

        newPage = page;

        if (currentActivePage != null) {
            onDone = DisableCurrentPage;
            onDone += ActivateNewPage;
            currentActivePage.DeactivatePage(onDone);
        }
        else 
            ActivateNewPage();
    }

    private void DisableCurrentPage() {
        currentActivePage.gameObject.SetActive(false);
        currentActivePage = null;
    }
    
    private void ActivateNewPage() {
        currentActivePage = newPage; //HÄR SÄTTS NEW PAGE
        currentActivePage.gameObject.SetActive(true);
        currentActivePage.ActivatePage(() => OnSuspendInput?.Invoke(false));
        onDone = null;
        newPage = null;

        currentActivePage.SelectTopButton();
    }

    public bool IsPageActive() => currentActivePage != null;

    //Called from scene changer buttons (beta release) /Martin
    public void CloseMenuOnSceneChange() {
        DisableCurrentPage();
        gameMenuController.SceneChangerCloseMenu();
        EventHandler<SceneChangeEvent>.FireEvent(null);
    }
}
