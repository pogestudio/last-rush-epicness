using UnityEngine;
using System.Collections;

public class AbstractEnemy : MonoBehaviour
{
    public GameObject bloodSpurtEffect;
    private GameObject[] effects;

    public int health;
    public float defaultMovingSpeed;
    float _movingSpeed;
    public float movingSpeed
    {
        get { return _movingSpeed; }
        set { _movingSpeed = Mathf.Max(value, 0); }
    }
    public GameObject target;
    public float searchRadius = 80F;

    private void die()
    {
        networkView.RPC("dieRPC", RPCMode.All);
    }


    [RPC]
    private void dieRPC()
    {
        //Debug.Log ("Monster should die");
        DropsLoot lootComponent = gameObject.GetComponent<DropsLoot>();
        lootComponent.ShouldDropLoot();

        if (bloodSpurtEffect)
        {
            for (int i = 0; i < effects.Length; i++)
            {
                if (!effects[i])
                {
                    Destroy(effects[i]);
                    effects[i] = null;
                }
            }
        }
        Destroy(gameObject);
    }


    public void applyDamage(int damage)
    {
        Vector3 shotDir = transform.position - target.transform.position;   //TODO replace with BULLET direction (not related to target or whatever)
        networkView.RPC("applyDamageRPC", RPCMode.All, damage, shotDir);
    }

    [RPC]
    private void applyDamageRPC(int damage, Vector3 shotDir)
    {

        //real damage
        if (networkView.isMine)
        {
            health -= damage;


            if (health <= 0)
            {
                die();
            }
        }

        //effect
        if (bloodSpurtEffect)
        {
            for (int i = 0; i < effects.Length; i++)
            {
                if (!effects[i])
                {
                    effects[i] = Instantiate(bloodSpurtEffect, transform.position, Quaternion.LookRotation(shotDir)) as GameObject;
                    Destroy(effects[i], 1.0f);
                    break;
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        movingSpeed = defaultMovingSpeed; // set the moving speed we have from editor. 
        if (bloodSpurtEffect)
            effects = new GameObject[4];
        if (!target)
        {
            Debug.Log("Does not have initial target");
        }
    }

    protected void walk()
    {
        if (!networkView.isMine)
            Debug.LogWarning("walk is called, shouldnt be since this object is a replicate");

        Vector3 delta = target.transform.position - transform.position;
        delta.Normalize();
        float moveSpeed = this.movingSpeed * Time.deltaTime;
        //transform.position = transform.position + (delta * moveSpeed);
        rigidbody.AddForce(delta * moveSpeed, ForceMode.Acceleration);
        transform.LookAt(target.transform);
    }
}

