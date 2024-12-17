using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Windows;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected PlayerController playerController => 
        GameManager.instance.blackBoard.GetValue("PlayerController",out PlayerController _playerController) ? _playerController : null;

    public Rigidbody2D rb;

    public float patrolSpeed;
    public float chaseSpeed;
    public List<Transform> patrolPoints;
    public float timer;
    public float direction;

    public float lineOfSight;

    public int currentPatrolIndex = 0;

    [Header("LayerMasks")]
    [SerializeField] private LayerMask playerLayer;


    [Header("CenterPoint")]
    public Transform centerPoint;
    
    [Header("Conditions")]
    public bool isFacingLeft;
    public bool playerInDistance;

    public bool playerInTheAreaOfPatrol => patrolPoints[0].transform.position.x < playerController.transform.position.x &&
        patrolPoints[1].transform.position.x > playerController.transform.position.x;

    public bool canChasePlayer => playerInDistance && playerInTheAreaOfPatrol;

    [Header("Alert State")]
    public Image alertImage;


    BehaviourTree behaviourTree = new BehaviourTree("Enemy");

    void Start()
    {
        SortedSelectorNode mainSelector = new SortedSelectorNode("MainSelector");

        //patrol
        SequenceNode patrolSequence = new SequenceNode("PatrolSequence",10);

        Leaf patrolStrategy = new Leaf("PatrolStrategy", new PatrolStrategy(this));
        patrolSequence.AddChild(patrolStrategy);

        //chase
        SequenceNode chaseSequence = new SequenceNode("ChaseSequence", 5);

        Leaf chaseCondition = new Leaf("ChaseCondition",new Condition(() => canChasePlayer));
        Leaf alertStrategy = new Leaf("AlertStrategy", new AlertStrategy(this));
        Leaf chaseStrategy = new Leaf("ChaseStrategy", new ChaseStrategy(this));

        chaseSequence.AddChild(chaseCondition);
        chaseSequence.AddChild(alertStrategy);
        chaseSequence.AddChild(chaseStrategy);

        mainSelector.AddChild(patrolSequence);
        mainSelector.AddChild(chaseSequence);

        behaviourTree.AddChild(mainSelector);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        behaviourTree.Process();
    }
    void Update()
    {
        SetDirection();
        SetRotation();
        //Debug.Log("Direction:" + direction);
        CheckIfPlayerInDistance();
        /*Debug.Log("Player in distance:" + playerInDistance);
        Debug.Log("Can chase:" + canChasePlayer);*/
    }

    private void CheckIfPlayerInDistance()
    {
        playerInDistance = 
            Physics2D.Raycast(transform.position, direction > 0 ?
            Vector2.right : Vector2.left, lineOfSight,playerLayer);
    }

    public void SetDirection()
    {
        direction = (patrolPoints[currentPatrolIndex].transform.position - transform.position).normalized.x;

    }

    public void SetRotation()
    {
        if (!isFacingLeft && direction < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isFacingLeft = true;
        }
        if (isFacingLeft && direction > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            isFacingLeft = false;
        }

    }

    private void OnDrawGizmos()
    {
       Gizmos.DrawLine(
            transform.position,new Vector2(transform.position.x + (direction > 0 ? lineOfSight : -1 * lineOfSight),transform.position.y));
    }

   
}
