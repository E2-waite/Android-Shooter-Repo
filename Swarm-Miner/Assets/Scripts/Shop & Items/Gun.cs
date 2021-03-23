using UnityEngine;

[System.Serializable]
public class Gun : Item
{
    public enum Type
    {
        pistol,
        machineGun,
        shotgun
    }
    public Type gunType;
    public int rounds;
    public float shotDelay, reloadTime, currReloadTime;
    public int damage;
    public int currRounds;
    float currShotDelay;
    bool reloading;

    public Gun()
    {
        currRounds = rounds;
        reloading = false;
    }

    public bool Shoot(Vector3 start, Vector3 forward, LayerMask mask, ref Vector3 hitPoint)
    {
        if (currShotDelay <= 0 && !reloading)
        {
            currRounds--;
            currShotDelay = shotDelay;
            if (currRounds <= 0)
            {
                reloading = true;
                currReloadTime = reloadTime;
            }

            RaycastHit hit;
            if (Physics.Raycast(start, forward, out hit, Mathf.Infinity, mask))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<Enemy>().Hit(damage);
                }
                if (hit.collider.CompareTag("Hive"))
                {
                    hit.collider.GetComponent<Hive>().Hit(damage);
                }
                if (hit.collider.CompareTag("HiveShield"))
                {
                    hit.collider.GetComponent<HiveShield>().Hit(damage);
                }
            }
            if (hit.collider == null)
            {
                hitPoint = start + (forward * 25);
            }
            else
            {
                hitPoint = hit.point;
            }

            return true;
        }
        return false;
    }

    public bool Reload()
    {
        if (reloading || currRounds == rounds)
        {
            return false;
        }
        reloading = true;
        currReloadTime = reloadTime;
        return true;
    }

    public bool IsReloading()
    {
        return reloading;
    }

    public void Cooldown()
    {
        if (currShotDelay > 0)
        {
            currShotDelay -= Time.deltaTime;
        }

        // Tick down reload time, unless at 0 - then checks if reloading then resets rounds if so
        if (currReloadTime > 0)
        {
            currReloadTime -= Time.deltaTime;
        }
        else
        {
            if (reloading)
            {
                reloading = false;
                currRounds = rounds;
            }
        }
    }
}
