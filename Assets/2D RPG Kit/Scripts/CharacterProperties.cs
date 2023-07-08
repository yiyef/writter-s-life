using System;
using UnityEngine;

[Serializable]
public struct CharacterPropertiesInfo
{
    public int Time;
    public int CurrentHour;
    public int CurrentMinute;

    public int Stamina;
    public int Spirit;
    public int Food;
    public int Money;
    public int Day; 
}

public class CharacterProperties
{
    public static CharacterPropertiesInfo PlayerInfo;
    public static Action<CharacterPropertiesInfo> OnPropertiesChangeEvent;

    static CharacterProperties()
    {
        PlayerInfo.Stamina = 80;
        PlayerInfo.Spirit = 80;
        PlayerInfo.Food = 80;
        PlayerInfo.Day = 1;
        PlayerInfo.Money = 2000;
        PlayerInfo.CurrentHour = 7;
        PlayerInfo.CurrentMinute = 0;
    }
     
    public static void AddOperation(CharacterPropertiesInfo add)
    {
        PlayerInfo.Time += add.Time;
        PlayerInfo.CurrentHour += add.CurrentHour;
        PlayerInfo.CurrentMinute += add.CurrentMinute;
        PlayerInfo.Stamina += add.Stamina;
        PlayerInfo.Spirit += add.Spirit;
        PlayerInfo.Food += add.Food;
        PlayerInfo.Money += add.Money;
        PlayerInfo.Day += add.Day;
        
        if (OnPropertiesChangeEvent != null)
        {
            OnPropertiesChangeEvent(add);
        }
    }
}