using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectorNode : Node
{
    public SelectorNode(string name, int priorityNumber = 0) : base(name, priorityNumber)
    {
    }

    public override NodeStatus Process()
    {
        if (currentChildIndex > children.Count)
        {
            Reset();
            return Node.NodeStatus.FAILURE;
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
                return Node.NodeStatus.RUNNING;
            default:
                return Node.NodeStatus.NONE;

        }


    }
}

public class SortedSelectorNode : SelectorNode
{
    List<Node> sortedChildren => children.OrderBy(x => x.priorityNumber).ToList();
    public SortedSelectorNode(string name, int priorityNumber = 0) : base(name, priorityNumber)
    {
    }

    /*public override NodeStatus Process()
    {
        
        foreach (var child in sortedChildren)
        {
            switch (child.Process())
            {
                case Node.NodeStatus.SUCCESS:
                    return NodeStatus.SUCCESS;

                case Node.NodeStatus.FAILURE:
                    continue;

                case Node.NodeStatus.RUNNING:
                    return NodeStatus.RUNNING;
            }
        }
        return Node.NodeStatus.FAILURE;
    }*/
   
    public override NodeStatus Process()
    {
        if (currentChildIndex >= sortedChildren.Count)
        {
            Reset();
        }

        switch (sortedChildren[currentChildIndex].Process())
        {
            case Node.NodeStatus.SUCCESS:
                Reset();
                return Node.NodeStatus.SUCCESS;
            case Node.NodeStatus.FAILURE:
                currentChildIndex++;
                return sortedChildren.Count == currentChildIndex ? Node.NodeStatus.FAILURE : Node.NodeStatus.RUNNING;
            case Node.NodeStatus.RUNNING:
                return Node.NodeStatus.RUNNING;
            default:
                return Node.NodeStatus.NONE;

        }
    }
}