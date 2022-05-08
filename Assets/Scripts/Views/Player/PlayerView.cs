using UnityEngine;

public class PlayerView : MovementBase
{
    void Start()
    {
        InitialPosition = this.transform.position;
        InitialDirection = Vector2.right;

        Reset();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            SetDirection(Vector2.down);        
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            SetDirection(Vector2.up);      
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            SetDirection(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            SetDirection(Vector2.right);

        float angle = Mathf.Atan2(Direction.y, Direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
}
