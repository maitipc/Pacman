using System;
using UnityEngine;

public class PelletView : MonoBehaviour
{
    public event Action<int, bool> OnPelletEaten;
    
    [SerializeField] int points;
    [SerializeField] bool isPowerPellet;

    int pacmanLayer;

    void Awake()
    {
        pacmanLayer = LayerMask.NameToLayer("Pacman");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == pacmanLayer)
            EatPellet();
    }

    void EatPellet()
    {
        this.gameObject.SetActive(false);
        OnPelletEaten?.Invoke(points, isPowerPellet);
    }
}
