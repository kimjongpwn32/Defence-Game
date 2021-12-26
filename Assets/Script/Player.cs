using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Configuration parameters
    //Start is called before the first frame update
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    float xMin;
    float xMax;
    bool isFiring = false;

    void Start()
    {
        SetUpMoveBoundaries();
    }

    IEnumerator FireContnuously()
    {
        while (isFiring)
        {
            //No rotation
            //object,position,rotation
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
        yield return 0;

    }

    public void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isFiring = true;
            StartCoroutine(FireContnuously());
        } 
        if (Input.GetButtonUp("Fire1"))
        {
            isFiring = false;
        }
    }

    private void Move()
    {
        //frame rate independant
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax); 
        
        transform.position = new Vector2(newXPos, transform.position.y);

    }
}
