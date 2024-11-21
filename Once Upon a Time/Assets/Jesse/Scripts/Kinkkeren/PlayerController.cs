using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    private bool isShoot;
    private bool isCharging;

    private float forceMultiplier = 5f;
    private float chargeTime;
    private float maxChargeTime = 3f;

    private Vector3 shootDirection;

    public Slider chargeSlider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.drag = 0.1f;
        rb.angularDrag = 0.05f;

        chargeSlider.value = 0;
        chargeSlider.maxValue = maxChargeTime;
        chargeSlider.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdateShootDirection();

        if (Input.GetKeyDown(KeyCode.Space) && !isShoot)
        {
            isCharging = true;
            chargeTime = 0f;
            chargeSlider.gameObject.SetActive(true);
        }

        if (isCharging)
        {
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0, maxChargeTime);
            chargeSlider.value = chargeTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            Shoot(chargeTime);
            isCharging = false;
            chargeSlider.gameObject.SetActive(false);
        }
    }

    void UpdateShootDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            shootDirection = (hitPoint - transform.position).normalized;
        }
    }

    void Shoot(float chargeTime)
    {
        if (isShoot)
            return;

        float calculatedForce = chargeTime * forceMultiplier;

        rb.AddForce(shootDirection * calculatedForce, ForceMode.Impulse);
        isShoot = true;

        StartCoroutine(ResetBallVelocityAfterTime(5f));
    }

    IEnumerator ResetBallVelocityAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        isShoot = false;
    }
}
