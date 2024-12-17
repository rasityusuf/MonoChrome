using AdvancedStateHandling;
using Cinemachine;
using System.Collections;
using UnityEngine;

public class MainPlayerState : IState
{
    protected PlayerController playerController;
    public MainPlayerState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public virtual void OnStart()
    {
        return;
    }

    public virtual void OnExit()
    {
        return;
    }


    public virtual void Update()
    {
        return;
    }
}


public class MoveState : MainPlayerState
{
    
    public MoveState(PlayerController playerController) : base(playerController)
    {
    }

   
    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnExit()
    {
        base.OnExit();
    }


    public override void Update()
    {
        base.Update();
        playerController.rb.linearVelocity = new Vector2(playerController.moveSpeed * playerController.xInput, playerController.rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.jump = true;
        }
        
    }
}


public class InInteractionState : MainPlayerState
{
    public InInteractionState(PlayerController playerController) : base(playerController)
    {
    }


    public override void OnStart()
    {
        base.OnStart();
        playerController.rb.linearVelocity = new Vector2(0, 0);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("In Interaction");
    }
}

public class JumpState : MainPlayerState
{
    float jumpForce = 17f;

    public JumpState(PlayerController playerController) : base(playerController)
    {
    }


    public override void OnStart()
    {
        base.OnStart();

        playerController.rb.linearVelocity = new Vector2(0, jumpForce);

    }

    public override void OnExit()
    {
        base.OnExit();
        playerController.jump = false;
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("jumpState");

    }
}

public class DamageState : MainPlayerState
{
    float xMagnitude = 55f;
    float timer = 0.3f;
    CinemachineBasicMultiChannelPerlin channel =>
        GameManager.instance.blackBoard.GetValue("Channel", out CinemachineBasicMultiChannelPerlin _channel) ? channel : null;
    public DamageState(PlayerController playerController) : base(playerController)
    {
    }

    public override void OnStart()
    {
        base.OnStart();
        playerController.rb.linearVelocity = Vector2.zero;
        playerController.rb.
            AddForce(new Vector2(playerController.currentCollidedEnemy.direction * xMagnitude, 0),ForceMode2D.Impulse);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Update()
    {
        base.Update();
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            playerController.isDamaged = false;
            timer = 0.3f;

        }
    }

   
}

public class StayStillState : MainPlayerState
{
    public StayStillState(PlayerController playerController) : base(playerController)
    {
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnStart()
    {
        base.OnStart();
        playerController.StartCoroutine(Timer());
       
    }

    public override void Update()
    {
        base.Update();
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        playerController.rb.linearVelocity = Vector2.zero;
    }
}