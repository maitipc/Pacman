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

    protected void SetMovementDecision(Transform target, GhostName ghostName)
    {
        ChaseBehaviour chaseBehaviour = new ChaseBehaviour();
        ScatterBehaviour scatterBehaviour = new ScatterBehaviour();
        VulnerableBehaviour vulnerableBehaviour = new VulnerableBehaviour();
        DeadBehaviour deadBehaviour = new DeadBehaviour();

        switch (CurrentState)
        {
            case GhostState.Chase:
                SetDirection(
                    chaseBehaviour.Chase(character, target, ghostName, availableDirections)
                );
                Multiplier = chaseBehaviour.Multiplier;
                break;
            case GhostState.Scatter:
                SetDirection(
                    scatterBehaviour.GenerateDirection(availableDirections)
                );
                break;
            case GhostState.Vulnerable:
                SetDirection(
                    vulnerableBehaviour.Vulnerable(character, target, availableDirections)
                );
                Multiplier = vulnerableBehaviour.Multiplier;
                break;
            case GhostState.Dead:
                SetDirection(deadBehaviour.GoToHome(character, OutHome, availableDirections));
                Multiplier = deadBehaviour.Multiplier;    
                break;
            default:
                break;
        }
    }

    protected IEnumerator Respawn (float atHomeDuration)
    {
        character.transform.position = AtHome.position;

        while (atHomeDuration > 0)
        {
            yield return new WaitForSeconds(1);
            atHomeDuration--;
        }
        
        OnGhostRespawned?.Invoke();
        character.transform.position = OutHome.position;
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