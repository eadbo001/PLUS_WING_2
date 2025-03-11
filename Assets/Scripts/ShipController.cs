using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameObject ShieldObject;
    public Transform CameraAndModelTransform;
    public List<Transform> LaserPositions;
    public GameObject LaserHitExplosionEffectPrefab;
    public AudioClip AudioClipLaser;
    public AudioClip AudioClipInterior;

    public float ShipSpeed = 30f;
    public float ShipCounterOffset = 10f;
    public float RotationAngleSpeed = 5f;
    public float HorizontalInputLerpSpeed = 5f;
    private float horizontalInputValue = 0f;
    private float inputTargetValue = 0f;
    private float shieldTextOffsetSpeed = .5f;

    private bool leftRightLasersShooting = true;
    private bool shieldsActive = false;
    
    private Material shieldMaterial => ShieldObject.GetComponent<Renderer>().material;
    private AudioSource audioSource => GetComponent<AudioSource>();
    private ShipShields shipShields => GetComponentInChildren<ShipShields>();

    private void Awake()
    {
        //Callback for shields collision
        shipShields.Init(HandleShieldsCollision);

        ShieldObject.SetActive(false);
        CameraAndModelTransform.transform.position = new Vector3(0, ApplicationController.GlobalRotationOffset - ShipCounterOffset, 0);
        InputController.Instance.OnKeyAxisXY += keyXY => { horizontalInputValue = keyXY.x; };
        InputController.Instance.OnFireDown += HandleFireButton;
    }

    private void HandleFireButton(bool isDown)
    {
        if (isDown)
        {
            audioSource.pitch = Random.Range(.9f, 1.1f);
            audioSource.PlayOneShot(AudioClipLaser);

            GameObject laserObject1 = LaserFactory.GetLaserObject(2f, 150f, LaserHitExplosionEffectPrefab);
            GameObject laserObject2 = LaserFactory.GetLaserObject(2f, 150f, LaserHitExplosionEffectPrefab);

            laserObject1.transform.position = leftRightLasersShooting ? LaserPositions[0].position : LaserPositions[2].position;
            laserObject2.transform.position = leftRightLasersShooting ? LaserPositions[1].position : LaserPositions[3].position;

            leftRightLasersShooting = !leftRightLasersShooting;
        }
    }

    private void Update()
    {
        inputTargetValue = Mathf.Lerp(inputTargetValue, horizontalInputValue, Time.deltaTime * HorizontalInputLerpSpeed);
        transform.Rotate(Vector3.forward, RotationAngleSpeed * inputTargetValue * Time.deltaTime, Space.World);

        transform.Translate(transform.forward * ShipSpeed * Time.deltaTime);
    }

    private void HandleShieldsCollision(Collider collision)
    {
        if (!shieldsActive)
        {
            shieldsActive = true;
            StartCoroutine(ShieldEffect());
        }
    }

    private IEnumerator ShieldEffect()
    {
        ShieldObject.gameObject.SetActive(true);
        float startTime = Time.time;        
        Vector2 shieldTextOffset = Vector2.zero;

        while (Time.time - startTime < .5f)
        {
            shieldTextOffset.x += Time.deltaTime * shieldTextOffsetSpeed;            
            shieldMaterial.SetTextureOffset("_BaseMap", shieldTextOffset);
            yield return null;
        }
        
        ShieldObject.gameObject.SetActive(false);
        shieldsActive = false;
    }
}
