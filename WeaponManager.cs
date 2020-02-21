using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int weaponPos = 0;
    public List<Gun> weapons;
    public Rigidbody player;

    private Gun curWeapon;
    private Animator anim;
    private GameManager gM;

    void Start()
    {
        gM = GameManager.gManInstance;
    }

    void Update()
    {
        if (Convert.ToBoolean(Input.GetAxis("SwitchWeapon")))
        {
            weaponPos += Mathf.Clamp((int)Input.GetAxis("SwitchWeapon"), -1, 1);
            weaponPos = Mathf.Clamp(weaponPos, 0, weapons.Count - 1);

            SwitchWeapon(weapons[weaponPos]);
        }

        if (Input.GetButtonDown("Shoot"))
        {
            if (curWeapon.Ammo > 0 && !anim.GetBool("_Reload"))
            {
                FireBullet();
            }
        }

        if (Input.GetButtonDown("Reload"))
        {
            Reload();
        }

        if (player.velocity.magnitude != 0)
        {
            anim.SetFloat("_Speed", player.velocity.magnitude);
        }
    }

    private void FireBullet()
    {
        anim.SetInteger("_Ammo", curWeapon.Ammo);
        anim.SetTrigger("_FireTrigger");

        String caseTup = $"{curWeapon.HitScan}, {curWeapon.BurstShot}";

        switch (caseTup)
        {
            //Normal Hitscan
            case ("True, False"):
                BulletHitScan(curWeapon.BulletOrigin.forward);
                break;

            //Shotgun (Hitscan)
            case ("True, True"):
                BurstFire();
                break;

            //Shotgun (No Hitscan)
            case ("False, True"):
                break;

            //Other
            case ("False, False"):
                break;
        }

        curWeapon.Ammo--;
    }

    private void Reload()
    {
        if (curWeapon.Ammo < curWeapon.MagSize)
        {
            curWeapon.MaxAmmo -= (curWeapon.MaxAmmo - curWeapon.Ammo);
            curWeapon.Ammo = curWeapon.MagSize;

            anim.SetBool("_Reload", true);
            anim.SetInteger("_Ammo", curWeapon.Ammo);
        }
    }

    public void BulletHitScan(Vector3 dir)
    {
        RaycastHit rHit = new RaycastHit();

        if (Physics.Raycast(curWeapon.BulletOrigin.position, dir, out rHit, curWeapon.Range))
        {
            if (rHit.transform.tag == "Enemy" && rHit.transform.GetComponent<Basic_HP>() != null)
            {
                rHit.transform.GetComponent<Basic_HP>().hp -= curWeapon.Damage;
                Debug.DrawLine(curWeapon.BulletOrigin.position, rHit.point, Color.red, 5f);

                return;
            }

            Debug.DrawLine(curWeapon.BulletOrigin.position, rHit.point, Color.white, 5f);
        }
        else
        {
            Debug.DrawRay(curWeapon.BulletOrigin.position, dir * 200, Color.white, 20f);
        }

    }

    void BurstFire()
    {
        Vector3 posOrigin = curWeapon.BulletOrigin.forward;

        for (int i = 0; i < curWeapon.PelletCount; i++)
        {
            float r1 = UnityEngine.Random.Range(-curWeapon.PelletSpread, curWeapon.PelletSpread);
            float r2 = UnityEngine.Random.Range(-curWeapon.PelletSpread, curWeapon.PelletSpread);
            float r3 = UnityEngine.Random.Range(-curWeapon.PelletSpread, curWeapon.PelletSpread);

            Vector3 posTarget = new Vector3(posOrigin.x + r1, posOrigin.y + r2, posOrigin.z + r3);

            BulletHitScan((posOrigin + posTarget).normalized);
        }
    }

    public void AddWeapon(Gun g)
    {
        switch (g)
        {
            case Pistol p:
                weapons.Add(g);
                SwitchWeapon(g);
                break;
        }
    }

    public void SwitchWeapon(Gun gun)
    {
        foreach (Gun g in weapons)
        {
            g.GunMesh.SetActive(false);

            if (g.GunMesh == gun.GunMesh)
            {
                g.GunMesh.SetActive(true);
                curWeapon = g;
                anim = g.GunMesh.GetComponentInChildren<Animator>();
            }
        }
    }
}