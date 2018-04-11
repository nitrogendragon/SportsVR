using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {
    // camerarig position reference
    public Transform cameraRigTransform;
    // reference for reticle prefab
    public GameObject teleportReticlePrefab;
    // another reticle reference bbut not prefab
    private GameObject reticle;
    // positon of reticle
    private Transform teleportReticleTransform;
    // positon of head
    public Transform headTransform;
    // offset of reticle
    public Vector3 teleportReticleOffset;
    // layers used fo rdetermining whether to teleport or not
    public LayerMask teleportMask;
    public LayerMask dontTeleportMask;
    // should we teleport?
    private bool shouldTeleport;

    private SteamVR_TrackedObject trackedObj;//object being tracked particularly the controller I believe
    // prefab for laser
    public GameObject laserPrefab;
    // actual laser 
    private GameObject laser;
    // psoiton of laser
    private Transform laserTransform;
    // where the laser hits an object
    private Vector3 hitPoint;
    private SteamVR_Controller.Device Controller//gets the controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    //function for showing the laser
    private void ShowLaser(RaycastHit hit)
    {
        // sets active/visible
        laser.SetActive(true);
        // sets positon
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        // wher it points  to
        laserTransform.LookAt(hitPoint);
        // scales laser based on hit distance and scale of x and y
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
    }

    void Awake()
    {
        //grabs tracked object 
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Start()
    {
        // instantiate laser using prefab
        laser = Instantiate(laserPrefab);
        // set position
        laserTransform = laser.transform;

        // instantiates reticle based on prefab
        reticle = Instantiate(teleportReticlePrefab);
        //sets position
        teleportReticleTransform = reticle.transform;
    }
    // Update is called once per frame
    void Update () {
        // did we touch the touchpad
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //raycasts
            RaycastHit hit;

            // did we hit a non teleportable area, if so do nothing
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, dontTeleportMask))
            {
                return;
            }
                // if we hit a telport area..
                else if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                hitPoint = hit.point;// set point
                ShowLaser(hit);//show laser at point
                // set active
                reticle.SetActive(true);
                // set position of reticle
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                // we can teleport
                shouldTeleport = true;
            }
        }
        else // hide laser
        {
            laser.SetActive(false);
            reticle.SetActive(false);
        }
        //on touchpad release telport to spot
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
        {
            Teleport();
        }
    }

    private void Teleport()
    {
        // falsify bool
        shouldTeleport = false;
        // hide reticle
        reticle.SetActive(false);
        // adjust for height of player
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        // set y difference
        difference.y = 0;
        // teleport
        cameraRigTransform.position = hitPoint + difference;
    }
}
