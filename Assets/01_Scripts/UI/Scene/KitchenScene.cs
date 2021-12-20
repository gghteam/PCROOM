using UnityEngine;
using UnityEngine.UI;

public class KitchenScene : UIScreen
{
    [SerializeField]
    Button startButton;
    [SerializeField]
    Button inbutton;
    private void Awake()
    {
        inbutton.onClick.AddListener(() => GameManager.Instance.UpdateState(GameState.Kitchen));
        startButton.onClick.AddListener(() => GameManager.Instance.UpdateState(GameState.Running));
    }
    public override void UpdateScreenStatus(bool open)
    {
        base.UpdateScreenStatus(open);
    }
}
