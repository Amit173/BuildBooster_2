using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Loader Class that contains loader animation coroutine..
public class Loader : MonoBehaviour
{
    public Image ig;
    // To activate the loading panel..
    public void Active()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(loading());
    }
    // To deactivate the loading panel..
    public void Deactive()
    {
        StopAllCoroutines();
        this.gameObject.SetActive(false);
    }
    // Main animation coroutine
    public IEnumerator loading()
    {
        yield return new WaitForEndOfFrame();

        if (ig.fillClockwise)
        {
            ig.fillAmount += 1 * Time.deltaTime;
            if (ig.fillAmount == 1)
                ig.fillClockwise = false;
            StartCoroutine(loading());
        }
        else
        {
            ig.fillAmount -= 1 * Time.deltaTime;
            if (ig.fillAmount == 0f)
                ig.fillClockwise = true;
            StartCoroutine(loading());
        }
    }
}