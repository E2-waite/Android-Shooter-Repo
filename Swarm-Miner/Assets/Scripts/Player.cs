using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum EquipSlot
    {
        miningLazer,
        gun
    }

    public EquipSlot currentSlot = EquipSlot.miningLazer;

    public int gems = 0, maxGems = 5, credits = 0;
    Rigidbody rb;
    public int maxLives = 3, currLives;
    public float speed = 10;
    float moveX, moveZ;
    public Vector3 shootVec;
    public LineRenderer line;
    public GameObject lazerPrefab, trailPrefab;
    GameObject lazer;
    public List<GameObject> childLazers = new List<GameObject>();
    public Gun currGun;
    public LayerMask mask;
    public Inventory inventory;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currLives = maxLives;


        UIController.Instance.healthBar.UpdateHealthBar(currLives, maxLives);
        inventory = new Inventory();
        inventory.AddItem(ItemController.Instance.guns[0]);
        currGun = inventory.weapons[inventory.gunInd];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShopController.Instance.ToggleShop();
        }

        if (!LevelController.Instance.paused)
        {
            LookAt();
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
            Move();
            UIController.Instance.currency.UpdateText(gems, maxGems, credits);
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentSlot = EquipSlot.miningLazer;
                UIController.Instance.SwitchWeaponUI(currentSlot);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentSlot = EquipSlot.gun;
                UIController.Instance.SwitchWeaponUI(currentSlot);
            }

            if (currentSlot == EquipSlot.miningLazer)
            {
                if (Input.GetMouseButton(0))
                {
                    if (lazer == null)
                    {
                        lazer = Instantiate(lazerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    }
                    lazer.GetComponent<Lazer>().Out(transform.position, transform.forward);
                }
                else
                {
                    ClearLazers();
                }
            }
            else if (currentSlot == EquipSlot.gun)
            {
                ClearLazers();

                if (Input.GetMouseButton(0))
                {
                    Vector3 hitPoint = new Vector3(0, 0, 0);
                    if (currGun.Shoot(transform.position, transform.forward, mask, ref hitPoint))
                    {
                        GameObject trail = Instantiate(trailPrefab, transform.position, Quaternion.identity);
                        trail.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                        trail.GetComponent<LineRenderer>().SetPosition(1, hitPoint);
                        StartCoroutine(ClearTrail(trail));
                    }
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    currGun.Reload();
                }
            }
            currGun.Cooldown();
        }
    }

    IEnumerator ClearTrail(GameObject trail)
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(trail);
    }

    void ClearLazers()
    {
        if (lazer != null)
        {
            // If all children are destroyed - destroy base lazer
            if (lazer.GetComponent<Lazer>().ShutDownChild())
            {
                Destroy(lazer);
            }
        }
    }

    void Move()
    {
        var dir = new Vector3(moveX * speed, 0, moveZ * speed);
        rb.AddForce(dir);
    }

    void LookAt() // Look at mouse
    {
        var v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);

        var lookPos = v3 - transform.position;
        lookPos.y = 0;

        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
    }

    bool hitCooldown = false;

    public void Hit() // Take damage when player is hit
    {
        if (!hitCooldown)
        {
            hitCooldown = true;
            currLives--;
            if (currLives <= 0)
            {
                ClearLazers();
                Destroy(gameObject);
            }
            UIController.Instance.healthBar.UpdateHealthBar(currLives, maxLives);
            StartCoroutine(HitCooldown());
        }
    }

    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(0.05f);
        hitCooldown = false;
    }

    public bool Heal()
    {
        if (currLives < maxLives)
        {
            currLives++;
            UIController.Instance.healthBar.UpdateHealthBar(currLives, maxLives);
            return true;
        }
        return false;
    }

    public bool PickupGem()
    {
        if (gems < maxGems)
        {
            gems++;
            return true;
        }
        return false;
    }
}
 