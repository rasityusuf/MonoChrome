using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Node
{

    protected int currentChildIndex;
    public int priorityNumber;
    public BlackBoard blackBoard => GameManager.instance.blackBoard;
    public enum NodeStatus
    {
        SUCCESS,
        FAILURE,
        RUNNING,
        NONE
    }

    protected string name;
    protected List<Node> children = new();

    public Node(string name,int priorityNBumber)
    {
        this.name = name;
        this.priorityNumber = priorityNBumber;
    }

    public void AddChild(Node child)
    {
        children.Add(child);
    }
    public virtual Node.NodeStatus Process() => Node.NodeStatus.NONE;

    public void Reset() => currentChildIndex = 0;


}










