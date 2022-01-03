using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine
{
    private Dictionary<Type, PlayerState> instantiatedStates = new Dictionary<Type, PlayerState>();
    public PlayerState currentState { get; private set; }
    public bool holdToSprint { get; private set; }

    public StateMachine(object owner, PlayerState[] states)
    {
        Debug.Assert(states.Length > 0);

        foreach (PlayerState state in states)
        {
            PlayerState instantiated = UnityEngine.Object.Instantiate(state);
            instantiated.Initialize(this, owner);
            instantiatedStates.Add(state.GetType(), instantiated);

            if (!currentState)
                currentState = instantiated;
        }
        currentState.EnterState();

        //Sub
        EventHandler<SaveSettingsEvent>.RegisterListener(OnSaveSettings);
    }
  
    public void RunUpdate()
    {
        currentState?.RunUpdate();
    }
    public void ChangeState<T>() where T : PlayerState
    {
        Debug.Log("Changing State");
        if (instantiatedStates.ContainsKey(typeof(T)))
        {
            PlayerState instance = instantiatedStates[typeof(T)];
            currentState?.ExitState();
            currentState = instance;
            currentState.EnterState();
            EventHandler<PlayerStateChangeEvent>.FireEvent(new PlayerStateChangeEvent(currentState));
        }
        else
            Debug.Log(typeof(T) + "not found");
    }
    //Storing previous state when entering airborne
    public void ChangeState<T>(PlayerState previousState) where T : PlayerState
    {
        if (instantiatedStates.ContainsKey(typeof(T)))
        {
            PlayerState instance = instantiatedStates[typeof(T)];
            currentState?.ExitState();
            currentState = instance;
            currentState.EnterState(previousState);
            EventHandler<PlayerStateChangeEvent>.FireEvent(new PlayerStateChangeEvent(currentState));
        }
        else
            Debug.Log(typeof(T) + "not found");
    }
    private void OnSaveSettings(SaveSettingsEvent eve)
    {
        holdToSprint = !eve.settingsData.pressToSprint;
    }
}
