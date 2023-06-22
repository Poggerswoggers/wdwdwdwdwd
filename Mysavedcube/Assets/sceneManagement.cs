using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagement : MonoBehaviour
{
    //public enum SceneName {None, MainMenu, Tutorial, Level1, Level2, Level3, Options}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectScene()
    {
        
    }

    public void Play(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }



    public void SwitchScene(string sceneName)
    {       
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
