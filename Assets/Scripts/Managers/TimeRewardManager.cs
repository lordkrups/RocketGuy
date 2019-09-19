using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewardManager : MonoBehaviour
{
    public UILabel playTimeLabel;

    public UILabel coinRewardLabel;
    public UILabel coinAmountLabel;

    public UILabel medalRewardLabel;
    public UILabel medalAmountLabel;

    public void SetTimeReward(int coinReward, int medalReward, int hour, int min, int coins, int medals)
    {
        coinRewardLabel.text = "+" + "[FFE63A]" + (EverythingLoader.Instance.gameManager.totalTimeRewardUnlocks * 6).ToString() + "[-]/h";
        medalRewardLabel.text = "+" + "[FFE63A]" + (EverythingLoader.Instance.gameManager.totalTimeRewardUnlocks * 12).ToString() + "[-]/h";

        playTimeLabel.text = "total play time: " + hour.ToString() + "H:" + min.ToString() + "M";
        coinAmountLabel.text = coins.ToString();
        medalAmountLabel.text = medals.ToString();

        this.On();
    }

    public void OpenPanel()
    {
        this.On();
    }
    public void ClosePanel()
    {
        this.Off();
    }
}
