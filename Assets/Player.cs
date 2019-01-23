using UnityEngine;

public class Player : MonoBehaviour
{
    float moveSpeed = 4f;
    Vector2 velocity;

    ColliderBox[] walls;
    ColliderBox colliderBox;

    SpriteRenderer sprite;

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
        velocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        velocity.y = Input.GetAxisRaw("Vertical") * moveSpeed;

        Vector2 movement = velocity * Time.deltaTime;

        foreach (var wall in walls)
        {
            HitData hit = colliderBox.IsOverlapping(new Vector2(movement.x, 0), wall);

            if (hit != null)
            {
                movement.x -= Mathf.Sign(movement.x) * hit.overlap.x;
            }
        }

        foreach (var wall in walls)
        {
            HitData hit = colliderBox.IsOverlapping(new Vector2(movement.x, movement.y), wall);

            if (hit != null)
            {
                movement.y -= Mathf.Sign(movement.y) * hit.overlap.y;
            }
        }

        transform.Translate(movement);
    }
}