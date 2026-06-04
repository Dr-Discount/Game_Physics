using System.Collections.Generic;
using UnityEngine;

public class PushDownStateMachine
{
    public Dictionary<string, AIState> states = new Dictionary<string, AIState>();
    public Stack<AIState> stack = new Stack<AIState>();

    public AIState currentState { get { return (stack.Count > 0) ? stack.Peek() : null; } }

    public void AddState(AIState state)
    {
        if (states.ContainsKey(state.Name)) { Debug.LogError("State Machine already contain state " + state.Name); return; }
        states[state.Name] = state;
    }

    public void Update()
    {
        currentState?.OnUpdate();
    }

    public void SetState<T>()
    {
        SetState(typeof(T).Name);
    }
    public void PushState<T>()
    {
        PushState(typeof(T).Name);
    }

    public void PushState(string name)
    {
        if (!states.ContainsKey(name)) { Debug.LogError("State Machine does not contains state " + name); return; }

        var nextState = states[name];

        currentState.OnExit();
        stack.Push(nextState);
        currentState.OnEnter();
    }

    public void PopState()
    {
        if (stack.Count == 0) return;

        currentState?.OnExit();
        stack.Pop();

        currentState?.OnEnter();
    }

    public void SetState(string name)
    {
        if (!states.ContainsKey(name)) { Debug.LogError("State Machine does not contains state " + name); return; }

        while (stack.Count > 0) stack.Pop();

        var newState = states[name];
        stack.Push(newState);
        currentState.OnEnter();
    }

    public string GetString()
    {
        string str = "";

        var array = stack.ToArray();
        for (int i = 0; i < array.Length; i++)
        {
            str += array[i].Name;
            if (i < array.Length - 1) str += "\n";
        }

        return str;
    }
}