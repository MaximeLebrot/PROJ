using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DropDownSceneChanger : MonoBehaviour {

    private TMP_Dropdown dropDownList;

    private void Awake() {
        dropDownList = GetComponent<TMP_Dropdown>();
        
        dropDownList.onValueChanged.AddListener(ChangeSceneOnSelected);
        
    }

    private void ChangeSceneOnSelected(int index) => EventHandler<LoadSceneEvent>.FireEvent(new LoadSceneEvent(dropDownList.options[index].text));
}
