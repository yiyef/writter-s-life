using System;
using UnityEngine;


public class CharacterProperties : MonoBehaviour
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
        if (add == null)
        {
            return;
        }
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

[RequireComponent(typeof(BoxCollider2D))]
public class PropertiesActivator : MonoBehaviour
{
    [SerializeField] 
    private CharacterProperties characterProperties;
    
    [SerializeField]
    private bool onlyActiveOnce = true;

    private bool triggered = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (onlyActiveOnce)
            {
                if (!triggered)
                {
                    AddProperties();
                    triggered = true;
                }
            }
            else
            {
                AddProperties();
                triggered = true;
            }
        }
    }

    public void AddProperties()
    {
        if (PlayerController.instance == null)
        {
            return;
        }
        
        PlayerController.instance.characterProperties.AddOperation(characterProperties);
        if (PlayerController.instance.OnPropertiesChangeEvent != null)
        {
            PlayerController.instance.OnPropertiesChangeEvent(characterProperties);
        }
    }

}