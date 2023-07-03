using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DialogInfo
{
    public string[] lines;
}


[RequireComponent(typeof(BoxCollider2D))]
public class PropertiesActivator : MonoBehaviour
{
    public List<DialogInfo> Dialogues; // 每个string数组代表一个对话，可以在Unity编辑器中设置

    public Sprite[] _sprites;
    [SerializeField] 
    private List<CharacterProperties> characterProperties;

    private int _curIndex;
    private CharacterProperties _curCharacterProperties;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 确保只有玩家可以触发
        {
            // 在所有对话中随机选择一个
            _curIndex = UnityEngine.Random.Range(0, Dialogues.Count);
            _curCharacterProperties = characterProperties[_curIndex];
            string[] selectedDialogue = Dialogues[_curIndex].lines;
            
            // 开始对话
            DialogManager.instance.ShowDialogAuto(_sprites,selectedDialogue,false);
            AddProperties();
        }
    }

    public void AddProperties()
    {
        if (PlayerController.instance == null)
        {
            return;
        }
        
        PlayerController.instance.characterProperties.AddOperation(_curCharacterProperties);
        if (PlayerController.instance.OnPropertiesChangeEvent != null)
        {
            PlayerController.instance.OnPropertiesChangeEvent(_curCharacterProperties);
        }
    }

}