using System.Collections;
using UnityEngine;

public class SpriteAnims : MonoBehaviour
{
    public UISprite sprite;
    public UISprite effect;
    public Coroutine _currentCoroutine;

    public bool effectOn;
    public float effectTime;

    private void Update()
    {
        if (effect.isActiveAndEnabled)
        {
            effectTime += Time.deltaTime;
        }

        if (effectTime >= 2f)
        {
            effect.Off();
            effectTime = 0f;
        }
    }

    public void FlashRed(int style = 0, float posX = 0, float posY = 0)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);

            _currentCoroutine = null;
        }

        if (style == 0)
        {
            _currentCoroutine = StartCoroutine(FlashRedCo());
        }
        if (style == 1)
        {
            _currentCoroutine = StartCoroutine(HurtFlashCo(posX, posY));
        }
        if (style == 2)
        {
            _currentCoroutine = StartCoroutine(CrashFlashCo(posX, posY));
        }
        if (style == 3)
        {
            _currentCoroutine = StartCoroutine(DeadFlashCo(posX, posY));
        }
        if (style == 4)
        {
            _currentCoroutine = StartCoroutine(PreShootFlash());
        }
    }

    IEnumerator PreShootFlash()
    {

        sprite.color = new Color32(0, 255, 70, 255);

        yield return new WaitForSeconds(0.5f);

        sprite.color = new Color32(255, 255, 0, 255);

        yield return new WaitForSeconds(0.5f);

        sprite.color = new Color32(255, 0, 0, 255);

        yield return new WaitForSeconds(0.5f);

        sprite.color = new Color32(255, 255, 255, 255);

        yield return new WaitForSeconds(0f);

    }
    IEnumerator FlashRedCo()
    {

        sprite.color = new Color32(255, 0, 0, 255);

        yield return new WaitForSeconds(0.1f);

        sprite.color = new Color32(255, 255, 255, 255);

        yield return new WaitForSeconds(0.15f);

    }
    IEnumerator HurtFlashCo(float posX, float posY)
    {
        Vector2 pos = new Vector2(posX, posY);
        effect.transform.position = pos;
        effect.spriteName = ("hit");
        effect.On();

        sprite.color = new Color32(255, 0, 0, 255);

        yield return new WaitForSeconds(0.1f);

        sprite.color = new Color32(255, 255, 255, 255);

        yield return new WaitForSeconds(0.15f);

        effect.Off();

    }
    IEnumerator CrashFlashCo(float posX, float posY)
    {
        Vector2 pos = new Vector2(posX, posY);
        effect.transform.position = pos;
        effect.spriteName = ("crash");
        effect.On();


        sprite.color = new Color32(255, 0, 0, 255);

        yield return new WaitForSeconds(0.1f);

        sprite.color = new Color32(255, 255, 255, 255);

        yield return new WaitForSeconds(0.15f);

        effect.Off();

    }
    IEnumerator DeadFlashCo(float posX, float posY)
    {


        sprite.color = new Color32(255, 0, 0, 255);

        yield return new WaitForSeconds(0.1f);

        sprite.color = new Color32(255, 255, 255, 255);

        yield return new WaitForSeconds(0.1f);

        sprite.color = new Color32(255, 0, 0, 255);

        yield return new WaitForSeconds(0.1f);

        sprite.color = new Color32(255, 0, 0, 255);

        yield return new WaitForSeconds(0.1f);

        Vector2 pos = new Vector2(posX, posY);
        effect.transform.position = pos;
        effect.spriteName = ("death");
        effect.On();

        yield return new WaitForSeconds(2f);

        effect.Off();
    }


}
