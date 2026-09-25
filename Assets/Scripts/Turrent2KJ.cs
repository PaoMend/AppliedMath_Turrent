using UnityEngine;

public class Turrent2KJ : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float range = 15f;
    [SerializeField] private float sightTolerance = 0.98f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private float fireTimer;
    private bool playerWasInSight;

    void Update()
    {
        if (player == null) return;

        DrawSightLine();

        bool inSights = IsPlayerInSight();

        if (inSights && !playerWasInSight)
        {
            Shoot();
        }

        playerWasInSight = inSights;
    }

    bool IsPlayerInSight()
    {
        Vector3 direction3D = player.position - transform.position;

        if (direction3D.magnitude > range)
            return false;

        direction3D.Normalize();

        Vector3 forward3D = transform.forward;
        forward3D.y = 0f;
        forward3D.Normalize();

        Vector2 forward = new Vector2(forward3D.x,forward3D.z);
        Vector2 toPlayer = new Vector2(direction3D.x,direction3D.z);

        float dot = Vector2.Dot(forward,toPlayer);

        return dot >= sightTolerance;
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
            return;

        GameObject bullet = Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);
        Bullet bulletScript =bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetTarget(player);
        }
    }

    void DrawSightLine()
    {
        if (lineRenderer == null) return;

        Vector3 start = transform.position;
        start.y += 0.05f;

        Vector3 end = start + transform.forward * range;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}