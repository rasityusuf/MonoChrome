using System;
using System.Collections.Generic;

namespace AdvancedStateHandling
{
    public class AdvancedStateMachine
    {
        public IState currentState { get; set; }
        public Transition prevTransit;
        public StateNode stateNode { get; private set; }
        public Dictionary<Type, StateNode> nodes = new Dictionary<Type, StateNode>();
        public List<Transition> anyTransitions = new List<Transition>();

        public void Update()
        {
            foreach (var transit in anyTransitions)
            {
                if (transit.Condition.Evaluate())
                {  
                    if(prevTransit != transit)
                    {
                        ChangeState(transit.To);
                    }
                    currentState.Update();
                    prevTransit = transit;
                    return;
                }
            }
            prevTransit = null;

            var transition = GetTransition(currentState);

            if (transition != null)
            {
                ChangeState(transition.To);
            }

            currentState.Update();

        }


        private void ChangeState(IState to)
        {
            currentState.OnExit();

            currentState = to;

            currentState.OnStart();
        }

        private Transition GetTransition(IState currentState)
        {
           
            foreach (var transition in nodes[currentState.GetType()].transitions)
            {
                if (transition.Condition.Evaluate())
                    return transition;
            }

            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).state, condition);
        }

        public void AddTransitionFromAnytate(IState to, IPredicate condition)
        {
            anyTransitions.Add(new Transition(to, condition));
        }

        private StateNode GetOrAddNode(IState state)
        {
            if (!nodes.ContainsKey(state.GetType()))
            {
                stateNode = new StateNode(state);
                nodes[state.GetType()] = stateNode;
                return stateNode;
                
            }
            else
            {
                return nodes[state.GetType()];
            }

        }

        public class StateNode
        {
            public IState state { get; private set; }

            public List<Transition> transitions { get; private set; }
            
            public StateNode(IState state)
            {
                this.state = state;
                this.transitions = new();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                transitions.Add(new Transition(to, condition));
            }

        }
    }
}

