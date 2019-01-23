using UnityEngine;

public class HitData
{
    public Vector2 overlap;
}

public class ColliderBox : MonoBehaviour
{
    public float width, height;

    public HitData IsOverlapping(Vector2 offset, ColliderBox other)
    {
        if (
            offset.x +
            this.transform.position.x - this.width / 2 <
            other.transform.position.x + other.width / 2 &&
            offset.x + 
            this.transform.position.x + this.width / 2 >
            other.transform.position.x - other.width / 2 &&
            offset.y +
            this.transform.position.y - this.height / 2 <
            other.transform.position.y + other.height / 2 &&
            offset.y +
            this.transform.position.y + this.height / 2 >
            other.transform.position.y - other.height / 2
        )
        {
            HitData hit = new HitData
            {
                overlap = new Vector2(
                    (this.width / 2 + other.width / 2) -
                    Mathf.Abs((this.transform.position.x + offset.x) - other.transform.position.x),
                    (this.height / 2 + other.height / 2) -
                    Mathf.Abs((this.transform.position.y + offset.y) - other.transform.position.y)
                )
            };
            return hit;
        }
        return null;
    }
}

