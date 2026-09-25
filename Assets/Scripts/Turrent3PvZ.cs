using UnityEngine;

public class Turrent3PvZ : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float range = 8f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int bulletCount = 5;
    [SerializeField] private float spreadAngle = 30f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int circlePoints = 40;

    private bool playerWasInRange;

    void Update()
    {
        if (player == null)
            return;

        DrawRange();

        bool inRange = IsPlayerInRange();
        if (inRange && !playerWasInRange)
        {
            Shoot();
        }

        playerWasInRange = inRange;
    }

    bool IsPlayerInRange()
    {
        Vector3 direction = player.position - transform.position;
        return direction.magnitude <= range;
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null || player == null) return;

        Vector3 directionToPlayer = player.position - firePoint.position;

        directionToPlayer.y = 0f;
        directionToPlayer.Normalize();

        for (int i = 0; i < bulletCount; i++)
        {
            float angle;

            if (bulletCount == 1)
            {
                angle = 0f;
            }
            else
            {
                angle = Mathf.Lerp(-spreadAngle / 2f,spreadAngle / 2f,(float)i / (bulletCount - 1));
            }

            Vector3 direction = Quaternion.Euler(0f, angle, 0f) * directionToPlayer;

            GameObject bullet = Instantiate(bulletPrefab,firePoint.position,Quaternion.LookRotation(direction));

            Bullet2 bulletScript = bullet.GetComponent<Bullet2>();

            if (bulletScript != null)
            {
                bulletScript.SetDirection(direction);
            }
        }
    }

    void DrawRange()
        {
            if (lineRenderer == null) return;

            lineRenderer.positionCount = circlePoints + 1;

            Vector3 center = transform.position;
            center.y += 0.05f;

            for (int i = 0; i <= circlePoints; i++)
            {
                float angle = (360f / circlePoints) * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * range;
                float z = Mathf.Sin(angle * Mathf.Deg2Rad) * range;
                Vector3 point = center + new Vector3(x, 0f, z);
                lineRenderer.SetPosition(i, point);
            }
        }
}