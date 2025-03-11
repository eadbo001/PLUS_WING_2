using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    private Vector3 rotationDir;
    private float rotationSpeed;
    
    public void SetRandomRotation()
    {
        transform.localRotation = Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
    }

    private void OnEnable()
    {
        rotationDir = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        rotationSpeed = Random.Range(20f, 60f);
    }
    private void Update()
    {
        transform.Rotate(rotationDir, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
