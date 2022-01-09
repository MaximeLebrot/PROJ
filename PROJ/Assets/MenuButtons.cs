using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

    private List<UIButton> buttons;
    private FadeGroup fadeGroup;
    
    public void Initialize() {
        buttons = GetComponentsInChildren<UIButton>().ToList();
        fadeGroup = GetComponent<FadeGroup>();
        fadeGroup.SetAlpha(0);
    }

    public async Task EnableButtons() => await fadeGroup.InitiateFade(FadeMode.FadeIn);
    public async Task DisableButtons() {
        
        await fadeGroup.InitiateFade(FadeMode.FadeOut);
        
        gameObject.SetActive(false);
    }

    public async Task MoveButtons(UIButton pressedButton) {

        List<Task> moveTasks = new List<Task>(buttons.Count);

        foreach (UIButton button in buttons) {
            button.SetState(button == pressedButton ? ButtonState.Selected : ButtonState.NotSelected);
            
            moveTasks.Add(button.Move());
        }
        
        await Task.WhenAll(moveTasks);
        
    }
    
    public async Task ResetButtons() {

        List<Task> moveTasks = new List<Task>(buttons.Count);

        foreach (UIButton button in buttons) {
            
            button.SetState(ButtonState.Inactive);
            
            moveTasks.Add(button.Move());
            
        }

        await Task.WhenAll(moveTasks);

    }
}
