using UnityEngine;
/*
 * Place this script on the UI element that you will use to change keybinds in game.
*/
public class KeybindCommand : MonoBehaviour
{
    [SerializeField] private KeybindEnum _command = KeybindEnum.none;

    public void SetCommand(KeybindEnum toSet) { _command = toSet; }
    public KeybindEnum GetCommand() { return _command; }
}
