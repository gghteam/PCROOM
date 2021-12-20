using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarbageControllrer : MonoBehaviour
{
    public Vector3[] location { get; private set; } = new Vector3[4] { new Vector3(-764, -324, 0), new Vector3(-319, -384, 0), new Vector3(31, -326, 0), new Vector3(312, -465, 0) };
    [SerializeField]
    private GameObject[] garbage;
    [SerializeField]
    private GameObject food;
    [SerializeField]
    private Sprite[] foodSprite;
    [SerializeField]
    private Sprite[] garbageFoodSprite;
    public bool[] mode { get; private set; } = new bool[9]; 
    public int[,] garbageIndex { get; private set; } = new int[9, 3];
    private List<int> tempIndex = new List<int>() { 10, 10, 10};
    private int plus = 0;

    public void SpawnGarbe(int Count,int current)
    {
        for(int i =0; i < Count; i++)
        {
            int a = Random.Range(0, 4); 
            for(int j = 0; j < tempIndex.Count; j++)
            {
                if(tempIndex[j] == a || tempIndex[2] == a)
                {
                    while(tempIndex[j] == a || tempIndex[2] == a) { 
                    a = Random.Range(0, 4);
                        }
                }
            }
            tempIndex[i] = a;
            garbageIndex[current,i] = tempIndex[i]; 
        }
          for(int i = 0; i < 3; i++)
        {
            tempIndex[i] = 10;
        }
        

    }

    public void SpawnFood(int cuurent)
    {
        int b = Random.Range(0, 4);
        int c = Random.Range(0, 2);
        if (tempIndex[0] == b || tempIndex[1] == b)
        {
            while (tempIndex[0] == b || tempIndex[1] == b)
            {
                b = Random.Range(0, 4);
            }
        }
        tempIndex[2] = b;
        garbageIndex[cuurent, 2] = tempIndex[2];
        if (c == 0)
            mode[cuurent] = false;
        else if (c == 1)
            mode[cuurent] = true;
        GameManager.Instance.uiManager.ChangeFood(cuurent);
    }

    public void SetGarbage(int count,int current)
    {
        for(int i = 0; i < count; i++)
        {
            garbage[i].GetComponent<RectTransform>().anchoredPosition = location[garbageIndex[current,i]];
            garbage[i].GetComponent<Food>().ChangeNum(plus);
            plus++;
            garbage[i].SetActive(true);
        }
        food.GetComponent<Food>().ChangeMode(mode[current]);
        if (GameManager.Instance.uiManager.checkFood[current])
        {
            food.GetComponent<RectTransform>().anchoredPosition = location[garbageIndex[current, 2]];
            if (GameManager.Instance.uiManager.isFood[current] == "SR")
            {
                if(mode[current])
                {
                    food.GetComponent<Image>().sprite = garbageFoodSprite[0];
                    food.GetComponent<Food>().Saveindex(0);
                }
                else
                food.GetComponent<Image>().sprite = foodSprite[0];
            }
            else if (GameManager.Instance.uiManager.isFood[current] == "SFR")
            {
                if (mode[current])
                {
                    food.GetComponent<Image>().sprite = garbageFoodSprite[1];
                    food.GetComponent<Food>().Saveindex(1);
                }
                else
                    food.GetComponent<Image>().sprite = foodSprite[1];
            }
            else if (GameManager.Instance.uiManager.isFood[current] == "H")
            {
                if (mode[current])
                {
                    food.GetComponent<Image>().sprite = garbageFoodSprite[2];
                    food.GetComponent<Food>().Saveindex(2);
                }
                else
                    food.GetComponent<Image>().sprite = foodSprite[2];
            }
            else if (GameManager.Instance.uiManager.isFood[current] == "M")
            {
                if (mode[current])
                {
                    food.GetComponent<Image>().sprite = garbageFoodSprite[3];
                    food.GetComponent<Food>().Saveindex(3);
                }
                else
                    food.GetComponent<Image>().sprite = foodSprite[3];
            }
            else if(GameManager.Instance.uiManager.isFood[current] == "I")
            {
                if (mode[current])
                {
                    food.GetComponent<Image>().sprite = garbageFoodSprite[4];
                    food.GetComponent<Food>().Saveindex(4);
                }
                else
                    food.GetComponent<Image>().sprite = foodSprite[4];
            }
            food.SetActive(true);
        }
        plus = 0;
    }

    public void Show()
    {
        for(int i = 0; i < 2; i++)
        {
            if(garbage[i].activeSelf)
            {
                garbage[i].SetActive(false);
            }
        }
        if (food.activeSelf)
        {
            food.SetActive(false);
        }
    }

    public void CancelGarbage(int current, int index)
    {
        garbageIndex[current, index] = 0;
        garbageIndex[current, 0] = garbageIndex[current, 1];
    }

    public void CancelFood(int current)
    {
        garbageIndex[current, 2] = 0;
        GameManager.Instance.uiManager.ChangeFood(current);
        if(GameManager.Instance.uiManager.isSit[current])
        {
            int c = Random.Range(0, 101);
            if (c <= 70)
            {
                GameManager.Instance.AddFine();
            }
        }
    }

    public void ChangeSprite(int index)
    {
        food.GetComponent<Image>().sprite = foodSprite[index];
    }

    public void GarbageReset()
    {
        for (int i = 0; i < 9; i++)
        {
            mode[i] = false;
            for (int j = 0; j < 3; j++)
            {
                garbageIndex[i, j] = 0;
            }
        }
        tempIndex = new List<int>() { 10, 10, 10 };
    }
}
