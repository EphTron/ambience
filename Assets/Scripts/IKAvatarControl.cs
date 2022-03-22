using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class IKAvatarControl : MonoBehaviour
{
    public GameObject xrOrigin;
    public GameObject vrCamera;
    public GameObject ikBackTarget;

    public GameObject avatarHead;
    //public GameObject avatarHeadTransfrom;
    //protected Animator animator;

    //public bool ikActive = false;
    //public Transform rightHandObj = null;
    //public Transform lookObj = null;

    void Start()
    {
        Vector3 originToCamera = vrCamera.transform.position - xrOrigin.transform.position;
        this.transform.position = xrOrigin.transform.position + new Vector3(originToCamera.x, 0f, originToCamera.z);
        //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // update avatar position
        Vector3 originToCamera = vrCamera.transform.position - xrOrigin.transform.position;
        Vector3 originToCameraXZ = new Vector3(originToCamera.x, 0f, originToCamera.z);
        if (originToCameraXZ.magnitude > 0.2f)
        {
            this.transform.position = xrOrigin.transform.position + new Vector3(originToCamera.x, 0f, originToCamera.z);
        }

        // get forward direction of camera in xz plane
        Vector3 forwardDir = Vector3.ProjectOnPlane(vrCamera.transform.forward, Vector3.up);
        Debug.DrawRay(this.transform.position, forwardDir.normalized * 100, Color.magenta);
        Debug.DrawRay(vrCamera.transform.position, vrCamera.transform.forward * 100, Color.yellow);

        float avatarHeadGroundDistance = (this.transform.position - avatarHead.transform.position).y;
        float vrHeadGroundDistance = (xrOrigin.transform.position - vrCamera.transform.position).y;
        Debug.Log("Distance:" + vrHeadGroundDistance + " Avatar: " + avatarHeadGroundDistance);
        // angle between projected camera forward direction and body forward direction
        float angle = Vector3.Angle(forwardDir, this.transform.forward);

        Quaternion currentOrientation = this.transform.rotation;
        Quaternion goalOrientation = Quaternion.LookRotation(forwardDir, Vector3.up);

        this.transform.rotation = Quaternion.Slerp(currentOrientation, goalOrientation, 0.5f);
        ikBackTarget.transform.position = vrCamera.transform.position;
        //this.transform.rotation = Quaternion.LookRotation(this.transform.forward, forwardDir);
    }

    ////a callback for calculating IK
    //void OnAnimatorIK()
    //{
    //    if (animator)
    //    {

    //        //if the IK is active, set the position and rotation directly to the goal. 
    //        if (ikActive)
    //        {

    //            // Set the look target position, if one has been assigned
    //            if (lookObj != null)
    //            {
    //                animator.SetLookAtWeight(1);
    //                animator.SetLookAtPosition(lookObj.position);
    //            }

    //            // Set the right hand target position and rotation, if one has been assigned
    //            if (rightHandObj != null)
    //            {
    //                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
    //                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
    //                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
    //                animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
    //            }

    //        }

    //        //if the IK is not active, set the position and rotation of the hand and head back to the original position
    //        else
    //        {
    //            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
    //            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
    //            animator.SetLookAtWeight(0);
    //        }
    //    }
    //}
}