using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpReporter : MonoBehaviour
{
    public PlayerBrain p;
    public UILabel attackIncrease;
    public UILabel extraAttIncease;
    public UILabel attackSpeed;
    public UILabel attackSpeedIncease;
    public UILabel critPerc;
    public UILabel critDmg;
    public UILabel hpIncrease;
    public UILabel healPerc;
    public UILabel rageActive;
    public UILabel bloodThirstActive;
    public UILabel invincibleACtive;
    public UILabel smartActive;
    public bool hide;

    public GameObject reporter;

    // Update is called once per frame
    void Update()
    {
        attackIncrease.text = ("Regular Attack: " + p.playerStats.AttackPower.ToString());

        extraAttIncease.text = ("Extra Attack: " + p.extraDmgRef.ToString());

        attackSpeed.text = ("Attack Speed: " + (2f - p.playerStats.AttackSpeed).ToString());

        attackSpeedIncease.text = ("AtkSpdIncrease: " +(1f - p.extraAtkSpeed).ToString());

        critPerc.text = ("Crit % Chance: " + p.playerStats.CriticalHitRate.ToString());

        critDmg.text = ("Crit Dmg %: " + p.playerStats.CriticalHitDamagePercentage.ToString());

        hpIncrease.text = ("Extra HP: " + p.extraHPRef.ToString());

        healPerc.text = ("heal &: " + p.playerStats.HealPercentage.ToString());

        bloodThirstActive.text = ("BloodThirst Unlocked: " + p.playerStats.BloodThirst.ToString());

        rageActive.text = ("Rage Unlocked: " + p.playerStats.Rage.ToString());

        invincibleACtive.text = ("Invincible Unlocked: " + p.playerStats.Invincible.ToString());

        smartActive.text = ("Smart Unlocked: " + p.playerStats.Smart.ToString());
    }

    public void ToggleShow()
    {
        if (hide)
        {
            hide = false;
            reporter.Off();
        }
        else
        {
            hide = true;
            reporter.On();
        }
    }
}
