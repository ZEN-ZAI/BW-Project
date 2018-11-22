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
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);

        }
        //DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string scene)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsynchronously(scene));
    }

    public void LoadingScreen(bool state)
    {
        loadingScreen.SetActive(state);
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
