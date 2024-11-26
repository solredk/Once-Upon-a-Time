using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public bool isShoot;
    private bool isCharging;

    private float forceMultiplier = 5f;
    private float chargeTime;
    private float maxChargeTime = 3f;

    private float rotationAngle = 0f;
    private float rotationSpeed = 90f;

    private Vector3 shootDirection;

    public Slider chargeSlider;
    public LineRenderer lineRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //chargeSlider = FindAnyObjectByType<Slider>();

        rb.drag = 0.1f;
        rb.angularDrag = 0.05f;

        chargeSlider.value = 0;
        chargeSlider.maxValue = maxChargeTime;
        chargeSlider.gameObject.SetActive(false);

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = true;
        }
    }

    void Update()
    {
        if (!isShoot)
        {
            UpdateShootDirection();
        }

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
        if (Input.GetKey(KeyCode.A))
        {
            rotationAngle -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotationAngle += rotationSpeed * Time.deltaTime;
        }

        float radians = rotationAngle * Mathf.Deg2Rad;
        shootDirection = new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians)).normalized;

        if (lineRenderer != null)
        {
            Vector3 startPoint = transform.position;
            Vector3 endPoint = startPoint + shootDirection * 2f;
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);
        }
    }

    void Shoot(float chargeTime)
    {
        if (isShoot)
            return;

        float calculatedForce = chargeTime * forceMultiplier;

        rb.AddForce(shootDirection * calculatedForce, ForceMode.Impulse);
        isShoot = true;

        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }

        StartCoroutine(ResetBallVelocityAfterTime(5f));
    }

    IEnumerator ResetBallVelocityAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        isShoot = false;

        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
        }
    }

    public void ResetShootStatus()
    {
        isShoot = false;
    }

    public void SetControllable(bool canControl)
    {
        this.enabled = canControl;
    }
}
