using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int id = 0;
    bool fire = false;

    public Animator animator;
    public GameObject crossHair;

    public GameObject arrowPrefab;

    private void Start()
    {
        Debug.Log("HellO");
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("MoveHorizontal"), Input.GetAxis("MoveVertical"), 0.0f);
        Vector3 aim = new Vector3(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"), 0.0f);

        AimAndShoot(aim);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + movement * Time.deltaTime;
    }

    private void AimAndShoot(Vector2 aim)
    { 
        Vector2 shootingDirection = new Vector2(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"));
        
        if (aim.magnitude > 0.0f)
        {
            aim.Normalize();
            aim *= 0.4f;
            crossHair.transform.localPosition = aim;
            crossHair.SetActive(true);

            shootingDirection.Normalize();
            if (Input.GetAxis("Fire") > 0.3f && !fire)
            {
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * 3.0f;
                arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                fire = true;
                Destroy(arrow, 2);
            }

            if (Input.GetAxis("Fire") == 0.0)
            {
                fire = false;
            }

        } else
        {
            crossHair.SetActive(false);
        }
    }
}
