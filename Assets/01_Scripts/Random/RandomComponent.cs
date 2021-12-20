using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RandomComponent : Component
{
    public void UpdateState(GameState state)
    {
        switch (state)
        {
            case GameState.Init:
                GameManager.Instance.randomguest.StartRandom();
                break;
            case GameState.StandBy:
                SoundManager.Instance.SetBackgroundSoundclip(0);
                GameManager.Instance.uiManager.GuestReset();
                break;
            case GameState.Tutorial:
                if (PlayerPrefs.GetInt("TUTORIAL", 0) == 0)
                {
                    PlayerPrefs.SetInt("TUTORIAL", 1);
                }
                else
                {
                    GameManager.Instance.UpdateState(GameState.Running);
                }
                break;
            case GameState.Running:
                SoundManager.Instance.SetBackgroundSoundclip(0);
                GameManager.Instance.randomguest.RandomChose();
                if (PlayerPrefs.GetInt("DAY", 0) == 0)
                {
                    GameManager.Instance.SetTime();
                    PlayerPrefs.SetInt("DAY", 1);
                }
                break;
            case GameState.Kitchen:
                GameManager.Instance.uiManager.GuestReset();
                break;
            case GameState.Calculate:
                GameManager.Instance.uiManager.ResetCalculate();
                SoundManager.Instance.SetBackgroundSoundclip(1);
                GameManager.Instance.uiManager.AllReset();
                GameManager.Instance.uiManager.GuestReset();
                GameManager.Instance.uiManager.SetCalculate();
                GameManager.Instance.randomguest.ChangeTime();
                if (GameManager.Instance.randomguest.satisfaction - 0.2f <= 0)
                {
                    GameManager.Instance.randomguest.satisfaction -= 0.2f;
                }
                break;
            case GameState.Ending:
                GameManager.Instance.EveryReset();
                GameManager.Instance.uiManager.AllReset();
                GameManager.Instance.uiManager.SetCalculate();
                GameManager.Instance.randomguest.ChangeTime();
                break;
        }
    }
}
