using System;
using UnityEngine;

public class ShipShields : MonoBehaviour
{
    private Action<Collider> collisionCallback;    
    public void Init(Action<Collider> collisionCallback)
    {
        this.collisionCallback = collisionCallback;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Asteroid>())        
            collisionCallback?.Invoke(collision);
    }
}
