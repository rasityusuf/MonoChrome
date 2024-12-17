using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AdvancedStateHandling
{
    public class FuncPredicate : IPredicate
    {

        public Func<bool> func { get; private set; }
        public FuncPredicate(Func<bool> func)
        {
            this.func = func;
        }
        public bool Evaluate() => func.Invoke();
      
    }
}
