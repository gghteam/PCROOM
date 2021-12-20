using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private Image spriteRenderer;
    [SerializeField]
    private Sprite[] panel;
    private int Maxcount = 9;
    private int count = 0;

    public void Next()
    {
            if (count == Maxcount)
            {
                GameManager.Instance.UpdateState(GameState.Running);
                return;
            }
            spriteRenderer.sprite = panel[count];
            count++;
    }
}
