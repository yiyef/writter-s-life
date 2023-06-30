using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BoxCollider2D))]
public class PropertiesActivator : MonoBehaviour
{
    private Sprite[] _sprites;
    [SerializeField] 
    private List<CharacterProperties> characterProperties;
    [SerializeField]
    public List<string[]> dialogues; // 每个string数组代表一个对话，可以在Unity编辑器中设置


    private int _curIndex;
    private CharacterProperties _curCharacterProperties;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 确保只有玩家可以触发
        {
            // 在所有对话中随机选择一个
            _curIndex = UnityEngine.Random.Range(0, dialogues.Count);
            _curCharacterProperties = characterProperties[_curIndex];
            string[] selectedDialogue = dialogues[_curIndex];
            
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