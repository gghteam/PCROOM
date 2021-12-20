using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private User user = null;


    private string SAVE_PATH = "";
    private readonly string SAVE_FILENAME = "/SaveFile.txt";
    public User CurrentUser { get { return user; } }

    public int sit { get; private set; }
    public int seatCount { get; private set; }

    public int time { get; private set; }

    public Recipe recipe { get; private set; }
    public UIManager uiManager { get; private set; }
    public GarbageControllrer garbageControllrer { get; private set; }
    public RandomGuest randomguest { get; private set; }
    [SerializeField]
    private GameObject panel;

    public GameState STATE
    {
        get
        {
            return state;
        }
    }
    private List<Component> components = new List<Component>();

    private int dayGold = 0;
    private int fine;
    private int ingredients;
    private GameState state;
    public void Awake()
    {
        Application.targetFrameRate = 40;
        uiManager = GetComponent<UIManager>();
        recipe = FindObjectOfType<Recipe>();
        SAVE_PATH = Application.persistentDataPath + "/Save";
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        LoadFromJson();

        randomguest = FindObjectOfType<RandomGuest>();
        garbageControllrer = FindObjectOfType<GarbageControllrer>();
        components.Add(new UIComponent());
        components.Add(new RandomComponent());
        seatCount = 9;
        sit = 0;
        InvokeRepeating("SaveToJson", 0f, 1f);
        for (int i = 0; i < 7; i++)
        {
            recipe.currentingredientstype.Add(new List<PoolObjectType>());
        }
    }
    private void Start()
    {
        UpdateState(GameState.Init);
        PlayerPrefs.DeleteKey("DAY");
        //PlayerPrefs.DeleteKey("TUTORIAL");
    }
    public void GoStandby()
    {
        UpdateState(GameState.StandBy);
    }
    private void LoadFromJson()
    {
        string json = "";

        if (File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
        }
        else
        {
            SaveToJson();
            LoadFromJson();
        }
    }

    private void SaveToJson()
    {          //persistentDataPath:모바일
        if (user == null) return;
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }
    public int AddItem()
    {
        sit += 1;
        int j = 0;
        int a = 1;
        if(randomguest.RandomGt[randomguest.RandomGt.Count - 1].name == "연예인")
        {
            a = 2;
        }
        //CurrentUser.gold += 1000*a;
        //dayGold += 1000*a;
        int k = 1000 * a;
        for (j = 0; j < seatCount; j++)
        {
            if(!uiManager.isSit[j] && uiManager.isclean[j] == 0)
            {
                uiManager.isSit[j] = true;
                StartCoroutine(uiManager.Timer(j, k));
                break;
            }
        }
        uiManager.ChangeUI();
        return j;
    }

    public void MulDayGold(int i,int j)
    {
        dayGold += i * j;
    }

    public void MinusItem()
    {
        sit--;
    }

    public void UpdateState(GameState state)
    {
        this.state = state;

        for (int i = 0; i < components.Count; i++)
            components[i].UpdateState(state);
        if (state == GameState.Init)
            UpdateState(GameState.StandBy);
    }

    //시간을 초기화합니다.
    public void SetTime()
    {
        time = 9;
        uiManager.ChangeTime();
        InvokeRepeating("AddTime", 30, 30);
    }

    public void AddFine()
    {
        fine++;
        panel.SetActive(true);
        Invoke("DeletePanel", 2);
    }

    private void DeletePanel()
    {
        panel.SetActive(false);
    }

    public void AddIngredients()
    {
        ingredients++;
    }
    public int GetFine()
    {
        return fine;
    }
    public int GetIngredients()
    {
        return ingredients;
    }
    public int GetDayGold()
    {
        return dayGold;
    }

    public void AddCookGold(int i)
    {
        switch(i)
        {
            case 1:
                dayGold += 3000;
                break;
            case 2:
                dayGold += 3000;
                break;
            case 3:
                dayGold += 2000;
                break;
            case 4:
                dayGold += 1000;
                break;
            case 5:
                dayGold += 1500;
                break;
        }
        uiManager.ChangeUI();
    }

    public void Resetcu()
    {
        fine = 0;
        ingredients = 0;
        dayGold = 0;
    }
    //시간을 더하여 계산합니다.
    private void AddTime()
    {
        time++;
        if(time > 25)
        {
            CancelInvoke("AddTime");
            UpdateState(GameState.Calculate);
        }
        uiManager.ChangeTime();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveToJson();
    }
    private void OnApplicationQuit()
    {
        SaveToJson();
    }
    public void Fun()
    {
        CurrentUser.day++;
    }

    public void EveryReset()
    {
        CurrentUser.day = 0;
        CurrentUser.gold = 0;
        dayGold = 0;
        SaveToJson();
    }
}
