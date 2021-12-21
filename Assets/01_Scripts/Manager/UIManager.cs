using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;



public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject informobject = null;
    [SerializeField]
    private RectTransform informPanel = null;
    [SerializeField]
    private GameObject allSeat = null;
    [SerializeField]
    private Text currentSeat = null;
    [SerializeField]
    private Text goldText = null;
    [SerializeField]
    private GameObject MicroMove;
    [SerializeField]
    private GameObject Pot1;
    [SerializeField]
    private GameObject Pot2;
    [SerializeField]
    private GameObject Pot3;
    [SerializeField]
    private GameObject Pot4;
    [SerializeField]
    private GameObject CuttingBoard;
    [SerializeField]
    private GameObject Macine;
    [SerializeField]
    private Text revenuecounttext;
    [SerializeField]
    private Text ingredientcounttext;
    [SerializeField]
    private Text finecounttext;
    [SerializeField]
    private Text rentexpensecounttext;
    [SerializeField]
    private Text revenuecalculatecounttext;
    [SerializeField]
    private GameObject Option;
    [SerializeField]
    private Image finishimage;
    [SerializeField]
    private RectTransform finishimagerect;
    [SerializeField]
    private GameObject recipeobj;
    [SerializeField]
    private Image cookimage;
    [SerializeField]
    private GameObject back;
    [SerializeField]
    private GameObject back2;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private RectTransform ktchenrect;
    [SerializeField]
    private RectTransform IWButton;
    [SerializeField]
    private Image iwimage;

    public Image calcanvas;
    public Image bksimage;
    public Image efimage;

    public List<Sprite> soundsprites = new List<Sprite>();

    public List<Sprite> cooksprites = new List<Sprite>();

    public GameObject content;

    [SerializeField]
    private GameObject pCRoomSeat = null;
    [SerializeField]
    private GameObject pCSeat = null;

    public List<Sprite> finishsprites = new List<Sprite>();
    public GameObject basicparent;

    public bool[] isSit = new bool[9];
    public bool[] isEnd { get; private set; } = new bool[9];
    public bool[] checkFood { get; private set; } = new bool[9];

    private bool[] isOrder = new bool[9];

    private bool[] isMode = new bool[9];

    private int[] line = new int[9];
    private int maxLine = -1;
    public Queue<FinishType> fstype = new Queue<FinishType>();
    public Queue<FinishType> finishTypes = new Queue<FinishType>();

    public int[] isclean { get; private set; } = new int[9];

    public int currentsit { get; private set; } = 0;

    [SerializeField]
    private GameObject outButton = null;

    [SerializeField]
    private Text timeText = null;

    private float aniSpeed = 1;

    public List<Vector3> anchored /*{ get; private set; }*/ = new List<Vector3>();

    public int[] r = new int[9];

    public string[] isFood { get; private set; } = new string[9];

    public bool[] isPlay { get; private set; } = new bool[9];

    private bool isplay = false;
    private int j = 0;
    private int bgcount = 0;
    private int efcount = 0;
    private int iwcount = 0;
    private void Awake()
    {
        for (int i = 0; i < 9; i++)
        {
            isSit[i] = false;
            isEnd[i] = false;
            isOrder[i] = false;
            isMode[i] = false;
            isclean[i] = 0;
            line[i] = -1;
            r[i] = -1;
            isPlay[i] = false;
        }
    }

    private void Start()
    {
        SetSeat();
        ChangeUI();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                back.SetActive(true);
            }
        }
    }



    public void ChangeFood(int cuurent)
    {
        if (!checkFood[cuurent])
            checkFood[cuurent] = true;
        else
            checkFood[cuurent] = false;
    }
    public void OnClickGuestButton()
    {
        informobject.SetActive(true);
        OnToggleChange(false);
    }

    public void Orderck()
    {
        if (content.transform.childCount > 0)
        {
            cookimage.sprite = cooksprites[1];
        }
        else
        {
            cookimage.sprite = cooksprites[0];
        }
    }

    //유저 받는 것을 Yes클릭시
    public void OkButton()
    {
        if (GameManager.Instance.sit >= 9)
        {
            return;
        }
        else
        {
            for (int i = 0; i < isclean.Length; i++)
            {
                if (isclean[i] <= 0 && isSit[i] == false)
                {
                    SoundManager.Instance.SetEffectSoundClip(2);
                    int j = GameManager.Instance.AddItem();
                    OnToggleChange(true);
                    GameManager.Instance.randomguest.GuestImage.transform.DOMoveY(-70f, 0.5f).OnComplete(() =>
                    {
                        GameManager.Instance.randomguest.Stoptimer();
                        RandomGuestBasic a = GameManager.Instance.randomguest.RandomGt[GameManager.Instance.randomguest.RandomGt.Count - 1];
                        if (a.mask == false)
                        {
                            Debug.Log("Tlq");
                            GameManager.Instance.AddFine();
                        }
                        if (a.old < 20)
                        {
                            GameManager.Instance.randomguest.CertainRandom(5, j);
                        }
                        else if (a.old >= 20 && a.old < 30)
                        {
                            if(a.index == 6)
                            {
                                GameManager.Instance.randomguest.CertainRandom(0, j);
                                return;
                            }
                            GameManager.Instance.randomguest.CertainRandom(3, j);
                        }
                        else if (a.old <= 30 && a.old < 40)
                        {
                            GameManager.Instance.randomguest.CertainRandom(2, j);
                        }
                        else if (a.old <= 40 && a.old < 50)
                        {
                            GameManager.Instance.randomguest.CertainRandom(0, j);
                        }
                        else
                        {
                            GameManager.Instance.randomguest.CertainRandom(0, j);
                        }
                        GameManager.Instance.randomguest.RandomChose();
                        GameManager.Instance.randomguest.ChangeTime();
                        Orderck();
                    });
                    break;
                }
            }
        }
    }

    //유저 받는 것을 No클릭시
    public void NoButton()
    {
        //T 손님 무시
        SoundManager.Instance.SetEffectSoundClip(0);
        GameManager.Instance.randomguest.GuestImage.transform.DOMoveY(-70f, 0.5f).OnComplete(() =>
        {
            int a = GameManager.Instance.randomguest.RandomGt.Count;
            GameManager.Instance.randomguest.RandomGt.RemoveAt(a - 1);
            GameManager.Instance.randomguest.RandomChose();
        });
        OnToggleChange(true);
        GameManager.Instance.randomguest.Stoptimer();
        GameManager.Instance.randomguest.ChangeTime();
    }

    //유저 정보 판넬 위치를 바꿈
    public void GuestReset()
    {
        OnToggleChange(true);
        GameManager.Instance.randomguest.StopGachaUI();
        GameManager.Instance.randomguest.GuestImage.transform.DOMoveY(-70f, 0f);
        GameManager.Instance.randomguest.Stoptimer();
    }
    public void OnToggleChange(bool isOn)
    {
        informPanel.DOAnchorPosY(isOn ? 1400f : 0f, 0.2f).SetEase(Ease.InCirc);
    }

    //현재 선택한 시트에 번호를 알려줌
    public void GetSeatNumber()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;


        for (int i = 0; i < GameManager.Instance.seatCount; i++)
        {
            if (allSeat.transform.GetChild(i).gameObject == clickObject.gameObject)
            {
                currentsit = i;
                currentSeat.text = string.Format("{0}", currentsit + 1);
                ShowOut();
                break;
            }
        }
    }

    //좌석을 키거나 꺼요.
    public void GopCRoomSeat()
    {
        if (!pCRoomSeat.activeSelf)
        {
            SetSeat();
            pCRoomSeat.SetActive(true);
        }
        else
            pCRoomSeat.SetActive(false);
    }

    //내보낼수 있는 좌석만 버튼이 나타나게 합니다
    private void ShowOut()
    {
        if (isSit[currentsit])
        {
            outButton.SetActive(true);
        }
        else
        {
            outButton.SetActive(false);
        }
    }

    //Seat에 앉았는지 색으로 변경합니다.
    private void SetSeat()
    {

        for (int i = 0; i < GameManager.Instance.seatCount; i++)
        {
            if (isSit[i])
            {
                allSeat.transform.GetChild(i).GetComponent<Image>().color = Color.blue;
                allSeat.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                if (isclean[i] != 0)
                {
                    allSeat.transform.GetChild(i).GetComponent<Image>().color = Color.green;
                    continue;
                }
                allSeat.transform.GetChild(i).GetComponent<Image>().color = new Vector4(255 / 255, 255 / 255, 255 / 255, 255 / 255);
            }
        }

        ShowOut();
    }
    //pc번호좌석을 종료하고 해당 pc좌석을 엽니다(청소씬으로 이동)
    public void GoCurrentPcSeat()
    {
        pCRoomSeat.SetActive(false);
        if (checkFood[currentsit])
            GameManager.Instance.garbageControllrer.SetGarbage(isclean[currentsit] - 1, currentsit);
        else
            GameManager.Instance.garbageControllrer.SetGarbage(isclean[currentsit], currentsit);
        pCSeat.SetActive(true);
    }

    //Pc좌석의 창을 닫습니다.
    public void CancelPcSeat()
    {
        pCSeat.SetActive(false);
        GameManager.Instance.garbageControllrer.Show();
    }

    //손님 시간을 임의로 계산합니다
    public IEnumerator Timer(int index,int gold)
    {
        int random = Random.Range(0, 100);
        int time = 0;
        if (random >= 0 && random <= 24)
        {
            time = 30;
            GameManager.Instance.MulDayGold(gold, 1);
        }
        else if (random >= 25 && random <= 59)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                time = 60;
                aniSpeed /= 2;
                GameManager.Instance.MulDayGold(gold, 2);
            }
            else if (rand == 1)
            {
                time = 90;
                aniSpeed /= 3;
                GameManager.Instance.MulDayGold(gold, 3);
            }

        }
        else if (random >= 60 && random <= 79)
        {
            time = 120;
            aniSpeed /= 4;
            GameManager.Instance.MulDayGold(gold, 4);
        }
        else if (random >= 80 && random <= 94)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                time = 180;
                aniSpeed /= 5;
                GameManager.Instance.MulDayGold(gold, 5);
            }
            else if (rand == 1)
            {
                time = 240;
                aniSpeed /= 6;
                GameManager.Instance.MulDayGold(gold, 6);
            }
        }
        else if (random >= 95 && random <= 99)
        {
            time = 300;
            aniSpeed /= 7;
            GameManager.Instance.MulDayGold(gold, 7);
        }
        allSeat.transform.GetChild(index).GetChild(0).GetComponent<Text>().color = Color.black;

        while (time >= 0)
        {
            if (!isSit[index])
            {
                break;
            }
            allSeat.transform.GetChild(index).GetChild(0).GetComponent<Text>().text = string.Format("{0}", time);
            //Debug.Log(time);
            time--;
            yield return new WaitForSeconds(1f);
        }
        if (time <= 0)
        {
            allSeat.transform.GetChild(index).GetChild(0).GetComponent<Text>().color = Color.red;
            isEnd[index] = true;
        }
    }

    //해당 좌석을 쫒아낸다.
    public void Kick()
    {
        //TODO 판넬 생각?
        isSit[currentsit] = false;
        allSeat.transform.GetChild(currentsit).GetChild(0).GetComponent<Text>().gameObject.SetActive(false);
        if (!isEnd[currentsit])
        {
            int c = Random.Range(0, 101);
            if (c <= 70)
            {
                GameManager.Instance.AddFine();
            }
        }
        if (isOrder[currentsit] && !isMode[currentsit])
        {
            GameManager.Instance.randomguest.ot.RemoveAt(line[currentsit]);
            r[line[currentsit]] = -1;
            for (int i = 0; i < 8; i++)
            {
                if (r[i] == -1 && r[i + 1] != -1)
                {
                    r[i] = r[i + 1];
                    r[i + 1] = -1;
                }
            }
            ObjectPool.Instance.ReturnObject(PoolObjectType.Order, content.transform.GetChild(line[currentsit]).gameObject);
        }
        if (line[currentsit] != maxLine)
        {
            for (int i = 0; i < 9; i++)
            {
                if (line[i] > 0)
                {
                    line[i]--;
                }
            }
        }
        line[currentsit] = -1;
        if (maxLine > -1)
        {
            maxLine--;
        }
        isEnd[currentsit] = false;
        isOrder[currentsit] = false;
        isMode[currentsit] = false;
        GameManager.Instance.MinusItem();
        //TODO 더러운지 안더러운지 판단 및 확률 필요
        int num = Random.Range(0, 2);
        if (num == 0 && isSit[currentsit]) //깨끗하면
            allSeat.transform.GetChild(currentsit).GetComponent<Image>().color = new Vector4(255 / 255, 255 / 255, 255 / 255, 255 / 255);
        else
        {
            int count = Random.Range(1, 3);
            isclean[currentsit] += count;
            if (checkFood[currentsit])
                GameManager.Instance.garbageControllrer.SpawnGarbe(isclean[currentsit] - 1, currentsit);
            else
                GameManager.Instance.garbageControllrer.SpawnGarbe(isclean[currentsit], currentsit);
            allSeat.transform.GetChild(currentsit).GetComponent<Image>().color = Color.green;
        }
        ShowOut();
        Orderck();
        SetSeat();
    }

    //시간Ui을 바꿉니다.
    public void ChangeTime()
    {
        //오전 9시 ~ 오전 12시
        if (GameManager.Instance.time < 13)
        {
            timeText.text = string.Format("Day {0}\n{1,0:D2}:00 AM", GameManager.Instance.CurrentUser.day + 1, GameManager.Instance.time);
        }
        //오전 1시 ~ 오전 2시
        else if (GameManager.Instance.time > 24)
        {
            timeText.text = string.Format("Day {0}\n{1,0:D2}:00 AM", GameManager.Instance.CurrentUser.day + 1, GameManager.Instance.time - 24);
        }
        //오후 1시 ~ 오후 12시
        else
            timeText.text = string.Format("Day {0}\n{1,0:D2}:00 PM", GameManager.Instance.CurrentUser.day + 1, GameManager.Instance.time - 12);
    }

    public void ChangeUI()
    {
        goldText.text = string.Format("{0}₩", GameManager.Instance.CurrentUser.gold + GameManager.Instance.GetDayGold());
    }

    public void OnclickPot1()
    {
        rpck2(0, GameManager.Instance.recipe.Soup_Ramen, GameManager.Instance.recipe.Stir_Fried_Ramen, FinishType.Soup_Ramen, FinishType.Stir_Fried_Ramen, Pot1);
    }
    public void OnclicPot2()
    {
        rpck2(1, GameManager.Instance.recipe.Soup_Ramen, GameManager.Instance.recipe.Stir_Fried_Ramen, FinishType.Soup_Ramen, FinishType.Stir_Fried_Ramen, Pot2);
    }
    public void OnclicPot3()
    {
        rpck2(2, GameManager.Instance.recipe.Soup_Ramen, GameManager.Instance.recipe.Stir_Fried_Ramen, FinishType.Soup_Ramen, FinishType.Stir_Fried_Ramen, Pot3);
    }
    public void OnclicPot4()
    {
        rpck2(3, GameManager.Instance.recipe.Soup_Ramen, GameManager.Instance.recipe.Stir_Fried_Ramen, FinishType.Soup_Ramen, FinishType.Stir_Fried_Ramen, Pot4);
    }
    public void OnclickMicroMove()
    {
        rpck(4, GameManager.Instance.recipe.HotBar, FinishType.HotBar, MicroMove);
    }

    public void OnclickCuttingBoard()
    {
        rpck(5, GameManager.Instance.recipe.MayoTuna, FinishType.MayoTuna, CuttingBoard);
    }
    public void OnclickMacine()
    {
        rpck(6, GameManager.Instance.recipe.IceTea, FinishType.IceTea, Macine);
    }

    public void rpck(int index, PoolObjectType[] finishTypes, FinishType finishType, GameObject obj)
    {
        FinishType c = FinishType.Olio;
        if (GameManager.Instance.recipe.Recipeck(finishTypes, index) == finishType)
        {
            SoundManager.Instance.SetEffectSoundClip(1);
            c = finishType;
            fstype.Enqueue(c);
            GameManager.Instance.recipe.currentingredientstype[index].Clear();
            Debug.Log("?");
            int j = obj.transform.childCount;
            Debug.Log(j);
            for (int i = 0; i < j; i++)
            {
                Debug.Log("??");
                ObjectPool.Instance.ReturnObject(obj.transform.GetChild(0).GetComponent<Ingredients>().Gettype(), obj.transform.GetChild(0).gameObject);
            }
        }
        else
        {
            GameManager.Instance.recipe.currentingredientstype[index].Clear();
            int j = obj.transform.childCount;
            for (int i = 0; i < j; i++)
            {
                ObjectPool.Instance.ReturnObject(obj.transform.GetChild(0).GetComponent<Ingredients>().Gettype(), obj.transform.GetChild(0).gameObject);
            }
        }
    }

    public void rpck2(int index, PoolObjectType[] finishTypes, PoolObjectType[] finishTypes2, FinishType finishType, FinishType finishType2, GameObject obj)
    {
        FinishType c = FinishType.Olio;

        if (GameManager.Instance.recipe.Recipeck(finishTypes, index) == finishType)
        {
            Debug.Log("물들어있는 라면");
            SoundManager.Instance.SetEffectSoundClip(1);
            c = finishType;
            GameManager.Instance.recipe.currentingredientstype[index].Clear();

            fstype.Enqueue(c);
            int j = obj.transform.childCount;
            Debug.Log(j);
            for (int i = 0; i < j; i++)
            {
                ObjectPool.Instance.ReturnObject(obj.transform.GetChild(0).GetComponent<Ingredients>().Gettype(), obj.transform.GetChild(0).gameObject);
            }
        }
        else if (GameManager.Instance.recipe.Recipeck(finishTypes2, index) == finishType2)
        {
            Debug.Log("물안들어있는 라면");
            SoundManager.Instance.SetEffectSoundClip(1);
            c = finishType2;
            GameManager.Instance.recipe.currentingredientstype[index].Clear();
            fstype.Enqueue(c);
            int j = obj.transform.childCount;
            Debug.Log(j);
            for (int i = 0; i < j; i++)
            {
                Debug.Log(i);
                ObjectPool.Instance.ReturnObject(obj.transform.GetChild(0).GetComponent<Ingredients>().Gettype(), obj.transform.GetChild(0).gameObject);
            }
        }
        else
        {
            GameManager.Instance.recipe.currentingredientstype[index].Clear();
            int j = obj.transform.childCount;
            for (int i = 0; i < j; i++)
            {
                ObjectPool.Instance.ReturnObject(obj.transform.GetChild(0).GetComponent<Ingredients>().Gettype(), obj.transform.GetChild(0).gameObject);
            }
        }
    }
    public void OnclickFinish()
    {
        finishimagerect.anchoredPosition = new Vector2(-2000, 0);
        if (fstype.Count == 0 || GameManager.Instance.randomguest.ot.Count == 0)
        {
            Debug.Log("안됨");
        }
        else if (GameManager.Instance.randomguest.ot[0] == fstype.Peek())
        {
            Debug.Log("성공");
            if (GameManager.Instance.randomguest.ot[0] == FinishType.Soup_Ramen)
            {
                InputFood("SR", r[0]);
                finishimage.sprite = finishsprites[0];
                GameManager.Instance.AddCookGold(1);
            }
            else if (GameManager.Instance.randomguest.ot[0] == FinishType.Stir_Fried_Ramen)
            {
                InputFood("SFR", (r[0]));
                finishimage.sprite = finishsprites[1];
                GameManager.Instance.AddCookGold(2);
            }
            else if (GameManager.Instance.randomguest.ot[0] == FinishType.HotBar)
            {
                InputFood("H", r[0]);
                finishimage.sprite = finishsprites[2];
                GameManager.Instance.AddCookGold(3);
            }
            else if (GameManager.Instance.randomguest.ot[0] == FinishType.MayoTuna)
            {
                InputFood("M", r[0]);
                finishimage.sprite = finishsprites[3];
                GameManager.Instance.AddCookGold(4);
            }
            else if (GameManager.Instance.randomguest.ot[0] == FinishType.IceTea)
            {
                InputFood("I", r[0]);
                finishimage.sprite = finishsprites[4];
                GameManager.Instance.AddCookGold(5);
            }
            finishimagerect.DOAnchorPosX(0, 1).OnComplete(() =>
            {
                finishimagerect.DOAnchorPosY(2000, 1);
            });
            isclean[(r[0])] += 1;
            isMode[(r[0])] = true;
            GameManager.Instance.garbageControllrer.SpawnFood(r[0]);
            r[0] = -1;
            for (int i = 0; i < 8; i++)
            {
                if (r[i] == -1 && r[i + 1] != -1)
                {
                    r[i] = r[i + 1];
                    r[i + 1] = -1;
                }
            }
            GameManager.Instance.randomguest.ot.RemoveAt(0);
            fstype.Dequeue();
            ObjectPool.Instance.ReturnObject(PoolObjectType.Order, content.transform.GetChild(0).gameObject);
        }
        else
        {
            fstype.Dequeue();
        }
        Orderck();
    }
    public void OnclickOption()
    {
        Time.timeScale = 0;
        Option.SetActive(true);
    }

    public void OnclickXButton()
    {
        Option.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnclickGotitleButton()
    {
        back2.SetActive(true);
    }

    public void OnclickExitButton()
    {
        back.SetActive(true);
    }

    public void OnclickYes()
    {
        Application.Quit();
    }
    public void OnclickNo()
    {
        back.SetActive(false);
        back2.SetActive(false);
    }
    public void OnclickOptionYes()
    {
        back2.SetActive(false);
        Option.SetActive(false);
        Time.timeScale = 1;
        GameManager.Instance.GoStandby();
    }

    public void OnclickRecipeButton()
    {
        recipeobj.SetActive(true);
    }

    public void OnclickRecipeXButton()
    {
        recipeobj.SetActive(false);
    }
    public void InputFood(string s, int index)
    {
        isFood[index] = s;
    }

    public void Mode(int index)
    {
        isOrder[index] = true;
    }

    public void AddLine(int current)
    {
        maxLine++;
        line[current] = maxLine;
    }
    public void SetCalculate()
    {
        int ingredients = GameManager.Instance.GetIngredients() * 200;
        int fine = GameManager.Instance.GetFine() * 5000;
        int rentexpense = 30000;
        int revenue = GameManager.Instance.GetDayGold();
        int calculate = revenue - (ingredients + fine + rentexpense);

        revenuecounttext.DOText(revenue.ToString(), 1f).OnComplete(() =>
        {
            ingredientcounttext.DOText("-" + ingredients.ToString(), 1f).OnComplete(() =>
            {
                finecounttext.DOText("-" + fine.ToString(), 1f).OnComplete(() =>
                {
                    rentexpensecounttext.DOText("-" + rentexpense.ToString(), 1f).OnComplete(() =>
                    {
                        revenuecalculatecounttext.DOText(calculate.ToString(), 1f).OnComplete(() =>
                        {
                            if (GameManager.Instance.CurrentUser.gold <= -100000)
                            {
                                Debug.Log("?");
                                GameManager.Instance.UpdateState(GameState.Ending);
                            }
                        });
                    });
                });
            });
        });



        GameManager.Instance.CurrentUser.gold += calculate;
        GameManager.Instance.Resetcu();
        ChangeUI();
    }

    public void ResetCalculate()
    {
        revenuecounttext.text = "0";
        ingredientcounttext.text = "0";
        finecounttext.text = "0";
        revenuecalculatecounttext.text = "0";
        rentexpensecounttext.text = "0";
    }
    public void AllReset()
    {
        int j = GameManager.Instance.uiManager.content.transform.childCount;
        for (int i = 0; i < j; i++)
            ObjectPool.Instance.ReturnObject(PoolObjectType.Order, GameManager.Instance.uiManager.content.transform.GetChild(0).gameObject);
        //c(MicroMove);
        //c(Pot1);
        //c(Pot2);
        //c(Pot3);
        //c(Pot4);
        //c(CuttingBoard);
        //c(Macine);
        GameManager.Instance.garbageControllrer.GarbageReset();
        GameManager.Instance.uiManager.fstype.Clear();
        GameManager.Instance.uiManager.finishTypes.Clear();
        GameManager.Instance.randomguest.ot.Clear();
        Orderck();
        for (int i = 0; i < 9; i++)
        {
            allSeat.transform.GetChild(i).GetChild(0).GetComponent<Text>().gameObject.SetActive(false);
            PlayerPrefs.SetInt("DAY", 0);
            isSit[i] = false;
            isEnd[i] = false;
            checkFood[i] = false;
            isOrder[i] = false;
            isMode[i] = false;
            isPlay[i] = false;
            isclean[i] = 0;
            line[i] = -1;
            r[i] = -1;
            isFood[i] = "";
        }
        maxLine = -1;
    }

    public void c(GameObject obj)
    {
        int j = obj.transform.childCount;
        for (int i = 0; i < j; i++)
        {
            ObjectPool.Instance.ReturnObject(obj.transform.GetChild(0).GetComponent<Ingredients>().Gettype(), obj.transform.GetChild(0).gameObject);
        }
    }

    public Canvas GetCanvas()
    {
        return canvas;
    }

    public void OnClickLeft()
    {
        if (ktchenrect.anchoredPosition.x <= -11840)
        {
            return;
        }
        else if(isplay == false)
        {
            isplay = true;
            float a = ktchenrect.anchoredPosition.x;
            ktchenrect.DOAnchorPosX(a - 2960,1).OnComplete(()=> isplay = false);
        }
    }
    public void OnClickRight()
    {
        if (ktchenrect.anchoredPosition.x >= 0)
        {
            return;
        }
        else if(isplay == false)
        {
            isplay = true;
            float a = ktchenrect.anchoredPosition.x;
            ktchenrect.DOAnchorPosX(a + 2960, 1).OnComplete(() =>  isplay = false);
        }
    }

    public void OnClickBGM()
    {
        if(bgcount == 0)
        {
            bgcount = 1;
            SoundManager.Instance.bgmSetVolume(false);
        }
        else
        {
            bgcount = 0;
            SoundManager.Instance.bgmSetVolume(true);
        }
    }
    public void OnClickEf()
    {
        if(efcount == 0)
        {
            efcount = 1;
            SoundManager.Instance.efectSetVolume(false);
        }
        else
        {
            efcount = 0;
            SoundManager.Instance.efectSetVolume(true);
        }
    }
    public void OnIngredientWindowClick()
    {
        if(iwcount == 0)
        {
            iwcount = 1;
            IWButton.DOAnchorPosY(350, 0.3f).SetEase(Ease.Linear);
            iwimage.sprite = soundsprites[4];
        }
        else
        {
            iwcount = 0;
            IWButton.DOAnchorPosY(640, 0.3f).SetEase(Ease.Linear);
            iwimage.sprite = soundsprites[5];
        }
    }
}