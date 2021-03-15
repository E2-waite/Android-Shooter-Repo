using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    LineRenderer line;
    GameObject child;
    public void Out(Vector3 startPos, Vector3 dir)
    {
        if (line == null)
        {
            line = GetComponent<LineRenderer>();
        }

        line.SetPosition(0, startPos);

        RaycastHit hit;

        if (Physics.Raycast(startPos, dir, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Mirror"))
            {
                // Set child to newly generated (reflected) lazer if mirror is hit
                GameObject obj = hit.collider.gameObject.GetComponent<Mirror>().Reflect(gameObject, hit.normal, 100);

                if (child != null && obj != child)
                {
                    ShutDownChild();
                }
                child = obj;
            }
            else
            {
                ShutDownChild();
            }

            // If lazer hits an enemy - deal damage to it
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Enemy>().Hit(500);
            }

            if (hit.collider.gameObject.CompareTag("Dirt"))
            {
                hit.collider.gameObject.GetComponent<Block>().Damage(200 * Time.deltaTime);
            }

            line.SetPosition(1, hit.point);
        }
        else
        {
            ShutDownChild();
            line.SetPosition(1, startPos + (dir * 25));
        }
    }

    public bool ShutDownChild()
    {
        // Checks if child is waiting for it's child to be shut down - if not child is destroyed
        if (child != null)
        {
            if (child.GetComponent<Lazer>().ShutDownChild())
            {
                Destroy(child.gameObject);
                return true;
            }
        }
        return true;
    }
}
