using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LocalizationScript : MonoBehaviour
{
    public Text LanguageText;
    public Text newText;
    public Text continueText;
    public Text exitText;
    public Text languageText;
    public Text nextText;
    public Text prevText;
    public Text saveText;
    //Start is called before the first frame update
    void Start()
    {
        RefreshTexts();
    }

    private void RefreshTexts()
    {
        LanguageText.text = GleyLocalization.Manager.GetText(WordIDs.language1).ToString();
        newText.text = GleyLocalization.Manager.GetText(WordIDs.new1).ToString(); 
        //continueText.text = GleyLocalization.Manager.GetText(WordIDs.continue1).ToString();
        //exitText.text = GleyLocalization.Manager.GetText(WordIDs.exit1).ToString();
        //languageText.text = GleyLocalization.Manager.GetText(WordIDs.language1).ToString();
        nextText.text = GleyLocalization.Manager.GetText(WordIDs.nextId).ToString();
        prevText.text = GleyLocalization.Manager.GetText(WordIDs.prevId).ToString();
        saveText.text = GleyLocalization.Manager.GetText(WordIDs.saveId).ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void NextLanguage()
    {
        GleyLocalization.Manager.NextLanguage();
        RefreshTexts();
    }

    public void PrevLanguage()
    {
        GleyLocalization.Manager.PreviousLanguage();
        RefreshTexts();
    }

    public void SaveLanguage()
    {
        GleyLocalization.Manager.SetCurrentLanguage(GleyLocalization.Manager.GetCurrentLanguage());
    }
}
