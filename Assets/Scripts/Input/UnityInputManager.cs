using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityInputManager : InputManager
{
    [SerializeField]
    private string _playerAxisPrefix = "";

    [SerializeField]
    private int _maxNumberOfPlayers = 1;

    [Header("Unity Axis Mapping")]
    [SerializeField]
    private string _jumpAxis = "Jump";
    [SerializeField]
    private string _attackAxis = "Fire1";
    [SerializeField]
    private string _subAttackAxis = "Fire2";
    [SerializeField]
    private string _dashkAxis = "Fire3";
    [SerializeField]
    private string _runkAxis = "Run";
    [SerializeField]
    private string _moveHorizontalkAxis = "Horizontal";
    [SerializeField]
    private string _moveVerticalAxis = "Vertical";
    [SerializeField]
    private string _pauseAxis = "Cancel";

    private Dictionary<int, string>[] _actions;

    protected override void Awake()
    {
        base.Awake();

        if(InputManager.instance != null)
        {
            isEnabled = false;
            return;
        }

        SetInstance(this);
        _actions = new Dictionary<int, string>[_maxNumberOfPlayers];

        for(int i = 0; i < _maxNumberOfPlayers; i++)
        {
            Dictionary<int, string> playerActions = new Dictionary<int, string>();
            _actions[i] = playerActions;

            string prefix = !string.IsNullOrEmpty(_playerAxisPrefix) ? _playerAxisPrefix + i : string.Empty;
            AddAction(InputAction.Jump, prefix + _jumpAxis, playerActions);
            AddAction(InputAction.Attack, prefix + _attackAxis, playerActions);
            AddAction(InputAction.SubAttack, prefix + _subAttackAxis, playerActions);
            AddAction(InputAction.Dash, prefix + _dashkAxis, playerActions);
            AddAction(InputAction.Run, prefix + _runkAxis, playerActions);
            AddAction(InputAction.MoveHorizontal, prefix + _moveHorizontalkAxis, playerActions);
            AddAction(InputAction.MoveVertical, prefix + _moveVerticalAxis, playerActions);
            AddAction(InputAction.Pause, prefix + _pauseAxis, playerActions);
        }
    }

    private static void AddAction(InputAction action, string actionName, Dictionary<int, string> actions)
    {
        if (string.IsNullOrEmpty(actionName))
            return;

        actions.Add((int)action, actionName);
    }

    public override bool GetButton(int playerId, InputAction action)
    {
        bool value = Input.GetButton(_actions[playerId][(int)action]);
        return value;
    }

    public override bool GetButtonDown(int playerId, InputAction action)
    {
        bool value = Input.GetButtonDown(_actions[playerId][(int)action]);
        return value;
    }

    public override bool GetButtonUp(int playerId, InputAction action)
    {
        bool value = Input.GetButtonUp(_actions[playerId][(int)action]);
        return value;
    }

    public override float GetAxis(int playerId, InputAction action)
    {
        float value = Input.GetAxisRaw(_actions[playerId][(int)action]);
        return value;
    }

}
