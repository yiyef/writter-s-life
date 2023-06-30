using System;
using UnityEngine;


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