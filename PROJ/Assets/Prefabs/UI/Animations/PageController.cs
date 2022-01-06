using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageController : MonoBehaviour {

    public List<MenuSettings> PageObjects { get; private set; }
    private GraphicRaycaster graphicRaycaster;
    private MenuSettings currentActivePage;
    private Action onDone;
    private MenuSettings newPage;
    
    public bool InputSuspended { get; private set; }
    
    //F�rl�t Jonathan /martin
    GameMenuController gameMenuController;

    public void Initialize() {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
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

    private void OnEnable() => graphicRaycaster.enabled = true;

    public UIMenuItemBase FindRequestedOption<T>() {
        
        foreach (MenuSettings menuSetting in PageObjects) {
            
            if (menuSetting.HasMenuItem<T>()) 
                return menuSetting.GetOption<T>();
        }

        return null;
    }
    
    
    private void HandleButtonClicked(UIButton clickedButton) {
        InputSuspended = true;
        graphicRaycaster.enabled = false;
        clickedButton.onMoveCallback += () => {
            SwitchPage(clickedButton.MenuSetting);
        };
    }
    
    public void ResetPages() {
        
        InputSuspended = true;
        graphicRaycaster.enabled = false;
        Action callback = DisableCurrentPage;
        callback += () => {
            InputSuspended = false;
            graphicRaycaster.enabled = true;
        };
        currentActivePage?.DeactivatePage(callback);
    }

    private void SwitchPage(MenuSettings page) {

        newPage = page;

        if (currentActivePage != null) {
            onDone = DisableCurrentPage;
            onDone += ActivateNewPage;
            onDone += () => {
                graphicRaycaster.enabled = true;
                InputSuspended = false;
            };
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
        
        graphicRaycaster.enabled = false;
        InputSuspended = true;
        
        currentActivePage = newPage; //HÄR SÄTTS NEW PAGE
        currentActivePage.gameObject.SetActive(true);

        currentActivePage.ActivatePage(() => {
            graphicRaycaster.enabled = true;
            InputSuspended = false;
        });
        onDone = null;
        newPage = null;

        currentActivePage.SelectTopButton();
    }

    public bool IsPageActive() => currentActivePage != null;

    //Called from scene changer buttons (beta release) /Martin
    public void CloseMenuOnSceneChange() {
        ResetPages();
        gameMenuController.SceneChangerCloseMenu();
        EventHandler<SceneChangeEvent>.FireEvent(null);
    }
}
