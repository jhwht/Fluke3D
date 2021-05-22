using System;
using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _fireRate = 0.1f;
    [SerializeField] private float _damage = 1;
    [SerializeField] private float _range = 500f;
    [SerializeField] private SpriteRenderer _gun;

    private bool _isFiring;
    private bool _canFire;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _canFire = true;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_isFiring && _canFire)
        {
            StartCoroutine(Fire());
        }
    }

    public void OnFirePressed(bool isFiring)
    {
        _isFiring = isFiring;
    }

    private IEnumerator Fire()
    {
        _canFire = false;
        var ray = _camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        TryHitEnvironment(ray);
        _animator.Play("GunFire");
        _audioSource.Play();

        yield return new WaitForSeconds(_fireRate);
        _canFire = true;
    }

    private bool TryHitEnvironment(Ray ray)
    {
        if (Physics.Raycast(ray, out var hitInfo, _range, LayerMask.GetMask("Environment")) == false)
        {
            return false;
        }

        var shootable = hitInfo.collider.GetComponent<IShootable>();

        if (shootable != null)
        {
            shootable.TakeShot(hitInfo.point, _damage);
            print("damage");
        }
        else
        {
            // Hit environment
        }

        return true;
    }
}