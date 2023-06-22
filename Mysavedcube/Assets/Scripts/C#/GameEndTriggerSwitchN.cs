using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTriggerSwitchN : MonoBehaviour
{
    [SerializeField] string LevelToLoad;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LoadNextLevelCo());
    }

    IEnumerator LoadNextLevelCo()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(LevelToLoad);
    }

}