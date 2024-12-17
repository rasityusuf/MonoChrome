using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdvancedStateHandling
{
    public interface IState
    {
        void OnStart();
        void OnExit();
        void Update();

    }

    public interface IPredicate
    {
        bool Evaluate();
    }
}
