using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;
    public CharacterManager characterManager;
    GameObject birdPrefab;
    Rigidbody2D bird;
    Collider2D birdCollider;

    public Vector3 currentPosition;

    public float maxLength;
    public float bottomBoundary;

    bool isMouseDown;
    public bool isBirdOnSlingshot;

    public float birdPositionOffSet;
    public float force;

    public int lives;

    private void Awake()
    {
        isBirdOnSlingshot = true;
        lives = 3;
    }

    private void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
    }

    public void CreateBird()
    {
        if (lives > 0)
        {
            birdPrefab = characterManager.birdPrefab;

            bird = Instantiate(birdPrefab).GetComponent<Rigidbody2D>();
            birdCollider = bird.GetComponent<Collider2D>();
            birdCollider.enabled = false;

            bird.isKinematic = true;
            isBirdOnSlingshot = true;
            lives--;
        }
        else
        { 
            lives--;
        }
    }

    IEnumerator DestroyBird(Rigidbody2D bird, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(bird.gameObject);
    }

    private void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);

            currentPosition = ClampBoundary(currentPosition);

            SetStrips(currentPosition);

            if (birdCollider)
            {
                birdCollider.enabled = true;
            }
        }
        else
        {
            ResetStrips();
        }
    }

    void Shoot()
    {
        isBirdOnSlingshot = false;
        bird.isKinematic = false;
        Vector3 birdForce = (currentPosition - center.position) * force * -1;
        bird.velocity = birdForce;

        StartCoroutine(DestroyBird(bird, 7f));
        bird = null;
        birdCollider = null;
        Invoke("CreateBird", 2);
    }

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (bird)
        { 
            Vector3 dir = position - center.position;
            bird.transform.position = position + dir.normalized * birdPositionOffSet;
            bird.transform.right = -dir.normalized;
        }
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }
}
