using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public UILabel mainText;
    public UILabel coinText;
    public UILabel expText;
    public UILabel currentLevel;
    public UISprite incLvlSprite;


    // Start is called before the first frame update
    public void ShowStats(bool gameOver, int coins, int exp, int cLvl, bool nLvl = false)
    {
        if (!gameOver)
        {
            mainText.text = "World Complete!";
        }
        if (gameOver)
        {
            mainText.text = "Game Over!";
        }
        coinText.text = "Coins earned: " + coins.ToString();
        expText.text = "Exp earned: " + exp.ToString();
        currentLevel.text = cLvl.ToString();
        if (nLvl)
        {
            incLvlSprite.On();
        }
    }
}
