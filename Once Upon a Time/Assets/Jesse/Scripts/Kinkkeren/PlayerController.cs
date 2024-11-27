using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    [HideInInspector] public bool isShoot;

    private float forceMultiplier = 6f;
    private float rotationAngle = 0f;
    private float rotationSpeed = 90f;

    private Vector3 shootDirection;

    public LineRenderer lineRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.drag = 0.1f;
        rb.angularDrag = 0.05f;

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

    public void Shoot(float chargeTime)
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
