using System;
using System.Collections;
using UnityEngine;

public class MainStrategyForEnemy
{
    protected Enemy enemy;
    public MainStrategyForEnemy(Enemy enemy)
    {
        this.enemy = enemy; 
    }

    protected PlayerController playerController =>
        GameManager.instance.blackBoard.GetValue("PlayerController", out PlayerController _controller) ? _controller : null;  
}

public class Condition : IStrategy
{
    Func<bool> condition;

    public Condition(Func<bool> condition)
    {
        this.condition = condition;
    }

    public Node.NodeStatus Evaluate()
    {
        if (condition())
        {
            return Node.NodeStatus.SUCCESS;
        }
        else
        {
            return Node.NodeStatus.FAILURE;
        }
    }
}


public class PatrolStrategy : MainStrategyForEnemy, IStrategy
{
    float distance = 0.2f;

    bool coroutineBlocker = false;

    public PatrolStrategy(Enemy enemy) : base(enemy)
    {
        
    }

    public Node.NodeStatus Evaluate()
    {
        Debug.Log("PatrolStratgy");
       
        if (Vector2.Distance(enemy.transform.position, 
            new Vector2(enemy.patrolPoints[enemy.currentPatrolIndex].transform.position.x, enemy.transform.position.y)) < distance)
        {
            if (!coroutineBlocker)
            {
                enemy.StartCoroutine(Timer());
                coroutineBlocker = true;
            }
        }
        else
        {
           enemy.transform.position =
           Vector2.MoveTowards(enemy.transform.position, 
           new Vector2(enemy.patrolPoints[enemy.currentPatrolIndex].transform.position.x,enemy.transform.position.y), Time.deltaTime * enemy.patrolSpeed);
        }

        return Node.NodeStatus.SUCCESS;
    }

    private IEnumerator Timer()
    {   
        yield return new WaitForSeconds(enemy.timer);
        coroutineBlocker = false;
        enemy.currentPatrolIndex++;
        if(enemy.currentPatrolIndex >= enemy.patrolPoints.Count)
        {
            enemy.currentPatrolIndex = 0;
        }
    }
}


public class AlertStrategy : MainStrategyForEnemy, IStrategy
{
    float jumpForce = 10f;
    bool isJumped = false;
    public AlertStrategy(Enemy enemy) : base(enemy)
    {
              
    }

    public Node.NodeStatus Evaluate()
    {
        Debug.Log("Alert Strategy");
        if (!isJumped)
        {
            enemy.alertImage.enabled = true;
            enemy.rb.linearVelocity = new Vector2(0, jumpForce);
            isJumped = true;
        }

        if(enemy.rb.linearVelocity.y == 0)
        {
            enemy.alertImage.enabled = false;
            isJumped=false;
            return Node.NodeStatus.SUCCESS; 
        }
        return Node.NodeStatus.RUNNING;
    }
}


public class ChaseStrategy : MainStrategyForEnemy, IStrategy
{
 
    public ChaseStrategy(Enemy enemy) : base(enemy)
    {
    }

    public Node.NodeStatus Evaluate()
    {
        Debug.Log("ChaseStrategy");
        
        if (enemy.canChasePlayer)
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, playerController.transform.position, Time.deltaTime * enemy.chaseSpeed);
            return Node.NodeStatus.RUNNING;

        }
        else
        {
            return Node.NodeStatus.SUCCESS;
        }
    }
}


