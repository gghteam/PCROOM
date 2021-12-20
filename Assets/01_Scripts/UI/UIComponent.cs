using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponent : Component
{
    List<UIScreen> screens = new List<UIScreen>();

    public UIComponent()
    {
        this.screens.Add(GameObject.Find("StandbyScene").GetComponent<UIScreen>());
        this.screens.Add(GameObject.Find("RunningScene").GetComponent<UIScreen>());
        this.screens.Add(GameObject.Find("KitchenScene").GetComponent<UIScreen>());
        this.screens.Add(GameObject.Find("CalculateScene").GetComponent<UIScreen>());
        this.screens.Add(GameObject.Find("TutorialScene").GetComponent<UIScreen>());
        this.screens.Add(GameObject.Find("EndingScene").GetComponent<UIScreen>());
    }

    public void UpdateState(GameState state)
    {
        switch(state)
        {
            case GameState.Init:
                InitAllScreens();

                CloseAllScreens();
                break;
            default:
                ActiveSceen(state);
                break;
        }
    }

    void ActiveSceen(GameState type)
    {
        CloseAllScreens();

        GetScreen(type).UpdateScreenStatus(true);
    }

    void CloseAllScreens()
    {
        foreach(var screen in screens)
        {
            screen.UpdateScreenStatus(false);
        }
    }

    void InitAllScreens()
    {
        foreach (var screen in screens)
        {
            screen.Init();
        }
    }

    UIScreen GetScreen(GameState screenstate)
    {
        return screens.Find(el => el.uistate == screenstate);
    }
}
