using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance { get; set; }
    
    [SerializeField] private GameObject loadingScreen = null;
    [SerializeField] private Slider slider = null;

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void RunRoadsterSimulation(){
        StartCoroutine(LoadAsync("TeslaRoadsterScene"));
    }
    
    public void RunSpaceXLaunches(){
        StartCoroutine(LoadAsync("LaunchesScene"));
    }

    public void RunMainMenu(){
        StartCoroutine(LoadAsync("MenuScene"));
    }
    
    public void Quit(){
        Application.Quit();
    }

    IEnumerator LoadAsync(string name){
        AsyncOperation operation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        loadingScreen.SetActive(true);
        while(!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}
