using System.Collections.Generic;
using UnityEngine;

public class MenuAnimator : MonoBehaviour {
    
    private Dictionary<string, int> pageHashes = new Dictionary<string, int>();
    private readonly Stack<int> subMenuDepth = new Stack<int>();
    
    private Animator animator;
    
    private void Awake() {
        animator = GetComponent<Animator>();

        foreach (AnimatorControllerParameter parameter in animator.parameters) 
            pageHashes.Add(parameter.name, Animator.StringToHash(parameter.name));
    }

    public void SetTrigger(string parameterName) => animator.SetTrigger(pageHashes[parameterName]);

    public void SetBool(string parameterName, bool value) {
        
        animator.SetBool(pageHashes[parameterName], value);
        
        subMenuDepth.Push(pageHashes[parameterName]);
    }

    public void Back() {

        if (InsideSubMenu() == false)
            return;
        
        int currentLevel = subMenuDepth.Pop();
        
        animator.SetBool(currentLevel, !animator.GetBool(currentLevel));
    }

    public void EnableAnimator(bool enable) {
        if(enable)
           animator.Rebind();
        
        animator.enabled = enable;
    }

    public bool InsideSubMenu() => subMenuDepth.Count > 0;

}
