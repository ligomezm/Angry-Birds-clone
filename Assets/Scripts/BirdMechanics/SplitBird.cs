using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBird : MonoBehaviour
{
    [SerializeField] private float spawnRadius;
    [SerializeField] private float spawnScale;
    [SerializeField] private float spawnForce;
    [SerializeField] private float deactivateTime;

    private bool canSplit = true;
    private Rigidbody2D rb;

    public GameObject prefab;
    public Slingshot slingshot;

    private void Start()
    {
        slingshot = GameObject.Find("Slingshot").GetComponent<Slingshot>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !slingshot.isBirdOnSlingshot && canSplit && gameObject.tag == "Player")
        {
            SpawnObjects();
            rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            canSplit = false;
        }
    }


    private void FixedUpdate()
    {
        if (rb != null && rb.velocity.magnitude < 0.1f)
        {
            rb.isKinematic = false;
        }
    }


    private void SpawnObjects()
    {
        AudioManager.Instance.PlaySFX("Split");

        for (int i = 0; i < 3; i++)
        {
            Vector2 spawnPos = (Vector2)transform.position + new Vector2(Random.Range(0f, spawnRadius), Random.Range(-spawnRadius, spawnRadius));

            spawnPos.x = Mathf.Max(spawnPos.x, Camera.main.transform.position.x);

            GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
            obj.transform.localScale = Vector3.one * spawnScale;

            Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
            objRb.AddForce(Random.insideUnitCircle * spawnForce * 3, ForceMode2D.Impulse);
            obj.tag = "PlayerInstance";

            Destroy(obj, 5f);
        }
    }
}
