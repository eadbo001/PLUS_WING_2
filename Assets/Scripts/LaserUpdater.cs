using UnityEngine;

public class LaserUpdater : MonoBehaviour
{
    private LineRenderer lineRenderer => GetComponent<LineRenderer>();

    private float lineLength = 1f;
    private float laserSpeed = 10f;

    private GameObject particleEffectGO;

    private bool isDestroyed = false;

    public void Init(float length, float speed, GameObject particleEffect)
    {
        lineLength = length;
        laserSpeed = speed;
        particleEffectGO = particleEffect;
    }
    private void Update()
    {
        transform.position += transform.forward * laserSpeed * Time.deltaTime;
        lineRenderer.SetPosition(0,transform.position);
        lineRenderer.SetPosition(1, transform.position + Vector3.forward * lineLength);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDestroyed)
            return;
        else
        {
            Asteroid asteroid = other.gameObject.GetComponent<Asteroid>();
            if (asteroid != null)
            {
                isDestroyed = true;
                asteroid.gameObject.GetComponent<MeshRenderer>().enabled = false;
                asteroid.gameObject.GetComponent<Collider>().enabled = false;

                for (int i = 0; i < 4; i++)
                    InstantiateSmallVersion(other.gameObject);

                GameObject explosionEffectGO = Instantiate<GameObject>(particleEffectGO, other.transform.position, Quaternion.identity);
                Destroy(explosionEffectGO, 6f);
            }
        }
    }

    private void InstantiateSmallVersion(GameObject asteroidGO)
    {
        GameObject smallAsteroid = Instantiate<GameObject>(asteroidGO, asteroidGO.transform.position, Quaternion.identity);
        smallAsteroid.GetComponent<MeshRenderer>().enabled = true;
        smallAsteroid.GetComponent<Collider>().enabled = true;
        smallAsteroid.transform.localScale *= .25f;
        Rigidbody rb = smallAsteroid.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
    }
}
