using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pistol : Gun
{

    public int pMagSize;
    public int pMaxAmmo;
    public int pPelletCount;
    public float pDamage;
    public float pFireRate;
    public float pRange;
    public float pPelletSpread;
    public bool pHitScan;
    public bool pFullAuto;
    public bool pBurstShot;
    public GameObject pAmmoType;
    public GameObject pMesh;
    public Transform pBulletOrigin;

    public Pistol(Pistol gun)
    {
        Ammo = gun.pMagSize;
        MagSize = gun.pMagSize;
        MaxAmmo = gun.pMaxAmmo;
        PelletCount = gun.pPelletCount;
        Damage = gun.pDamage;
        FireRate  = gun.pFireRate;
        Range = gun.pRange;
        PelletSpread = gun.pPelletSpread;
        HitScan = gun.pHitScan;
        FullAuto = gun.pFullAuto;
        BurstShot = gun.pBurstShot;
        AmmoType = gun.pAmmoType;
        GunMesh = gun.pMesh;
        BulletOrigin = gun.pBulletOrigin;
    }

    void OnTriggerEnter(Collider col)
    {
        Pistol p = new Pistol(gameObject.GetComponent<Pistol>());
        PickUpGun(col, p);
    }
}
