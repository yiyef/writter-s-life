using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueSceneScript : MonoBehaviour
{
    public string nextSceneName; // Name of the scene to load after dialog ends

    // These variables can be filled in the Inspector or loaded from some other source
    public Sprite[] portraits;
    public string[] lines;
    public bool displayName = true;


    void Start()
    {
        Debug.Log("test");
        DialogManager.instance.ShowDialogAuto(portraits, lines, displayName);
        StartCoroutine(CheckDialogueEndCoroutine());
    }

    IEnumerator CheckDialogueEndCoroutine()
    {
        // Wait while the dialogue is still active
        while (GameManager.instance.dialogActive)
        {
            yield return null; // Wait for one frame
        }

        // After the dialog ends, load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
