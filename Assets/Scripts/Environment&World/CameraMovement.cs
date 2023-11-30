using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform followTransform;
    public BoxCollider2D mapBounds;
    
    

    private float xMin, xMax, yMin, yMax;
    private float camX, camY;
    private float camOrthoSize;
    private float camRatio;
    private Camera mainCam;

    // Start is called before the first frame update

    void Start()
    {
        xMin = mapBounds.bounds.min.x;
        yMin = mapBounds.bounds.min.y;
        xMax = mapBounds.bounds.max.x;
        yMax = mapBounds.bounds.max.y;

        mainCam = GetComponent<Camera>();
        camOrthoSize = mainCam.orthographicSize;
        camRatio = (xMax + camOrthoSize) / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        camY = Mathf.Clamp(followTransform.position.y, yMin + camOrthoSize, yMax - camOrthoSize);
        camX = Mathf.Clamp(followTransform.position.x, xMin + camRatio, xMax - camRatio);
        this.transform.position = new Vector3(camX, camY, this.transform.position.z);
    }
}
