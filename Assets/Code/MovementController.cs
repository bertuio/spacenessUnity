using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [SerializeField]
    private GameObject mainCam;
    private Vector3 cameraLinearOffset;
    private Quaternion cameraAngularOffset;
    private CharacterController cc;
    private Animator anim;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float linearSmoothRate;
    [SerializeField]
    private float angularSmoothRate;
    [SerializeField]
    private float maxClickDelay;
    private float nodAngle;
    private bool clickSwitcher;
    private float lastStepTime;
    private bool isWalking;
    // Start is called before the first frame update
    void Start()
    {
        cameraLinearOffset = transform.InverseTransformPoint(mainCam.transform.position);
        cameraAngularOffset = Quaternion.Inverse(transform.rotation)*mainCam.transform.rotation;
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        lastStepTime = Time.time;
        clickSwitcher = false;
        isWalking = false;
    }

    void Update() 
    {
        ComputeCameraTransform();
        PerformCharacterMoevement();
    }
    void PerformCharacterMoevement()
    {
        if (Time.time - lastStepTime > maxClickDelay & isWalking)
        {
            isWalking = false;
            anim.SetBool("Walking", false);
        }
        else
        {
            if (isWalking) cc.Move(transform.forward * speed*Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(clickSwitcher ? 0 : 1))
        {
            clickSwitcher = !clickSwitcher;
            isWalking = true;
            anim.SetBool("Walking", true);
            lastStepTime = Time.time;
        }
        cc.transform.rotation = Quaternion.Euler(new Vector3(0, rotationSpeed, 0) * Input.GetAxis("Mouse X") * Time.deltaTime) * cc.transform.rotation;

    }

    void ComputeCameraTransform() 
    {
        Vector3 newCameraPosition = Vector3.Lerp(mainCam.transform.position, transform.TransformPoint(cameraLinearOffset), linearSmoothRate);
        mainCam.transform.position = newCameraPosition;
        mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, transform.rotation * cameraAngularOffset, angularSmoothRate);
        nodAngle += Input.GetAxis("Mouse Y") * rotationSpeed;
        //TODO: add nod rotation
    }
}
