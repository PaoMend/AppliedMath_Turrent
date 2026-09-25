using UnityEngine;

public class TurrentAim : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] private float rotSpeed = 5;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        var dir = target.position - this.transform.position;
        var angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, angle, 0), Time.deltaTime * rotSpeed);
    }
}
