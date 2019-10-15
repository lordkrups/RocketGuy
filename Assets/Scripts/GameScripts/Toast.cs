using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toast : MonoBehaviour
{
    public UISprite toastSprite;
    public UILabel toastLabel;

    private void Awake()
    {
        toastSprite.Off();
        toastSprite.alpha = 0f;

    }

    public void ToastSetting(string t)
    {
        toastLabel.text = t;
        StartCoroutine(PlayToast());
    }

    IEnumerator PlayToast()
    {
        toastSprite.On();

        while (toastSprite.alpha < 1f)
        {
            toastSprite.alpha += 0.1f;
            //yield return new WaitForSeconds(0.1f);
            //break;
        }

        yield return new WaitForSeconds(1.5f);

        while (toastSprite.alpha > 0f)
        {
            toastSprite.alpha -= 0.1f;
            //yield return new WaitForSeconds(0.1f);
            //break;
        }
        toastSprite.Off();
    }
}
