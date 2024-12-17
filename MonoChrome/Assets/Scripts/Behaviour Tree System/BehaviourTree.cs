using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : Node
{
    public BehaviourTree(string name, int priorityNumber = 0) : base(name, priorityNumber)
    {
    }

    public override NodeStatus Process()
    {
        if (currentChildIndex >= children.Count)
        {
            Reset();
            //return Node.NodeStatus.FAILURE;
        }
        switch (children[currentChildIndex].Process())
        {
            case Node.NodeStatus.SUCCESS:
                Reset();
                return Node.NodeStatus.SUCCESS;
            case Node.NodeStatus.FAILURE:
                currentChildIndex++;
                return Node.NodeStatus.RUNNING;
            case Node.NodeStatus.RUNNING:
                //currentChildIndex++;
                return Node.NodeStatus.RUNNING;
            default:
                return Node.NodeStatus.NONE;
        }
    }
}
