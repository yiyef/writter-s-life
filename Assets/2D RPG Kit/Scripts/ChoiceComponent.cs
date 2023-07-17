using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ChoiceData
{
    public string choiceDesc;
    public bool NeedGoto;
    public int GotoIndex;
    public CharacterPropertiesInfo PropertiesInfo;
}

public class ChoiceComponent : MonoBehaviour
{
    public Button _choicesBtn;
    public Text desc;
    private ChoiceData _choiceData;
    public void InitChoiceComponent(ChoiceData choiceData)
    {
        _choiceData = choiceData;
        _choicesBtn.onClick.RemoveListener(OnClickBtnPress);
        _choicesBtn.onClick.AddListener(OnClickBtnPress);
        desc.text = choiceData.choiceDesc;
    }

    public void OnClickBtnPress()
    {
        if (_choiceData == null)
        {
            return;
        }

        int nextGotoIndex = -1;

        if (_choiceData.NeedGoto)
        {
            nextGotoIndex = _choiceData.GotoIndex;
        }
        DialogManager.instance.ShowNextDialog(nextGotoIndex);
        CharacterProperties.AddOperation(_choiceData.PropertiesInfo); 
    }
}