using System;
using UnityEngine;

[Serializable]
public struct CharacterProperties
{
    public int Time;
    public int CurrentHour;
    public int CurrentMinute;

    public int Stamina;
    public int Spirit;
    public int Food;
    public int Money;
    public int Day;

    public void AddOperation(CharacterProperties add)
    {
        this.Time = add.Time;
        this.CurrentHour = add.CurrentHour;
        this.CurrentMinute = add.CurrentMinute;
        this.Stamina = add.Stamina;
        this.Spirit = add.Spirit;
        this.Food = add.Food;
        this.Money = add.Money;
        this.Day = add.Day;
    }
}