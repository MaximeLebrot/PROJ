using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI percentText;
    [SerializeField] private Slider slider;
    
    private void Awake() {
        
        LoadLevel("TutorialMainHub");
        
    }
    
    private async void LoadLevel(string sceneName) {

        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(sceneName);

        await LoadScene(sceneLoading);
        
    }

    private async Task LoadScene(AsyncOperation loadOperation) {

        percentText.text = "0 %";
        
        while (!loadOperation.isDone) {

            float progress = Mathf.Clamp01(loadOperation.progress / .9f);
            
            slider.value = progress;
            
            await Task.Yield();
        }
        
        Debug.Log("Operation done");
        
    }
    
}
