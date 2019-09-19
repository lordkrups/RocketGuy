using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public UISlider introSlider;
    public float loaded;
    public bool gameLoadStarted, loadOne, loadTwo, loadThree, loadFour, loadFive, loadSix;
    public EverythingLoader EL;
    public GameManager GM;
    // Start is called before the first frame update
    void Awake()
    {
        introSlider.value = 0;
        //EL.Init();

    }
    private void CheckPlayerSave()
    {
        if (PlayerPrefs.GetInt("FirstPlay") == 0)
        {
            //Players first time to play the game
            EverythingLoader.Instance.firstPlay = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckLoaded();

        introSlider.value = loaded;

        if (loaded >=1f && !gameLoadStarted)
        {
            gameLoadStarted = true;
            GM.Init();
            StartCoroutine(LoadMenuScene());
        }
    }
    public void CheckLoaded()
    {
        //if (GM.isLoaded && !loadZero)
        //{
         //   loadZero = true;
         //   loaded += 0.15f;
        //}
        if (EL.playersLoaded && !loadOne)
        {
            loadOne = true;
            loaded += 0.17f;
        }
        if (EL.enemiesLoaded && !loadTwo)
        {
            loadTwo = true;
            loaded += 0.17f;
        }
        if (EL.weaponsLoaded && !loadThree)
        {
            loadThree = true;
            loaded += 0.17f;
        }
        if (EL.powerUpsLoaded && !loadFour)
        {
            loadFour = true;
            loaded += 0.17f;
        }
        if (EL.hatsLoaded && !loadFive)
        {
            loadFive = true;
            loaded += 0.17f;
        }
        if (EL.skillsLoaded && !loadSix)
        {
            loadSix = true;
            loaded += 0.17f;
        }


    }

    IEnumerator LoadMenuScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainMenu");
        yield return 0;

    }
}
