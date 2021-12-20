using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateScene : UIScreen
{
    [SerializeField]
    Button ReturnRunningButton;
    private void Start()
    {
        ReturnRunningButton.onClick.AddListener(() => GameManager.Instance.UpdateState(GameState.Running));
    }
    public override void UpdateScreenStatus(bool open)
    {
        base.UpdateScreenStatus(open);
    }
}
