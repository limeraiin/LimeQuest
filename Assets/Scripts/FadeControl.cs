using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeControl : MonoBehaviour
{
    private Animator anim;
    private bool isDone;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeStart(int sceneIndex)
    {
        StartCoroutine(Fading(sceneIndex));
    }

    private IEnumerator Fading(int sceneIndex)
    {
        anim.SetTrigger("FadeOut");
        while (!isDone)
        {
            yield return null;
        }

        isDone = false;
        SceneManager.LoadScene(sceneIndex);
    }

    public void SetDone()
    {
        isDone = true;
    }
}
