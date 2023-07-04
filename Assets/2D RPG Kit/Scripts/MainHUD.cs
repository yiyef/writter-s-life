using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MainHUD : MonoBehaviour
{
    public GameObject root;
    public int Time;
    public int currentHour;
    public int currentMinute;

    int Stamina;
    int Spirit;
    int Food;
    int Money;
    int Day;

    int MaxStamina = 100;
    int MaxFood = 100;
    int MaxSpirit = 100;
    int Skill = 60;
    int Inspiration = 100;
    int Speed = 1500;
    int WordCount;
    double Quality = 6.0;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI FoodText;
    public TextMeshProUGUI DayText;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI StaminaText;
    public TextMeshProUGUI SpiritText;
    public TextMeshProUGUI CurrentHourText;
    public TextMeshProUGUI CurrentMinuteText;
    public TextMeshProUGUI SkillText;
    public TextMeshProUGUI InspirationText;
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI WordCountText;
    public TextMeshProUGUI QualityText;
    public TextMeshProUGUI clockText;


    public GameObject MessagePanel;
    public Text MessageText;

    public GameObject DeathPanel;

    // Start is called before the first frame update
    //void Start()
    //{
    //    SetupGame();

    //}

    public void OnEnable()
    {
        CharacterProperties.OnPropertiesChangeEvent += OnPropertiesChange;
    }

    public void OnPropertiesChange(CharacterPropertiesInfo characterPropertiesInfo)
    {
        UpdateData();
        UpdateUI();
    }

    public void UpdateData()
    {
        Time = CharacterProperties.PlayerInfo.Time;
        currentHour = CharacterProperties.PlayerInfo.Time;
        currentMinute = CharacterProperties.PlayerInfo.CurrentMinute;
        Stamina = CharacterProperties.PlayerInfo.Stamina; 
        Spirit = CharacterProperties.PlayerInfo.Spirit; 
        Food = CharacterProperties.PlayerInfo.Food; 
        Money = CharacterProperties.PlayerInfo.Money; 
        Day = CharacterProperties.PlayerInfo.Day; 
    }

    public void SetupGame()
    {
        UpdateData();
        UpdateUI();
        //DeathPanel.SetActive(false);
        //MessagePanel.SetActive(false);
    }
    void UpdateClockDisplay()
    {
        clockText.text = "Day: " + Day + " Time:" + currentHour.ToString("D2") + ":" + currentMinute.ToString("D2");
    }

    void AdvanceOneMinute()
    {
        currentMinute++;
        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;
            if (currentHour >= 24)
            {
                currentHour = 0;
                Day += 1;
            }
        }

        UpdateClockDisplay();
    }

    public void DoAction(int minutes)
    {
        for (int i = 0; i < minutes; i++)
        {
            AdvanceOneMinute();
        }
    }

    public void AddToSpirit(int value)
    {
        Spirit = Spirit + value;
    }

    public void AddToStamina(int value)
    {
        Stamina = Stamina + value;
    }

    public void AddToFood(int value)
    {
        Food = Food + value;
    }


    public void AddToSkill(int value)
    {
        Skill = Skill + value;
    }
    public void AddToInspiration(int value)
    {
        Inspiration = Inspiration + value;
    }
    public void AddToSpeed(int value)
    {
        Speed = Speed + value;
    }
    public void AddToWordCount(int value)
    {
        WordCount = WordCount + value;
    }
    public void QualityCount(int value)
    {
        Quality = Quality + value;
    }

    public bool BuyItem(int value)

    {
        if (Money + value >= 0)
        {
            Money = Money + value;

            return true;
        }
        else
        {
            DisplayMessage("Not enough money");
            return false;
        }

    }

    public void CheckForFood()
    {
        if (Food <= 0)
        {
            Stamina = Stamina - 10;
        }
    }

    public void UpdateUI()
    {
        ClampHealthAndFood();
        CheckForFood();

        StaminaText.text = "Stamina: " + Stamina;
        SpiritText.text = "Spirit: " + Spirit;
        //FoodText.text = "Food: " + Food;
        //DayText.text = "Day: " + Day;
        //MoneyText.text = "Money: " + Money;
        //WordCountText.text = "WordCount: " + WordCount;

        UpdateClockDisplay();

        if (Stamina <= 0)
            YouDied();

        if (Spirit <= 0)
            YouDied();
    }


    public void ClampHealthAndFood()
    {
        if (Stamina > MaxStamina)
        {
            Stamina = MaxStamina;
        }
        if (Spirit > MaxSpirit)
        {
            Spirit = MaxSpirit;
        }

        if (Food > MaxFood)
        {
            Food = MaxFood;
        }
    }
    public void YouDied()
    {
        DeathPanel.SetActive(true);
    }




    public void DisplayMessage(string message)
    {
        MessageText.text = message;
        MessagePanel.SetActive(true);
        Invoke("HideMessage", 2);


    }
    public void HideMessage()
    {
        MessagePanel.SetActive(false);
    }

    void Update()
    {
        root.SetActive(!GameManager.instance.cutSceneActive);
    }

    // Use this for initialization
    void Start()
    {
        SetupGame();
        UpdateUI();
    }
}
