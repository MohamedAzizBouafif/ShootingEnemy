using UnityEngine;

public class bullet : MonoBehaviour
{
    float speed = 10f;

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, 1.5f);
    }

}
