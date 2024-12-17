using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    IStrategy strategy { get; set; }
    public Leaf(string name, IStrategy strategy, int priorityNumber = 0) : base(name, priorityNumber)
    {
        this.strategy = strategy;
    }

    public override NodeStatus Process() => strategy.Evaluate();

}