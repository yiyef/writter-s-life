using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DialogInfo
{
    public string Desc;
    public List<ChoiceData> ChoicesData;
    public CharacterPropertiesInfo PropertiesInfo;
}

[Serializable]
public class DialogGroup
{
    public List<DialogInfo> dialogInfos;

    public List<ChoiceData> FindChoiceData(string desc)
    {
        if (dialogInfos == null)
        {
            return null;
        }

        for (int i = 0; i < dialogInfos.Count; i++)
        {
            if (dialogInfos[i].Desc.Equals(desc))
            {
                return dialogInfos[i].ChoicesData;
            }
        }
        return null;
    }
    
    public bool TryGetDialogInfo(string desc,out DialogInfo dialogInfo)
    {
        if (dialogInfos != null)
        {
            for (int i = 0; i < dialogInfos.Count; i++)
            {
                if (dialogInfos[i].Desc.Equals(desc))
                {
                    dialogInfo = dialogInfos[i];
                    return true;
                }
            }
        }
        
        dialogInfo = default;
        return false;
    }
    
    public CharacterPropertiesInfo FindProperties(string desc)
    {
        CharacterPropertiesInfo result = new CharacterPropertiesInfo();
        for (int i = 0; i < dialogInfos.Count; i++)
        {
            if (dialogInfos[i].Desc.Equals(desc))
            {
                result = dialogInfos[i].PropertiesInfo;
            }
        }
        return result;
    }
    
    public string[] GetLines()
    {
        if (dialogInfos == null)
        {
            return null;
        }

        List<string> result = new List<string>();
        for (int i = 0; i < dialogInfos.Count; i++)
        {
            result.Add(dialogInfos[i].Desc);
        }

        return result.ToArray();
    }
}

[RequireComponent(typeof(BoxCollider2D))]
public class PropertiesActivator : MonoBehaviour
{
    [SerializeField]
    public List<DialogGroup> Dialogues;

    public Sprite[] _sprites;
    
    private int _curIndex;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogError(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Facing Collider")) // 确保只有玩家可以触发
        {
            // 在所有对话中随机选择一个
            _curIndex = UnityEngine.Random.Range(0, Dialogues.Count);
            DialogGroup dialogGroup = Dialogues[_curIndex];
            if (dialogGroup.dialogInfos != null)
            {
                // 开始对话
                DialogManager.instance.ShowDialogAuto(_sprites, dialogGroup, false);
            }
        }
    }

}