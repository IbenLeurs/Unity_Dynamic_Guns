using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public abstract class Gun : MonoBehaviour
{
    private int ammo;
    private int magSize;
    private int maxAmmo;
    private int pelletCount;
    private float damage;
    private float fireRate;
    private float range;
    private float pelletSpread; 
    private bool hitScan;
    private bool fullAuto;
    private bool burstShot;
    private GameObject ammoType;
    private GameObject gunMesh;
    private Transform bulletOrigin;

    public virtual void FireBullet(bool semiAuto, bool hitScan, GameObject projectile, Transform bulletOrigin)
    {
        if (semiAuto)
        {
            ReduceAmmo();
        }
    }

    void ReduceAmmo()
    {
        if (ammo > 0)
        {
            ammo--;
        }
    }

    void Reload()
    {
        if (ammo >= 0 && ammo < magSize)
        {
            maxAmmo -= (magSize - ammo);
            ammo = magSize;
        }
    }

    public void PickUpGun(Collider col, Gun gun)
    {
        if (col.tag == "Player")
        {
            WeaponManager wm = col.GetComponent<WeaponManager>();
            wm.AddWeapon(gun);
            wm.SwitchWeapon(gun);
            Destroy(gameObject);
        }
    }

    public int Ammo
    {
        get { return ammo; }
        set { ammo = value; }
    }

    public int MagSize
    {
        get { return magSize; }
        set { magSize = value; }
    }

    public int MaxAmmo
    {
        get { return maxAmmo; }
        set { maxAmmo = value; }
    }

    public int PelletCount
    {
        get { return pelletCount; }
        set { pelletCount = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float FireRate
    {
        get { return fireRate; }
        set { fireRate = value; }
    }

    public float Range
    {
        get { return range; }
        set { range = value; }
    }

    public float PelletSpread
    {
        get { return pelletSpread; }
        set { pelletSpread = value; }
    }

    public bool HitScan
    {
        get { return hitScan; }
        set { hitScan = value; }
    }

    public bool FullAuto
    {
        get { return fullAuto; }
        set { fullAuto = value; }
    }

    public bool BurstShot
    {
        get { return burstShot; }
        set { burstShot = value; }
    }

    public GameObject AmmoType
    {
        get { return ammoType; }
        set { ammoType = value; }
    }

    public GameObject GunMesh
    {
        get { return gunMesh; }
        set { gunMesh = value; }
    }

    public Transform BulletOrigin
    {
        get { return bulletOrigin; }
        set { bulletOrigin = value; }
    }
}