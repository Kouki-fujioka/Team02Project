using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public string sceneName;

    public Image fadeImage;
    public float fadeDuration = 1.0f;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {

    }

    public void aaaa()
    {
        StartCoroutine(SwichScene());
    }

    public IEnumerator SwichScene()
    {
       //isTransitioning = true;

       yield return StartCoroutine(Fade(1));

       SceneManager.LoadScene(sceneName);

        yield return null;

        yield return StartCoroutine(Fade(0));

        //isTransitioning = false;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
}
