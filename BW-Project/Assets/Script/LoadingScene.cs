using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;

    public static LoadingScene instance;

    void Awake()
    {
        instance = this;
    }

    public void LoadScene(string scene)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsynchronously(scene));
    }

    private IEnumerator LoadAsynchronously(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/.9f);
            slider.value = progress;

            yield return null;
        }
    }
}
