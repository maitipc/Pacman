using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostView : GhostBehaviour
{
    public event Action OnPacmanCollision;
    public event Action OnScatterStateEnd;

    [SerializeField] GameObject defaultState;
    [SerializeField] GameObject blueState;
    [SerializeField] GameObject eye;
    [SerializeField] GhostsDatabase database;

    public GhostsDatabase Database => database;
    public GameObject Target { get; set; }
    int scatterDuration { get; set; }

    void Start()
    {
        InitialPosition = this.transform.position;
        InitialDirection = Vector2.right;
        Speed = database.MovementSpeed;
        Multiplier = database.SpeedMultiplier;

        Reset();
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            OnPacmanCollision?.Invoke();
    }

    public void ChangeState(GhostState state)
    {
        CurrentState = state;

        switch (state)
        {
            case GhostState.Vulnerable:
                VulnerableGhost(true);
                break;
            case GhostState.Chase:
                VulnerableGhost(false);
                break;
             case GhostState.Scatter:
                VulnerableGhost(false);
                StartCoroutine(ScatterCountdown());
                break;
            case GhostState.Dead:
                DeadGhost();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        availableDirections = new List<Vector2>();

        if (collider.gameObject.layer == LayerMask.NameToLayer("TurningPoint"))
        {
            availableDirections = 
                collider.gameObject.GetComponent<TurningPoints>().availableDirections;

            SetMovementDecision(Target.transform, database.Name);
        }

        if (collider.gameObject == OutHome.gameObject && CurrentState == GhostState.Dead)
            StartCoroutine(Respawn(database.atHomeDuration));
    }

    IEnumerator ScatterCountdown ()
    {
        scatterDuration = database.ScatterDuration;
        
        while (scatterDuration > 0)
        {
            yield return new WaitForSeconds(1);
            scatterDuration--;
        }

        OnScatterStateEnd?.Invoke();
    }

    void VulnerableGhost(bool isVulnerable)
    {
        defaultState.SetActive(!isVulnerable);
        eye.SetActive(!isVulnerable);
        blueState.SetActive(isVulnerable);
        SetMovementDecision(Target.transform, database.Name);
    }

    void DeadGhost()
    {
        defaultState.SetActive(false);
        eye.SetActive(true);
        blueState.SetActive(false);
        SetMovementDecision(AtHome, database.Name);
    }
}
