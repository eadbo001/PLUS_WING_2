using UnityEngine;

public static class LaserFactory
{
    private static Material laserMaterial => Resources.Load<Material>("Laser");
    public static GameObject GetLaserObject(float length, float speed, GameObject particleEffect)
    {
        
        GameObject laserObject = new GameObject("laser");

        LineRenderer lineRenderer = laserObject.AddComponent<LineRenderer>();
        lineRenderer.material = laserMaterial;
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = 2;

        laserObject.AddComponent<LaserUpdater>().Init(length, speed, particleEffect);
        CapsuleCollider capsuleCollider = laserObject.AddComponent<CapsuleCollider>();
        capsuleCollider.isTrigger = true;
        capsuleCollider.direction = 2;
        capsuleCollider.radius = 0.1f;
        capsuleCollider.height = 2f;
        capsuleCollider.center = new Vector3(0, 0, 1f);

        return laserObject;
    }
}
