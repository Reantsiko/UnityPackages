using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct InputKeys
{
    public string commandName;
    public KeybindEnum command;
    public KeyCode keyCode;
    public KeyCode modifierKeyCode;
}

[CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings")]
public class Keybindings : ScriptableObject
{
    [Tooltip("Input a string ID for the player. This string will be used to differentiate between players in local co-op")]
    [SerializeField] private string _player = null;
    [SerializeField] private InputKeys[] _playerInput = null;
    [Tooltip("Change to true in case you want players to be able to use modifiers such as shift, ctrl, alt")]
    [SerializeField] private bool _useModifier = false;
    [SerializeField] private KeyCode[] _modifierKeyCodes = null;
    public Keybindings defaultKeybindings = null;
    [Tooltip("Input all the other KeyBindingsScriptableObject that you want to be cross checked for overlapping input.")]
    [SerializeField] private Keybindings[] _overlappingKeybindsChecker = null;
    private Dictionary<string, KeyCode> _keyBindingsDictionary = null;

    /*  -----------------------------------------------
           PUBLIC METHODS
        -----------------------------------------------*/
    /*
     * Use this method with Input.GetKey(...);
    */
    public KeyCode GetKeyCode(string command) { return _keyBindingsDictionary[command]; }
    /*
     * Use this method to set the value of keys as text. eg in options menu
    */
    public string GetKeyAsString(string variableName) { return _keyBindingsDictionary[variableName].ToString(); }
    /*
     * Used to differentiate between multiple players in local co-op
    */
    public string GetPlayerIdentity() { return _player; }
    public InputKeys[] GetPlayerInput() { return _playerInput; }
    /*
     * Will change the KeyCode in the array and dictionary.
    */
    public void SetKey(KeybindEnum commandName, KeyCode toSet, bool reset = false)
    {
        if (!_keyBindingsDictionary.ContainsKey(commandName.ToString()))
        {
            Debug.LogError("Command name: " + commandName + " does not exist.");
            return;
        }

        for (int i = 0; i < _playerInput.Length; i++)
        {
            if (_playerInput[i].command == commandName)
            {
                if (!reset)
                    CheckOverlappingKeybinds(toSet);
                _playerInput[i].keyCode = toSet;
                _keyBindingsDictionary[commandName.ToString()] = toSet;
                return;
            }
        }
    }
    /*
     * Resets all keybindings to the default set you created.
    */
    public void ResetKeys()
    {
        if (!defaultKeybindings)
        {
            Debug.LogError("No default keybindings set.");
            return;
        }

        foreach (var keybind in _playerInput)
            SetKey(keybind.command, defaultKeybindings.GetKeyCode(keybind.command.ToString()), true);
    }
    /*
     * Saving and loading of the keybindings through PlayerPrefs.
     * The keys will be saved under the name of the variable + the player number, in order to differentiate between
     * multiple players in local co-op.
    */
    public void SaveKeys()
    {
        PlayerPrefs.SetString("Keybinds " + _player, "Saved");
        foreach (var keybind in _playerInput)
            PlayerPrefs.SetInt(keybind.command + _player, keybind.keyCode.GetHashCode());
        if (_keyBindingsDictionary == null)
            FillDictionary();
    }
    public void LoadKeys()
    {
        if (!PlayerPrefs.HasKey("Keybinds " + _player))
        {
            SaveKeys();
            return;
        }
        for (int i = 0; i < _playerInput.Length; i++)
            _playerInput[i].keyCode = (KeyCode)PlayerPrefs.GetInt(_playerInput[i].command + _player);
        if (_keyBindingsDictionary == null)
            FillDictionary();
    }
    public void FillDictionary()
    {
        if (_keyBindingsDictionary == null)
        {
            _keyBindingsDictionary = new Dictionary<string, KeyCode>();
            foreach (var input in _playerInput)
                _keyBindingsDictionary.Add(input.command.ToString(), input.keyCode);
        }
    }
    /*  -----------------------------------------------
       PRIVATE METHODS
        -----------------------------------------------*/
    private void CheckOverlappingKeybinds(KeyCode keyCode)
    {
        if (_overlappingKeybindsChecker == null) { return; }

        for (int i = 0; i < _overlappingKeybindsChecker.Length; i++)
        {
            for (int j = 0; j < _overlappingKeybindsChecker[i]._playerInput.Length; j++)
            {
                if (_overlappingKeybindsChecker[i]._playerInput[j].keyCode == keyCode)
                {
                    var command = _overlappingKeybindsChecker[i]._playerInput[j].command;
                    _overlappingKeybindsChecker[i].SetKey(command, KeyCode.None, true); //3 param has to be set to true here, otherwise infinite loop
                    return;
                }
            }
        }
    }
}
