using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogAndChangeValue : MonoBehaviour
{
    public struct PlayerInfo
    {
        public int Time;
        public int currentHour;
        public int currentMinute;

        int Stamina;
        int Spirit;
        int Food;
        int Money;
        int Day;

        int MaxStamina;
        int MaxFood;
        int MaxSpirit;
        int Skill;
        int Inspiration;
        int Speed;
        int WordCount;
        double Quality;
    };

    public struct dialogInfo
    {
        public string[] dialogs;
        public Sprite[] sprites;
    }
    public Dictionary<string[],PlayerInfo> dialogueDict; // 每个string数组代表一个对话，可以在Unity编辑器中设置
    public float valueChange; // 碰撞后数值的变动，可以在Unity编辑器中设置

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 确保只有玩家可以触发
        {
            // 在所有对话中随机选择一个
            int index = Random.Range(0, dialogueDict.Count);
            int temp = 0;
            string[] selectedDialogue;
            PlayerInfo info;
            foreach (var iterator in dialogueDict)
            {
                if(temp == index)
                {
                    selectedDialogue = iterator.Key;
                    info = iterator.Value;
                    break;
                }
            }
             

            // 开始对话
            //DialogManager.instance.ShowDialogAuto(selectedDialogue);

            // 引起数值变动
            //ValueHolder.instance.value += valueChange; // 假设你有一个ValueHolder脚本保存了你想要改变的数值
        }
    }
}