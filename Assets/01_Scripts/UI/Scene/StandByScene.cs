using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandByScene : UIScreen
{
    [SerializeField]
    Button RunningButton;
    [SerializeField]
    GameObject a;

    private void Start()
    {
        RunningButton.onClick.AddListener(() => GameManager.Instance.UpdateState(GameState.Tutorial));
        a.SetActive(true);
    }
    public override void UpdateScreenStatus(bool open)
    {
        base.UpdateScreenStatus(open);
    }
}
