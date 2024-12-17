using UnityEngine;
using AdvancedStateHandling;
using System.Collections.Generic;
public class PlayerController : MonoBehaviour
{
    public int inWhichRoom;

    public float moveSpeed;
    public float xInput;
    public Rigidbody2D rb;


    public Enemy currentCollidedEnemy;

    public Animator characterAnimator;

    public Transform center;

    public Transform silhoutte;

    public List<Collider2D> roomCollisions = new List<Collider2D>();

    [Header("Inventory Amounts")]
    public int candleCounter;
    public int shadowPartCounter;
    public int keyCounter;

    [Header("Conditions")]
    public bool canCheck;
    public bool isInInteraction;
    public bool jump;
    public bool isJumped;
    public bool isDamaged;
    public bool hasCandle;
    public bool isInShadowEffect = false;
    public bool isOnShadowTrigger = false;
    public bool disableTrigger = false;
    public bool stop = false;
    public bool playerSet = false;
    public bool hasShadowPart;
    public bool hasKey;
    public bool inStair;
    public bool isFacingRight = true;
    public bool isInCollisionWithDrawer;

    [Header("DamageDirection")] 

    public AdvancedStateMachine playerStateMachine;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));
        GameManager.instance.blackBoard.SetValue("PlayerController", this);

        playerStateMachine = new AdvancedStateMachine();
        var moveState = new MoveState(this);
        var inInteractionState = new InInteractionState(this);
        var jumpState = new JumpState(this);
        var damageState = new DamageState(this);

        Add(moveState, jumpState, new FuncPredicate(() => jump));
        Add(jumpState, moveState, new FuncPredicate(() => !isJumped && rb.linearVelocity.y == 0f));
        Add(moveState, inInteractionState, new FuncPredicate(() => isInInteraction));
        Add(inInteractionState, moveState, new FuncPredicate(() => !isInInteraction));
        Add(jumpState, inInteractionState, new FuncPredicate(() => isInInteraction));
        Add(inInteractionState, jumpState, new FuncPredicate(() => !isInInteraction ));

        Any(damageState, new FuncPredicate(() => isDamaged));

        Add(damageState, moveState, new FuncPredicate(() => !isDamaged));
        
        playerStateMachine.currentState = moveState;
       

    }

    public void Add(IState from, IState to, IPredicate condition)
    {
        playerStateMachine.AddTransition(from, to, condition);
    }

    public void Any(IState to, IPredicate condition)
    {
        playerStateMachine.AddTransitionFromAnytate(to, condition);
    }

    void Update()
    {
        if(playerStateMachine.currentState is not InInteractionState)
            SetXAxis();
        AnimationController();
        if (!isInInteraction)
        {
            SetRotation();
        }

        if(candleCounter <= 0)
        {
            hasCandle = false;
            candleCounter = 0;

        }

        if (shadowPartCounter <= 0)
        {
            shadowPartCounter = 0;
            hasShadowPart = false;
        }

        if(keyCounter <= 0)
        {
            keyCounter = 0;
            hasKey = false;
        }

       

        Debug.Log("Room collisions" + roomCollisions.Count);
 
        playerStateMachine.Update();
    }

    public void SetXAxis()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        
    }

    public void SetRotation()
    {
        if (isFacingRight && xInput<0)
        {
            transform.localScale = new Vector2 (-1, 1);
            isFacingRight = false;
        }
        if (!isFacingRight && xInput > 0)
        {
            transform.localScale = new Vector2(1, 1);
            isFacingRight = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
         Debug.Log("OnTrigger");
         if (collision.TryGetComponent<ICheckable>(out ICheckable checkable))
         {
              checkable.OnEnterCheck();
              
         }

         if (collision.TryGetComponent<Candle>(out Candle candle))
         {
            Debug.Log("Candle Interaction Started");
            if (hasCandle)
                candle.OnStartInteract();
            else
            {
                if (!candle.isInteracted)
                {
                    UIController.instance.dialogue.text = candle.toSay;
                    ActivateAlertPanel();
                }
                  

            }

         }

        if (collision.TryGetComponent<MainEvent>(out MainEvent _event) && !_event.eventProgressed)
        {
            
            UIController.instance.dialogue.text = _event.textToSay;
            _event.eventProgressed = true;

            UIController.instance.alertPanel.SetActive(true);
        }

        if (collision.TryGetComponent<Room>(out Room room))
        {
             Debug.Log("Room trigger");
             room.SetPlayerColorToBlack(this);
             roomCollisions.Add(room.GetComponent<Collider2D>());
             room.isPlayerInHere = true;
                  
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ICheckable>(out ICheckable checkable))
        {
            checkable.OnLeaveCheck();
        }
        if (collision.TryGetComponent<Candle>(out Candle candle))
        {
            if(hasCandle)
                candle.OnLeaveInteract();
            else
                DisableAlertPanel();
        }

        if (collision.TryGetComponent<MainEvent>(out MainEvent _event))
        {
            DisableAlertPanel();
        }
       
        if (collision.TryGetComponent<Room>(out Room room))
        {
            room.isPlayerInHere = false;
            roomCollisions.Remove(room.GetComponent<Collider2D>());
            if (roomCollisions.Count == 0)
            {
                room.SetPlayerColorToWhite(this);
            }

        }
       
    }
    public void OnCollideWithEnemy(Enemy enemy)
    {
        currentCollidedEnemy = enemy;
        isDamaged = true;
    }

    public void AnimationController()
    {
        characterAnimator.SetFloat("xDirection", xInput);
    }

    public void ActivateAlertPanel()
    {
        UIController.instance.alertPanel.SetActive(true);
    }

    public void DisableAlertPanel()
    {
        UIController.instance.alertPanel.SetActive(false);
    }

}
