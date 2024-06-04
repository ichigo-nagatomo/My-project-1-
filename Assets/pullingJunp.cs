using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class pullingJunp : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private Vector3 clickPosition;
    [SerializeField]
    private float jumpPower = 10;
    private bool isCanJump;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity = new Vector3(0, 10, 0);
        }

        if (Input.GetMouseButtonDown(0)) {
            clickPosition = Input.mousePosition;
        }

        if (isCanJump && Input.GetMouseButtonUp(0)) {
            Vector3 dist = clickPosition - Input.mousePosition;

            if (dist.sqrMagnitude == 0) {
                return;
            }

            rb.velocity = dist.normalized * jumpPower;
        }

        
    }

    private void OnCollisionStay(Collision collision) {
        ContactPoint[] contacts = collision.contacts;

        Vector3 otherNormal = contacts[0].normal;

        Vector3 upVector = new Vector3(0, 1, 0);

        float dotUN = Vector3.Dot(upVector, otherNormal);

        float dotDeg = Mathf.Acos(dotUN) * Mathf.Rad2Deg;

        if (dotDeg <= 45) {
            isCanJump = true;
        }
        
    }

    private void OnCollisionExit(Collision collision) {
        isCanJump = false;
    }
}
