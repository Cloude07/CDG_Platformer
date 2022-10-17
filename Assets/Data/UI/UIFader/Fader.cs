using System;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private const string FADER_PATH = "UI/Objects/Fade";

    private static Fader _instance;
    [SerializeField] Animator animator;
    public static Fader instance
    {
        get
        {
            if(_instance == null)
            {
                var prefab = Resources.Load<Fader>(FADER_PATH);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public bool isFading { get; private set; }

    private Action _fadedInCallback;
    private Action _fadedOutCallback;

    public void FadeIn(Action fadedInCalback)
    {
        if (isFading)
            return;

        isFading = true;
        _fadedInCallback = fadedInCalback;
        animator.SetBool("Faded", true);
    }

    public void FadeOut(Action fadedOutCalback)
    {
        if (isFading)
            return;

        isFading = true;
        _fadedOutCallback = fadedOutCalback;
        animator.SetBool("Faded", false);
    }

    private void Handle_fadeInAnimationOver()
    {
        _fadedInCallback?.Invoke();
        _fadedInCallback = null;
        isFading = false;
    }
    private void Handle_fadeOutAnimationOver()
    {
        _fadedOutCallback?.Invoke();
        _fadedOutCallback = null;
        isFading = false;
    }
}
