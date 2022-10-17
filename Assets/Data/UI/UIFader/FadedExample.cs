using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadedExample : MonoBehaviour
{
    private const string SCENE_0 = "MainMenu";
    private const string SCENE_1 = "Level_0";

    private bool _isLoading;

    private static FadedExample _instance;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2))
            LoadScene(SCENE_1);

        if (Input.GetKey(KeyCode.Alpha1))
            LoadScene(SCENE_0);
    }

    private void LoadScene(string sceneName)
    {
        if (_isLoading)
            return;

        var currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == sceneName)
            throw new Exception("You are trying to load already loaded scene.");


        StartCoroutine(LoadSceneRoutine(sceneName));
    }



    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        _isLoading = true;

        var waitFading = true;
        Fader.instance.FadeIn(() => waitFading = false);

        while (waitFading)
            yield return null;

        AsyncOperation asyncLoader = SceneManager.LoadSceneAsync(sceneName);
        asyncLoader.allowSceneActivation = false;

        while (asyncLoader.progress < 0.9f)
        {
            yield return null;

        }


        asyncLoader.allowSceneActivation = true;

        waitFading = true;
        Fader.instance.FadeOut(() => waitFading = false);

        while (waitFading)
            yield return null;

        _isLoading = false;
    }

}
