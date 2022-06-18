using System;
using UnityEngine;

public class PelletView : MonoBehaviour
{
    public event Action<int, bool, int> OnPelletEaten;
    
    [SerializeField] int points;
    [SerializeField] bool isPowerPellet;

    int effectDuration;
    int pacmanLayer;

    void Awake()
    {
        pacmanLayer = LayerMask.NameToLayer("Pacman");
        
        if (isPowerPellet)
            effectDuration = 8; //passar isso pro game config
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == pacmanLayer)
            EatPellet();
    }

    void EatPellet()
    {
        this.gameObject.SetActive(false);
        OnPelletEaten?.Invoke(points, isPowerPellet, effectDuration);
    }
}
