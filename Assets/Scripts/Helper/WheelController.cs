using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public LevelManager levelManager;
    public UISprite wheelSprite;
    public List<UISprite> spriteList;
    public float spinSpeed;
    public bool stopSpin;

    public void SetImages(string sn1, string sn2, string sn3, string sn4, string sn5, string sn6)
    {
        spriteList[0].spriteName = sn1;
        spriteList[1].spriteName = sn2;
        spriteList[2].spriteName = sn3;
        spriteList[3].spriteName = sn4;
        spriteList[4].spriteName = sn5;
        spriteList[5].spriteName = sn6;

        Quaternion zeroPos = new Quaternion(0f, 0f, 0f, 0f);
        transform.rotation = zeroPos;
        stopSpin = false;
    }

    public void StartSpin()
    {
        spinSpeed = Random.Range(4.6f, 5.4f);
        //Debug.Log(spinSpeed);
        StartCoroutine(SpinForTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopSpin)
        {
            transform.Rotate(Vector3.forward, 45 * Time.deltaTime * spinSpeed);
        }
    }

    IEnumerator SpinForTime()
    {
        yield return new WaitForSeconds(1f);

        spinSpeed = 4f;

        yield return new WaitForSeconds(1f);

        spinSpeed = 3f;
        yield return new WaitForSeconds(1f);

        spinSpeed = 2f;
        yield return new WaitForSeconds(1f);

        spinSpeed = 1f;
        yield return new WaitForSeconds(1f);
        levelManager.audioManager.PlaySFXClip("spinend");

        spinSpeed = 0f;
        yield return new WaitForSeconds(0.5f);

        stopSpin = true;
    }
}
