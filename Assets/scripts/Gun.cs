using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzel;
    public bullet bulletPrefab;
    public float muzzelVelocity = 35f;
    public float msBetweenShots = 100f;

    float nextShootTime = 0f;

    public void Shoot()
    {
        if(Time.time >= nextShootTime)
        {
            nextShootTime = Time.time + msBetweenShots / 1000;
            bullet newBullet = Instantiate(bulletPrefab, muzzel.position, muzzel.rotation) as bullet;
            newBullet.setSpeed(muzzelVelocity);
        }
    }
}
