using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdvancedStateHandling
{
    public class Transition
    {
        public IState To { get; set; }
        public IPredicate Condition { get; set; }

        public Transition(IState to, IPredicate condition)
        {
            To = to;
            Condition = condition;
        }
    }
}

