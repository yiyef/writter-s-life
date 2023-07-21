using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }
    
    [SerializeField] private GameObject _inputRoot;
    [SerializeField] private InputField _input;

    private Action<string> OnInputEndCallback;

    private void Awake()
    {
        _instance = this;
    }

    public void OnEnable()
    {
        _input.onEndEdit.AddListener(OnEndEdit);
    }
    
    public void OnDisable()
    {
        _input.onEndEdit.RemoveListener(OnEndEdit);
    }

    private void OnEndEdit(string text)
    {
        if (OnInputEndCallback != null)
        {
            OnInputEndCallback(text);
        }
    }
    
    public void ShowInput(Action<string> callback)
    {
        OnInputEndCallback = callback;
        _inputRoot.SetActive(true);
    }

    public void HideInput()
    {
        _inputRoot.SetActive(false);
    }
}