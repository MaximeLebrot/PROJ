using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangePlaytestScene : MonoBehaviour
{
    Dropdown m_Dropdown;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        //Fetch the Dropdown GameObject
        m_Dropdown = GetComponent<Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(Dropdown change)
    {
        Debug.Log(change.options[change.value].text);
        SceneManager.LoadScene(change.options[change.value].text);
    }
}
