using UnityEngine;

public class GunController : MonoBehaviour
{
    Gun currentGun;
    public Transform gunHolder;
    public Gun startingGun;
    private void Start()
    {
        if(startingGun != null)
        {
            SpownGun(startingGun);
        }
    }

    public void SpownGun(Gun _selectedGun)
    {
        if(currentGun != null)
        {
            Destroy(currentGun.gameObject);
        }
        currentGun = Instantiate(_selectedGun, gunHolder.position, gunHolder.rotation) as Gun;
        currentGun.transform.parent = gunHolder;
    }

    public void shoot()
    {
        currentGun.Shoot();
    }


}
