using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    //[SerializeField] private GameObject effect;
    [SerializeField] private float points;
    [SerializeField] private Score score;
    public GameManager gameManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            score.AddScore(points);
            if (tag == "Enemy")
            {
                gameManager.UpdateEnemyList(gameObject);
                AudioManager.Instance.PlaySFX("Pop");
                Destroy(gameObject);
                //Instantiate(effect, transform.position, Quaternion.identity);
            }
        }
    }

    private void OnBecameInvisible()
    {
        score.AddScore(points);
        if (tag == "Enemy")
        {
            gameManager.UpdateEnemyList(gameObject);
            AudioManager.Instance.PlaySFX("Pop");
            Destroy(gameObject);
        }
    }
}
