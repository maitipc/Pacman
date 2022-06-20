using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostBehaviour : MovementBase
{
    public event Action OnGhostRespawned;
    public event Action OnGhostAtHome;

    protected List<Vector2> availableDirections;
    public GhostState CurrentState { get; set; }
    public Transform AtHome { get; set; }
    public Transform OutHome { get; set; }

    ChaseBehaviour chaseBehaviour;
    ScatterBehaviour scatterBehaviour;
    VulnerableBehaviour vulnerableBehaviour;
    DeadBehaviour deadBehaviour;

    void Awake() 
    {
        chaseBehaviour = new ChaseBehaviour();
        scatterBehaviour = new ScatterBehaviour();
        vulnerableBehaviour = new VulnerableBehaviour();
        deadBehaviour = new DeadBehaviour();
    }

    protected void SetMovementDecision(Transform target, GhostName ghostName)
    {
        Vector2 newDirection = Vector2.zero;

        switch (CurrentState)
        {
            case GhostState.Chase:
                newDirection = chaseBehaviour.Chase(this.gameObject, target, ghostName, availableDirections);
                Multiplier = chaseBehaviour.Multiplier;
                break;
            case GhostState.Scatter:
                newDirection = scatterBehaviour.GenerateDirection(availableDirections);
                break;
            case GhostState.Vulnerable:
                newDirection = vulnerableBehaviour.Vulnerable(this.gameObject, target, availableDirections);
                Multiplier = vulnerableBehaviour.Multiplier;
                break;
            case GhostState.Dead:
                newDirection = deadBehaviour.GoToHome(this.gameObject, OutHome, availableDirections);
                Multiplier = deadBehaviour.Multiplier;    
                break;
            default:
                break;
        }

        SetDirection(newDirection);
    }

    protected IEnumerator Respawn (float atHomeDuration)
    {
        transform.position = AtHome.position;

        yield return new WaitForSeconds(atHomeDuration);
        
        OnGhostRespawned?.Invoke();

        transform.position = OutHome.position;
        SetDirection(SetOutHomeDirection());
    }

    Vector2 SetOutHomeDirection()
    {
        Vector2[] directions = new Vector2[2]
        {
            Vector2.left,
            Vector2.right
        };

        int index = UnityEngine.Random.Range(0, directions.Length);
        return directions[index];
    }
}