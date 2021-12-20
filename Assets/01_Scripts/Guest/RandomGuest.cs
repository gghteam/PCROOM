using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RandomGuest : MonoBehaviour
{
    public List<RandomGuestBasic> RandomBasic = new List<RandomGuestBasic>();
    public List<RandomGuestBasic> RandomGt = new List<RandomGuestBasic>();

    [SerializeField]
    private RectTransform oldcheckObject = null;
    [SerializeField]
    private RectTransform maskcheckObject = null;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Image fill;
    public Image GuestImage;

    public List<FinishType> ot = new List<FinishType>();

    private int chpercentage = 35;
    public bool guestck = false;
    public float satisfaction = 0;
    int total = 0;

    private int Adolescent = 5;
    private int Youth = 3;
    private int Middle_Age = 2;

    private bool isTime = false;

    private IEnumerator a;
    private static IEnumerator b; //이거 스테틱으로 왜 썻지?

    private void Update()
    {
        if (isTime)
        {
            if (slider.value > 0.0F)
            {
                slider.value -= Time.deltaTime;
            }
            else
            {
                fill.gameObject.SetActive(false);
            }
        }
    }
    public void StartRandom()
    {
        for (int i = 0; i < RandomBasic.Count; i++)
        {
            total += RandomBasic[i].weight;
        }
    }
    private RandomGuestBasic RandomSt()
    {
        int weight = 0;
        int selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));
        for (int i = 0; i < RandomBasic.Count; i++)
        {
            weight += RandomBasic[i].weight;
            if (selectNum <= weight)
            {
                RandomGuestBasic temp = RandomBasic[i];
                int a = Random.Range(0, 100);
                int b = Random.Range(0, 100);
                if (chpercentage >= a)
                {
                    temp.character = true;
                }
                else
                {
                    temp.character = false;
                }
                if(20>=b)
                {
                    temp.mask = false;
                }
                else
                {
                    temp.mask = true;
                }
                RandomGt.Add(temp);
                return temp;
            }
        }
        return null;
    }

    public RandomGuestBasic RandomChose()
    {
        RandomGuestBasic SetRandomGuestBasic = RandomSt();
        a = GachaUISet(SetRandomGuestBasic);
        StartCoroutine(a);
        return SetRandomGuestBasic;
    }
    public void Stoptimer()
    {
        if (b != null)
            StopCoroutine(b);
    }
    public void StopGachaUI()
    {
        if (a != null)
            StopCoroutine(a);
    }

    public IEnumerator GachaUISet(RandomGuestBasic randomGuestBasic)
    {
        yield return new WaitForSeconds(8f - satisfaction);
        b = randomGuestBasic.character == true ? Timer(8) : Timer(15);
        if (randomGuestBasic.index.Equals(6))
        {
            oldcheckObject.gameObject.SetActive(true);
            oldcheckObject.DOAnchorPos(new Vector2(13f, 19f), 0);
            if (randomGuestBasic.mask == true)
            {
                GuestImage.sprite = randomGuestBasic.guestsprite[1];
                maskcheckObject.gameObject.SetActive(true);
                maskcheckObject.DOAnchorPos(new Vector2(5f, 10f), 0);
            }
            else if (randomGuestBasic.mask == false)
            {
                GuestImage.sprite = randomGuestBasic.guestsprite[0];
                maskcheckObject.gameObject.SetActive(true);
                maskcheckObject.DOAnchorPos(new Vector2(23f, 10f), 0);
            }
        }
        else
        {
            switch (randomGuestBasic.old)
            {
                case 10:
                    oldcheckObject.gameObject.SetActive(true);
                    oldcheckObject.DOAnchorPos(new Vector2(-1.5f, 19f), 0);
                    break;
                case 20:
                    oldcheckObject.gameObject.SetActive(true);
                    oldcheckObject.DOAnchorPos(new Vector2(13f, 19f), 0);
                    break;
                case 30:
                    oldcheckObject.gameObject.SetActive(true);
                    oldcheckObject.DOAnchorPos(new Vector2(27f, 19f), 0);
                    break;
                case 40:
                    oldcheckObject.gameObject.SetActive(true);
                    oldcheckObject.DOAnchorPos(new Vector2(0f, 15f), 0);
                    break;
                case 500:
                    oldcheckObject.gameObject.SetActive(true);
                    oldcheckObject.DOAnchorPos(new Vector2(25f, 15f), 0);
                    break;
            }
            if (randomGuestBasic.mask.Equals(true))
            {
                GuestImage.sprite = randomGuestBasic.guestsprite[1];
                maskcheckObject.gameObject.SetActive(true);
                maskcheckObject.DOAnchorPos(new Vector2(5f, 10f), 0);
                if (randomGuestBasic.character.Equals(true))
                {
                    GuestImage.sprite = randomGuestBasic.guestsprite[3];
                    SoundManager.Instance.SetEffectSoundClip(5);
                }
            }
            else if (randomGuestBasic.mask.Equals(false))
            {
                GuestImage.sprite = randomGuestBasic.guestsprite[0];
                maskcheckObject.gameObject.SetActive(true);
                maskcheckObject.DOAnchorPos(new Vector2(23f, 10f), 0);
                if (randomGuestBasic.character.Equals(true))
                {
                    GuestImage.sprite = randomGuestBasic.guestsprite[2];
                    SoundManager.Instance.SetEffectSoundClip(5);
                }
            }
        GuestImage.transform.DOMoveY(-28f, 0.5f).OnComplete(()=> StartCoroutine(b));
        }
    }

    private IEnumerator Timer(int a)
    {
        slider.maxValue = a;
        slider.value = a;
        isTime = true;
        fill.gameObject.SetActive(true);
        var wait = new WaitForSeconds(1f);
        while (a > 0)
        {
            yield return wait;
            a--;
        }
        GameManager.Instance.uiManager.OnToggleChange(true);
        isTime = false;
        fill.gameObject.SetActive(true);
        GuestImage.transform.DOMoveY(-70f, 0.5f).OnComplete(() => RandomChose());
    }

    public void CertainRandom(int index, int current)
    {
        GameObject obj = null;
        switch (index)
        {
            case 5:
                Adolescent--;
                int a = Random.Range(0, 101);
                if (a <= 20)
                {
                    Adolescent = 5;
                    OrderChose(obj, current);
                }
                else if (Adolescent == 0)
                {
                    Adolescent = 5;
                    OrderChose(obj, current);
                }
                break;
            case 3:
                Youth--;
                int b = Random.Range(0, 101);
                if (b <= 33)
                {
                    Youth = 3;
                    OrderChose(obj, current);
                }
                else if (Youth == 0)
                {
                    Youth = 3;
                    OrderChose(obj, current);
                }
                break;
            case 2:
                Middle_Age--;
                int c = Random.Range(0, 2);
                if (c == 1)
                {
                    Middle_Age = 2;
                    OrderChose(obj, current);
                }
                else if (Middle_Age == 0)
                {
                    Middle_Age = 2;
                    OrderChose(obj, current);
                }
                break;
            case 0:
                OrderChose(obj, current);
                break;
        }
    }
    private void OrderChose(GameObject obj, int current)
    {
        FinishType finishType = Order();
        ot.Add(finishType);
        obj = ObjectPool.Instance.GetObject(PoolObjectType.Order);
        obj.transform.SetParent(GameManager.Instance.uiManager.content.transform);
        obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, 0);
        obj.transform.localScale = new Vector3(0.9265509f, 1.604025f, 2.951774f);
        for (int j = 0; j < 9; j++)
        {
            if (GameManager.Instance.uiManager.r[j] == -1)
            {
                GameManager.Instance.uiManager.r[j] = current;
                break;
            }
        }
        GameManager.Instance.uiManager.Mode(current);
        GameManager.Instance.uiManager.AddLine(current);
    }

    private FinishType Order()
    {
        int a = Random.Range(0, 5);
        switch (a)
        {
            case 0:
                return FinishType.Soup_Ramen;
            case 1:
                return FinishType.Stir_Fried_Ramen;
            case 2:
                return FinishType.HotBar;
            case 3:
                return FinishType.MayoTuna;
            case 4:
                return FinishType.IceTea;
        }
        return FinishType.Olio;
    }

    public void ChangeTime()
    {
        isTime = false;
    }
}
