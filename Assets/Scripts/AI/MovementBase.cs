using UnityEngine;

public abstract class MovementBase : MonoBehaviour
{
    const float DISTANCE_FROM_TILE = 1.2f;

    public float Speed {get; set;}
    public float Multiplier {get; set;}
    
    protected Vector2 Direction {get; private set;}
    protected Vector3 InitialPosition {get; set;}
    protected Vector2 InitialDirection {get; set;} 
    protected GameObject character => this.gameObject;  
    protected new Rigidbody2D rigidbody => character.GetComponent<Rigidbody2D>();
    
    Vector2 nextDirection;
    LayerMask MazeWalls => LayerMask.GetMask("Walls");

    public void Reset ()
    {
        Direction = InitialDirection;
        nextDirection = Vector2.zero;
        character.transform.position = InitialPosition;
        rigidbody.isKinematic = false;
    }

    void FixedUpdate()
    {
        Vector2 currentPosition = rigidbody.position;
        Vector2 destination = Direction * Speed * Multiplier * Time.fixedDeltaTime;

        rigidbody.MovePosition(currentPosition + destination);
    }

    void Update()
    {
        if (nextDirection != Vector2.zero)
            SetDirection(nextDirection);
    }

    protected void SetDirection(Vector2 newDirection)
    {
        if (IsDirectionAvailable(newDirection))
        {
            Direction = newDirection;
            nextDirection = Vector2.zero;
        } 
        else 
        {
            nextDirection = newDirection;
        }
    }

    bool IsDirectionAvailable (Vector2 direction)
    {
        RaycastHit2D raycast = Physics2D.Raycast(
            transform.position, 
            direction, 
            DISTANCE_FROM_TILE, 
            MazeWalls
        );
        
        return raycast.collider == null;
    }
}