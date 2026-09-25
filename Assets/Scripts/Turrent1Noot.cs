using UnityEngine;

public class Turrent1Noot : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float range = 8f;
    [SerializeField] private float coneAngle = 45f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int conePoints = 20;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    private float fireTimer;

    void Start()
    {
        DrawCone();
    }

    void Update()
    {
        if (player == null)
            return;

        DrawCone();

        if (IsInCone(transform, player, range, coneAngle))
        {
            fireTimer += Time.deltaTime;

            if (fireTimer >= fireRate)
            {
                Shoot();
                fireTimer = 0f;
            }
        }
        else
        {
            fireTimer = 0f;
        }
    }

    bool IsInCone(
        Transform turret,
        Transform player,
        float range,
        float coneAngle)
    {

        Vector3 flatDirection = player.position - turret.position;
        flatDirection.y = 0f;
        Vector2 dir = new Vector2(flatDirection.x,flatDirection.z);

        if (dir.magnitude > range)
            return false;

        float pAngle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        float tAngle = turret.eulerAngles.y;
        float delta = Mathf.Abs(Mathf.DeltaAngle(tAngle,pAngle));

        return delta <= coneAngle / 2f;
    }

    void DrawCone()
    {
        if (lineRenderer == null)
            return;

        lineRenderer.positionCount = conePoints + 2;

        Vector3 origin = transform.position;
        origin.y += 0.05f;

        lineRenderer.SetPosition(0, origin);

        for (int i = 0; i <= conePoints; i++)
        {
            float angle = -coneAngle / 2f + (coneAngle / conePoints) * i;

            Vector3 direction = Quaternion.Euler(0f, angle, 0f) * transform.forward;

            Vector3 point = origin + direction * range;

            lineRenderer.SetPosition(i + 1,point);
        }

        lineRenderer.SetPosition(conePoints + 1,origin);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,firePoint.position,Quaternion.identity);

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetTarget(player);
        }
    }
}
