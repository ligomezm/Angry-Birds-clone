using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float fieldOfImpact;
    public float force;
    public LayerMask layerToHit;
    public GameObject explosionEffect;
    public Slingshot slingshot;

    private void Start()
    {
        slingshot = GameObject.Find("Slingshot").GetComponent<Slingshot>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !slingshot.isBirdOnSlignshot)
        {
            Explode();
        }
    }


    void Explode()
    {
        AudioManager.Instance.PlaySFX("Explosion");
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToHit);

        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);

            if (obj.gameObject.tag == "Enemy")
            {
                AudioManager.Instance.PlaySFX("Pop");
                Destroy(obj);
            }
        }

        GameObject explosionEffectInst = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(explosionEffectInst, 1);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
