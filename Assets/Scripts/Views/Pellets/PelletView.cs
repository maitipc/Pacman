using System;
using UnityEngine;

public class PelletView : MonoBehaviour
{
    public event Action<int, bool, int> OnPelletEaten;
    
    [SerializeField] int points;
    [SerializeField] bool isPowerPellet;

    int effectDuration;

    void Awake()
    {
        if (isPowerPellet)
            effectDuration = 8;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            EatPellet();
    }

    void EatPellet()
    {
        this.gameObject.SetActive(false);
        OnPelletEaten?.Invoke(points, isPowerPellet, effectDuration);
    }
}
