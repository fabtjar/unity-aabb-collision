using UnityEngine;

public class Player : MonoBehaviour
{
    float moveSpeed = 4f;
    float gravity = -20f;
    float jumpForce = 10f;
    Vector2 velocity;

    ColliderBox[] walls;
    ColliderBox colliderBox;

    SpriteRenderer sprite;
    bool onGround;

    void Start()
    {
        colliderBox = GetComponent<ColliderBox>();

        var wallObjects = GameObject.FindGameObjectsWithTag("Wall");
        walls = new ColliderBox[wallObjects.Length];
        for (int i = 0; i < walls.Length; i++)
            walls[i] = wallObjects[i].GetComponent<ColliderBox>();
    }

    void Update()
    {
        velocity.y += gravity * Time.deltaTime;

        velocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetKeyDown(KeyCode.UpArrow) && onGround)
            velocity.y = jumpForce;

        Vector2 movement = velocity * Time.deltaTime;

        foreach (var wall in walls)
        {
            HitData hit = colliderBox.IsOverlapping(new Vector2(movement.x, 0), wall);

            if (hit != null)
            {
                movement.x -= Mathf.Sign(movement.x) * hit.overlap.x;
            }
        }

        onGround = false;
        foreach (var wall in walls)
        {
            HitData hit = colliderBox.IsOverlapping(new Vector2(movement.x, movement.y), wall);

            if (hit != null)
            {
                if (movement.y < 0)
                    onGround = true;

                movement.y -= Mathf.Sign(movement.y) * hit.overlap.y;
                velocity.y = 0;
            }
        }

        transform.Translate(movement);
    }
}