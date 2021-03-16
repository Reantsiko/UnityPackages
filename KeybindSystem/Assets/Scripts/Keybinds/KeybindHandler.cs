using System.Collections;
using UnityEngine.UI;
using UnityEngine;

/*
 * The buttons that will be used to set the keybindings need to have the exact same name as the command string
 * of the keybind.
 * eg: command in the structure == forward ==> button's name must be forward
 * On the button's in the inspector under "On Click()" press on the + sign and drag the object
 * on which this script is placed. Add the function SetButton and then drag the button in the slot under it.
 * You can also add a tag to the button, this has to be the same as the scriptable object's Player string,
 * to differentiate between keybindings in local co-op.
 * 
 * With the variable array keysToIgnore you can set input that you don't want to be bound to keys.
*/

public class KeybindHandler : MonoBehaviour
{
    public Keybindings[] playerKeybinds = null;
    [SerializeField]private Button[] _keyBindButtons = null;
    [SerializeField]private KeyCode[] _keysToIgnore = null;

    private Coroutine _listeningForInput = null;

    void Start()
    {
        for (int i = 0; i < playerKeybinds.Length; i++)
        {
            playerKeybinds[i].LoadKeys();
            playerKeybinds[i].defaultKeybindings.FillDictionary();
        }
        SetButtonText();
    }
    /*  -----------------------------------------------
        PUBLIC METHODS
        -----------------------------------------------*/
    /*
     * Depending on your menu setup you might have to change
     * pressedButton.GetComponentInChildren<T>(); to pressedButton.GetComponent<T>();
    */
    public void SetButton(Button pressedButton)
    {
        var keybindCommand = pressedButton.GetComponent<KeybindCommand>();
        if (!keybindCommand)
        {
            Debug.LogError("No KeybindCommand script on " + pressedButton.name);
            return;
        }

        var textComponent = pressedButton.GetComponentInChildren<Text>(); //change this depending on how your button is set up
        if (!textComponent)
        {
            Debug.LogError("No text on button");
            return;
        }
        textComponent.text = "Press a key...";
        _listeningForInput = StartCoroutine(ListenForKeyInput(keybindCommand.GetCommand(), pressedButton.tag));
    }
    /*
     * Use this method on buttons, or add it to the end of the IEnumerator ListenForKeyInput
     * to save automatically.
    */
    public void SaveKeys()
    {
        for (int i = 0; i < playerKeybinds.Length; i++)
            playerKeybinds[i].SaveKeys();
    }
    public void ResetKeys()
    {
        for (int i = 0; i < playerKeybinds.Length; i++)
            playerKeybinds[i].ResetKeys();
        SetButtonText();
    }

    /*  -----------------------------------------------
        PRIVATE METHODS
        -----------------------------------------------*/
    private IEnumerator ListenForKeyInput(KeybindEnum command, string p)
    {
        while (!Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (KeyCode keyValue in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyValue))
                {
                    if (IgnoreInput(keyValue))
                        continue;
                    GetCorrectPlayer(p, command, keyValue);
                    SetButtonText();
                    _listeningForInput = null;
                    yield break;
                }
            }
            yield return new WaitForSecondsRealtime(0);
        }
        SetButtonText();
        _listeningForInput = null;
    }

    private bool IgnoreInput(KeyCode keyValue)
    {
        foreach (var ignore in _keysToIgnore)
            if (keyValue == ignore)
                return true;
        return false;
    }

    private void GetCorrectPlayer(string p, KeybindEnum command, KeyCode keyValue)
    {
        for (int i = 0; i < playerKeybinds.Length; i++)
        {
            if (p == playerKeybinds[i].GetPlayerIdentity())
                playerKeybinds[i].SetKey(command, keyValue);
        }
    }

    private void SetButtonText()
    {
        for (int i = 0; i < playerKeybinds.Length; i++)
        {
            var playerInput = playerKeybinds[i].GetPlayerInput();
            foreach (var b in _keyBindButtons)
            {
                foreach (var input in playerInput)
                {
                    if (input.command == b.GetComponent<KeybindCommand>().GetCommand() && b.CompareTag(playerKeybinds[i].GetPlayerIdentity()))
                    {
                        b.GetComponentInChildren<Text>().text = playerKeybinds[i].GetKeyAsString(b.name);
                    }
                }
            }
        }
    }
}
