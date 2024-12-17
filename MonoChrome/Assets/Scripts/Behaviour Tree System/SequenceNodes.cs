using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : Node
{
    public SequenceNode(string name, int priorityNBumber = 0) : base(name, priorityNBumber)
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
                currentChildIndex++;
                return children.Count == currentChildIndex ? NodeStatus.SUCCESS : NodeStatus.RUNNING;
            case Node.NodeStatus.FAILURE:
                Reset();
                return Node.NodeStatus.FAILURE;
            case Node.NodeStatus.RUNNING:
                //Reset();
                return Node.NodeStatus.RUNNING;
            default:
                return Node.NodeStatus.NONE;

        }
    }
}