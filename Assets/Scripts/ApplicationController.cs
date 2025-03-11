using System.Collections.Generic;
using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    public List<GameObject> AsteroidPrefabs;
    public int AsteroidsAmount = 200;
    public float AsteroidXYPosRandomness = 2f;
    public float AsteroidsMinDistance = 100;
    public float AsteroidsMaxDistance = 600;

    public static float GlobalRotationOffset = 100f;

    private float asteroidSpacing => (AsteroidsMaxDistance-AsteroidsMinDistance) / AsteroidsAmount;

    private void Awake()
    {
        InitGame();
    }

    private void InitGame()
    {
        if (AsteroidPrefabs != null && AsteroidPrefabs.Count > 0)
            InstantiateAsteroids();
    }

    private void InstantiateAsteroids()
    {
        for (int i = 0; i < AsteroidsAmount; i++)
        {
            GameObject asteroid = GameObject.Instantiate<GameObject>(AsteroidPrefabs[Random.Range(0, AsteroidPrefabs.Count - 1)]);
            asteroid.transform.position = new Vector3(
                Random.Range(-AsteroidXYPosRandomness, AsteroidXYPosRandomness),
                Random.Range(-AsteroidXYPosRandomness, AsteroidXYPosRandomness), 
                i * asteroidSpacing + AsteroidsMinDistance);
            asteroid.AddComponent<SimpleRotate>().SetRandomRotation();
            asteroid.transform.Translate(transform.right * ApplicationController.GlobalRotationOffset);

            Rigidbody asteroidRB = asteroid.AddComponent<Rigidbody>();
            asteroidRB.useGravity = false;
            asteroidRB.isKinematic = true;

            asteroid.AddComponent<Asteroid>();
        }
    }
}
