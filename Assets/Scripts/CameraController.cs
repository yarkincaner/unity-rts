using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviour
{
    float speed = 0.02f;
    float zoomSpeed = 1.0f;
    float rotateSpeed = 0.001f;

    float maxHeight = 60f;
    float minHeight = 10f;

    Vector2 p1;
    Vector2 p2;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 0.02f;
            zoomSpeed = 2.0f;
        } else
        {
            speed = 0.01f;
            zoomSpeed = 1.0f;
        }

        float horizontalSpeed = transform.position.y * speed * Input.GetAxis("Horizontal");
        float verticalSpeed = transform.position.y * speed * Input.GetAxis("Vertical");
        float scrollSpeed = transform.position.y * zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        if ((transform.position.y >= maxHeight) &&  (scrollSpeed > 0))
        {
            scrollSpeed = 0;
        } else if ((transform.position.y <= minHeight) && (scrollSpeed < 0))
        {
            scrollSpeed = 0;
        }

        if ((transform.position.y + scrollSpeed) > maxHeight) {
            scrollSpeed = maxHeight - transform.position.y;
        } else if ((transform.position.y + scrollSpeed) < minHeight)
        {
            scrollSpeed = minHeight - transform.position.y;
        }

        Vector3 verticalMove = new Vector3(0, scrollSpeed, 0);
        Vector3 lateralMove = horizontalSpeed * transform.right;
        Vector3 forwardMove = transform.forward;
        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= verticalSpeed;

        Vector3 move = verticalMove + lateralMove + forwardMove;
        Vector3 newPosition = transform.position += move;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 0.5f);

        getCameraRotation();
    }

    void getCameraRotation()
    {
        if (Input.GetMouseButtonDown(2)) {
            p1 = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            p2 = Input.mousePosition;
            float dx = (p2 - p1).x * rotateSpeed;
            float dy = (p2 - p1).y * rotateSpeed;

            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));

            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0));
        }
    }
}
