using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public LevelList levelLists;
    public AudioManager audioManager;
    public Transform camStopRight;
    public WeaponLoader weaponLoader;
    public GameMenuManager menuPanel;
    public UIWidget menuWidget;
    public UIWidget optionsWidget;
    public GameOverPanel gameOverPanel;
    public RevealPowerUp powerUpPanel;
    public UIPanel wheelPanel;
    public WheelController wheelController;

    public UILabel levelLabel;
    public UILabel coinsLabel;
    public UILabel powerUpText;
    public int goldEarned;
    public UISprite powerUpTextSprite;
    public UISlider expSlider;
    public float numberOfLevelUps;
    public float totalEarnedXP;
    public float totalEarnedCoins;

    public UIPanel GamePanel;
    public UIWidget CharacterPanel;
    public UIWidget SceneryPanel;
    public UIWidget bulletBox;
    public UIWidget bgScrollSprite;
    public UISprite blind;
    public Transform playerStart;

    public VirtualController virtualController;
    public Camera uiCamera;
    public PlayerBrain playerBrain;

    public int currentLevel;
    public bool menuOpen;
    public bool levelComplete;
    public bool worldComplete;
    public bool choosingPowerUp;
    public bool choosingWheelPower;
    public bool levelLoading;
    public bool gameOver;

    public Coroutine _currentCoroutine;

    public LevelParameters curLevelPF;
    //public List<LevelParameters> levelList;
    public List<Transform> enemiesList;
    public List<DropPiece> dropPiecesList;
    public List<DropContainer> dropContainerList;


    // Start is called before the first frame update
    void Awake()
    {
        //enemyLoader.Init();
        //weaponLoader.Init();
        //EverythingLoader.Instance.Init();
        levelLoading = true;
        blind.On();
        
        virtualController.Init(uiCamera, Move, Stop);
        playerBrain.Init(this, bulletBox);
        //currentLevel = 0;
        //var level = levelList[currentLevel].MakeInstance(bgScrollSprite.transform.gameObject);
        var level = levelLists.levelList[currentLevel].MakeInstance(bgScrollSprite.transform.gameObject);
        level.Init(this);
        level.transform.position = bgScrollSprite.transform.position;
        curLevelPF = level;
        camStopRight = curLevelPF.camStopRight;
        playerBrain.transform.position = playerStart.position;
        

        expSlider.value = 0f;
        playerBrain.playerStats.CharacterLevel = 1;
        levelLabel.text = ("Player Lvl." + playerBrain.playerStats.CharacterLevel.ToString());
        AddXP(0); //For intial setting
        AddValue(0);//For intial setting

        _currentCoroutine = StartCoroutine(ExecuteAfterTime(1f));
        StartCoroutine(ExecuteAfterTime(1f));
        levelLoading = false;
        //AddXP(450);
    }

    public void OpenMenuPanel()
    {
        if (menuOpen)
        {
            CloseMenuPanel();
        }
        else if (!menuOpen)
        {
            virtualController.canBeUsed = false;
            menuPanel.OnOpen();
            Time.timeScale = 0;
            menuPanel.On();
            menuWidget.On();
            menuOpen = true;
        }
    }
    public void OpenMainMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("MainMenu");
    }
    public void OpenOptionsWidget()
    {
        menuWidget.Off();
        optionsWidget.On();
    }
    public void CloseOptionsWidget()
    {
        optionsWidget.Off();
        menuWidget.On();
    }
    public void CloseMenuPanel()
    {
        virtualController.canBeUsed = true;
        menuPanel.Off();
        menuWidget.Off();
        optionsWidget.Off();
        Time.timeScale = 1;
        menuOpen = false;
    }
    // Update is called once per frame
    private void Move(Vector2 vec)
    {
        if (!levelLoading && !choosingPowerUp)
        {
            playerBrain.SetMove(vec.x, vec.y);
        }
    }
    private void Stop()
    {
        playerBrain.SetMove(0f,0f);
    }
    public void PressScreenToControl()
    {
        //Debug.Log("LM PressToControl");
        if (virtualController.IsPlaying)
        {
            return;
            //virtualController.Stop();
        }
        virtualController.StartControlling();
    }

    public void AddXP(int exp)
    {
        //Debug.Log("exp: " + exp);
        if (!playerBrain.playerStats.CheckMaxlevelReached())
        {
            //Debug.Log("AddXP 1");

            playerBrain.playerStats.ExperiencePoints += exp;
            playerBrain.expEarnedThisLevel += exp;
           // Debug.Log("expEarnedThisLevel: " + playerBrain.expEarnedThisLevel);

            while (playerBrain.playerStats.ExperiencePoints >= playerBrain.playerStats.GetNextLevelExp(playerBrain.playerStats.CharacterLevel))
            {
                playerBrain.playerStats.CharacterLevel++;
                numberOfLevelUps++;
                playerBrain.playerStats.ExperiencePoints = playerBrain.playerStats.ExperiencePoints - playerBrain.playerStats.GetNextLevelExp(playerBrain.playerStats.CharacterLevel);
                //break;
            }

            float slider = playerBrain.playerStats.ExperiencePoints / (float)playerBrain.playerStats.GetNextLevelExp(playerBrain.playerStats.CharacterLevel);

            expSlider.value = +slider;
            levelLabel.text = ("Player Lvl." + playerBrain.playerStats.CharacterLevel.ToString());
        }

        if (playerBrain.playerStats.CheckMaxlevelReached())
        {
            levelLabel.text = ("Max Lvl Reached.");
        }
    }
    public void AddValue(int v)
    {
        playerBrain.playerStats.CurrentMoney += v;
        playerBrain.coinsEarnedThisLevel += v;
        coinsLabel.text = playerBrain.playerStats.CurrentMoney.ToString();
    }
    public void AddEnemy(Transform en)
    {
        enemiesList.Add(en);

    }
    public void RemoveEnemy(EnemyMultiBrain en)
    {
        if (playerBrain.playerStats.BloodThirst)
        {
            Debug.Log("bloodthrist");
            playerBrain.GainHealth(en.hpReward / 2);
        }

        enemiesList.Remove(en.transform);
    }
    public bool EnemiesPresentCheck()
    {
        bool present = false;

        if(enemiesList.Count > 0)
        {
            present = true;
        }

        return present;
    }
    public Transform[] GetEnemyList()
    {
        Transform[] en;
        en = new Transform[enemiesList.Count];
        for (int i = 0; i < enemiesList.Count; i++)
        {
            en[i] = enemiesList[i].transform;
        }
        return en;
    }
    void Update()
    {
        uiCamera.transform.position = new Vector3(playerBrain.transform.position.x, 0f, 0f);

        if (uiCamera.transform.position.x < 0f)
        {
            uiCamera.transform.position = new Vector2(0f, 0f);
        }
        if (uiCamera.transform.localPosition.x > camStopRight.transform.localPosition.x)
        {
            uiCamera.transform.localPosition = new Vector2(camStopRight.transform.localPosition.x, 0f);
        }

        //levelStatusLabel.text = "Level Status: " + levelComplete.ToString();
        //enemyCountLabel.text = "Enemies Count: " + enemiesList.Count.ToString();
        //movedDropsLabel.text = "DP coubnt: " + dropPiecesList.Count.ToString();

        /*
        if (uiCamera.transform.position.x < 0f)
        {
            uiCamera.transform.position = new Vector2(0f, 0f);
        }
        if (uiCamera.transform.localPosition.x > 1446f)
        {
            uiCamera.transform.localPosition = new Vector2(1446f, 0f);
        }
        */
        if (!playerBrain.isAlive && !gameOver)
        {
            gameOver = true;
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = StartCoroutine(GameOver(2f));
            }
        }
        if (levelComplete && numberOfLevelUps > 0 && !choosingPowerUp)
        {
            playerBrain.canMove = false;
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = StartCoroutine(ChoosePowerUp());
            }
        }
        if (enemiesList.Count == 0 && !levelComplete)
        {
            levelComplete = true;
            curLevelPF.isComplete = true;
            curLevelPF.OpenGoal();
            MoveGoldToPlayer();
        }
    }

    public void ChooseWheelSkill()
    {
        playerBrain.canMove = false;

        StartCoroutine(StartWheelSkill());
    }
    IEnumerator StartWheelSkill()
    {
        choosingWheelPower = true;

        int puCount = EverythingLoader.Instance.PowerUpInfos.Count;
        int pu1 = 0;
        int pu11 = 0;
        List<string> spriteString = new List<string>();

        while (pu1 == 0)
        {
            pu11++;
            pu1 = Random.Range(0, EverythingLoader.Instance.PowerUpInfos.Count - 1);
            //Debug.Log("pu1 loop: " + pu1);
            //Debug.Log("pu11: " + pu11);

            for (int i = 0; i < playerBrain.activePowerUps.Count; i++)
            {
                if (pu1 == playerBrain.activePowerUps[i].id)
                {
                    if (EverythingLoader.Instance.PowerUpInfos[pu1].multiPick == 0)
                    {
                        pu1 = 0;
                        break;
                    }
                }
            }
        }
        //Debug.Log(EverythingLoader.Instance.PowerUpInfos[pu1].sprite);
        for (int i = 0; i < 5; i++)
        {
            int ran = Random.Range(1, EverythingLoader.Instance.PowerUpInfos.Count - 1);
            //Debug.Log("ran " + ran);

            spriteString.Add(EverythingLoader.Instance.PowerUpInfos[ran].sprite);
        }
        wheelController.SetImages(EverythingLoader.Instance.PowerUpInfos[pu1].sprite, spriteString[0],
            spriteString[1], spriteString[2], spriteString[3], spriteString[4]);

        yield return new WaitForSeconds(0.1f);
        wheelPanel.On();
        yield return new WaitForSeconds(0.5f);
        wheelController.StartSpin();
        audioManager.PlaySFXClip("spin");

        yield return new WaitUntil(() => wheelController.stopSpin == true);

        yield return new WaitForSeconds(1f);

        playerBrain.AddPowerUp(pu1);
        wheelPanel.Off();

        powerUpText.text = playerBrain.activePowerUps[playerBrain.activatePowersCount - 1].powerDescription;
        powerUpTextSprite.On();
        powerUpText.On();

        choosingWheelPower = false;
        playerBrain.canMove = true;

        yield return new WaitForSeconds(3f);
        powerUpTextSprite.Off();
        powerUpText.Off();
    }
    private void ChooseRandomPowerUps()
    {
        int puCount = EverythingLoader.Instance.PowerUpInfos.Count;
        int pu1 = 0, pu2 = 0, pu3 = 0;
        int pu11 = 0, pu22 = 0, pu33 = 0;

        powerUpPanel.ResetWheels();

        while (pu1 == 0)
        {
            pu11++;
            pu1 = Random.Range(1, powerUpPanel.leftSlot.slotSpritesRandomPosition.Count + 1);
            //Debug.Log("pu1 loop: " + pu1);
            //Debug.Log("pu11: " + pu11);

            for (int i = 0; i < playerBrain.activePowerUps.Count; i++)
            {
                if (pu1 == playerBrain.activePowerUps[i].id)
                {
                    if (EverythingLoader.Instance.PowerUpInfos[pu1].multiPick == 0)
                    {
                        powerUpPanel.RemovePowerUp(pu1);
                        pu1 = 0;
                        break;
                    }
                }
            }
        }
        while (pu2 == 0)
        {
            pu22++;
            pu2 = Random.Range(1, powerUpPanel.centerSlot.slotSpritesRandomPosition.Count + 1);
            //Debug.Log("pu2 loop: " + pu2);
            //Debug.Log("pu22: " + pu22);

            for (int i = 0; i < playerBrain.activePowerUps.Count; i++)
            {
                if (pu2 == playerBrain.activePowerUps[i].id)
                {
                    if (EverythingLoader.Instance.PowerUpInfos[pu2].multiPick == 0)
                    {
                        powerUpPanel.RemovePowerUp(pu2);
                        pu2 = 0;
                        break;
                    }
                }
            }
        }
        while (pu3 == 0)
        {
            pu33++;
            pu3 = Random.Range(1, powerUpPanel.rightSlot.slotSpritesRandomPosition.Count + 1);
            //Debug.Log("pu3 loop: " + pu3);
            //Debug.Log("pu33: " + pu33);

            for (int i = 0; i < playerBrain.activePowerUps.Count; i++)
            {
                if (pu3 == playerBrain.activePowerUps[i].id)
                {
                    if (EverythingLoader.Instance.PowerUpInfos[pu3].multiPick == 0)
                    {
                        powerUpPanel.RemovePowerUp(pu3);

                        pu3 = 0;
                        break;
                    }
                }
            }
        }
        powerUpPanel.SetPowerUp(pu1, pu2, pu3);
    }
    IEnumerator ChoosePowerUp()
    {
        choosingPowerUp = true;

        numberOfLevelUps--;

        yield return new WaitForSeconds(1f);

        powerUpPanel.Init();
        powerUpPanel.On();

        yield return new WaitForSeconds(0.5f);

        ChooseRandomPowerUps();

        yield return new WaitUntil(() => powerUpPanel.powerUpSelected == true);

        yield return new WaitForSeconds(0.5f);

        powerUpPanel.Off();


        yield return new WaitUntil(() => playerBrain.puAdded == true);

        powerUpText.text = playerBrain.activePowerUps[playerBrain.activatePowersCount - 1].powerDescription;
        powerUpTextSprite.On();
        powerUpText.On();


        choosingPowerUp = false;
        playerBrain.canMove = true;

        yield return new WaitForSeconds(3f);

        powerUpTextSprite.Off();
        powerUpText.Off();

    }

    IEnumerator GameOver(float time)
    {
        Debug.Log("Game over panel: ");

        gameOverPanel.ShowStats(true, playerBrain.coinsEarnedThisLevel, playerBrain.expEarnedThisLevel, playerBrain.playerStats.CharacterLevel);

        gameOverPanel.On();
        EverythingLoader.Instance.gameManager.DepositGold(playerBrain.coinsEarnedThisLevel);

        Debug.Log("expEarnedThisLevel: " + playerBrain.expEarnedThisLevel);

        EverythingLoader.Instance.gameManager.DepositExp((playerBrain.expEarnedThisLevel/100)*25);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
        // Code to execute after the delay

    }
    public void Scroll(Vector2 dir, bool setting)
    {
        if (!setting)
        {
            dir.y = 0f;

            if (dir.x > 0)
            {
                dir.x = 0.1f;
            }
            if (dir.x < 0)
            {
                dir.x = -0.1f;
            }
            bgScrollSprite.transform.Translate(-dir);
            Debug.Log(dir);


            if (bgScrollSprite.transform.localPosition.x < -700f)
            {
                bgScrollSprite.transform.localPosition = new Vector2(-720f, 0f);
            }
            if (bgScrollSprite.transform.localPosition.x > 700f)
            {
                bgScrollSprite.transform.localPosition = new Vector2(720f, 0f);
            }
        }
        else if (setting)
        {
            bgScrollSprite.transform.localPosition = dir;

        }

    }
    public void MoveGoldToPlayer()
    {
        int i = dropPiecesList.Count - 1;
        while (dropPiecesList.Count > 0)
        {
            dropPiecesList[i].SetTarget(playerBrain.transform, RemoveDropPiece);

            dropPiecesList[i].AllowMove();
            StartCoroutine(PlaySFX());

            if (dropPiecesList[i].isGold)
            {
                AddValue(dropPiecesList[i].value);
                Debug.Log("Add gold ");


            }
            if (dropPiecesList[i].isXp)
            {
                int extraXP = 0;
				int boostedXP = (int)dropPiecesList[i].value * currentLevel;

				if (playerBrain.playerStats.Smart)
                {
                    extraXP = (int)(boostedXP * 0.25f);
                    //Debug.Log("Extra XP: " + extraXP);
                    playerBrain.extraXPRef = extraXP;

                }
                Debug.Log("extraXP ");


                AddXP(boostedXP + extraXP);
            }
            if (dropPiecesList[i].isHealth)
            {
                playerBrain.GainHealth(dropPiecesList[i].value);
                Debug.Log("GainHealth ");

            }

            dropPiecesList.Remove(dropPiecesList[i]);
            i--;
        }
    }
    public void RemoveDropPiece(DropPiece dP)
    {
        //audioManager.PlaySFXClip("getItem");
        dropPiecesList.Remove(dP);
    }
    IEnumerator PlaySFX()
    {
        float ran = Random.Range(0f, 0.4f);
        yield return new WaitForSeconds(ran);
        audioManager.PlaySFXClip("getItem");

    }
    public void GoToNextLevel()
    {
        if (curLevelPF.isComplete == true)
        {
            //if (goldList.Count > 0)
                //MoveGoldToPlayer();

            levelLoading = true;

            currentLevel++;




            LoadNextLevel();


        }


    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        blind.Off();
    }
    IEnumerator WorldComplete()
    {
        gameOverPanel.ShowStats(false, playerBrain.coinsEarnedThisLevel, playerBrain.expEarnedThisLevel, playerBrain.playerStats.CharacterLevel);
        gameOverPanel.On();
        EverythingLoader.Instance.gameManager.DepositGold(playerBrain.coinsEarnedThisLevel);

        Debug.Log("expEarnedThisLevel: " + playerBrain.expEarnedThisLevel);

        EverythingLoader.Instance.gameManager.DepositExp((playerBrain.expEarnedThisLevel / 100) * 25);
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("MainMenu");

    }
    public void LoadNextLevel()
    {
        levelComplete = false;
        
        if (currentLevel >= levelLists.levelList.Count)
        {
            //currentLevel = 0;
            worldComplete = true;
        }

        while (dropContainerList.Count > 0)
        {
            dropContainerList[0].Dissapear();
            dropContainerList.Remove(dropContainerList[0]);
        }
        while (enemiesList.Count > 0)
        {
            enemiesList.Remove(enemiesList[0]);
        }
        if (!worldComplete)
        {
            blind.On();

            StartCoroutine(ExecuteAfterTime(1f));

            curLevelPF.Destruct();
            NGUITools.Destroy(curLevelPF.gameObject);

            var level = levelLists.levelList[currentLevel].MakeInstance(bgScrollSprite.transform.gameObject);
            level.Init(this);
            level.transform.position = bgScrollSprite.transform.position;
            curLevelPF = level;
            camStopRight = curLevelPF.camStopRight;

            Scroll(new Vector2(720f, 0f), true);

            playerBrain.Reset();
            playerBrain.transform.position = playerStart.position;
            uiCamera.transform.position = playerBrain.transform.position;

            levelLoading = false;
        }

        if (worldComplete)
        {
            StartCoroutine(WorldComplete());
        }

    }

}
