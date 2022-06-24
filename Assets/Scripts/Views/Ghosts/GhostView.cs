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

    int pacmanLayer { get; set; }
    int turningPointLayer { get; set; }
    int outsideHomeLayer { get; set; }

    public GhostsDatabase Database => database;
    public GameObject Target { get; set; }

    void Start()
    {
        pacmanLayer = LayerMask.NameToLayer("Pacman");
        turningPointLayer = LayerMask.NameToLayer("TurningPoint");
        outsideHomeLayer = LayerMask.NameToLayer("OutsideHome");

        InitialPosition = this.transform.position;
        InitialDirection = Vector2.right;
        Speed = database.MovementSpeed;
        Multiplier = database.SpeedMultiplier;

        availableDirections = new List<Vector2>();

        Reset();
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

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.layer == pacmanLayer)
            OnPacmanCollision?.Invoke();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == turningPointLayer)
        {
            availableDirections = 
                collider.gameObject.GetComponent<TurningPoints>().availableDirections;

            SetMovementDecision(Target.transform, database.Name);
        }
        
        if (collider.gameObject.layer == outsideHomeLayer && CurrentState == GhostState.Dead)
        {
            availableDirections = new List<Vector2>();
            StartCoroutine(Respawn(database.atHomeDuration));
        }
    }

    IEnumerator ScatterCountdown ()
    {
        yield return new WaitForSeconds(database.ScatterDuration);
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
