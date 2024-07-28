using UnityEngine;

public class TankShooterManager : MonoBehaviour
{
    public Vector2 bulletCenter;
    public float bulletRadius;
    public Vector2 groundCenter;
    public float groundRadius;

    private void OnDrawGizmos()
    {
        // Draw the main circle
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(bulletCenter, bulletRadius);

        // Draw the cutting circle
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCenter, groundRadius);

        // Calculate and draw the half-moon shape
        DrawHalfMoon();
    }

    private void DrawHalfMoon()
    {
        Vector2[] intersectionPoints = FindCircleIntersections(bulletCenter, bulletRadius, groundCenter, groundRadius);

        if (intersectionPoints == null || intersectionPoints.Length < 2)
        {
            Debug.LogError("No intersection points found.");
            return;
        }

        Vector2 p1 = intersectionPoints[0];
        Vector2 p2 = intersectionPoints[1];

        // Draw the arc of the main circle between the intersection points
        DrawArc(bulletCenter, bulletRadius, p1, p2, false);

        // Draw the arc of the cutting circle between the intersection points
        DrawArc(groundCenter, groundRadius, p1, p2, false);
    }

    private Vector2[] FindCircleIntersections(Vector2 c1, float r1, Vector2 c2, float r2)
    {
        float d = Vector2.Distance(c1, c2);

        if (d > r1 + r2 || d < Mathf.Abs(r1 - r2))
        {
            return null;
        }

        float a = (r1 * r1 - r2 * r2 + d * d) / (2 * d);
        float h = Mathf.Sqrt(r1 * r1 - a * a);

        Vector2 p0 = c1 + a * (c2 - c1) / d;

        Vector2 p1 = new Vector2(
            p0.x + h * (c2.y - c1.y) / d,
            p0.y - h * (c2.x - c1.x) / d
        );

        Vector2 p2 = new Vector2(
            p0.x - h * (c2.y - c1.y) / d,
            p0.y + h * (c2.x - c1.x) / d
        );

        return new Vector2[] { p1, p2 };
    }

    private void DrawArc(Vector2 center, float radius, Vector2 start, Vector2 end, bool clockwise)
    {
        float angleStart = Mathf.Atan2(start.y - center.y, start.x - center.x) * Mathf.Rad2Deg;
        float angleEnd = Mathf.Atan2(end.y - center.y, end.x - center.x) * Mathf.Rad2Deg;

        if (clockwise)
        {
            if (angleEnd > angleStart)
            {
                angleStart += 360;
            }
        }
        else
        {
            if (angleStart > angleEnd)
            {
                angleEnd += 360;
            }
        }

        for (float angle = angleStart; angle <= angleEnd; angle += 1.0f)
        {
            float rad = angle * Mathf.Deg2Rad;
            Vector3 point = new Vector3(
                center.x + radius * Mathf.Cos(rad),
                center.y + radius * Mathf.Sin(rad),
                0
            );
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}
