using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GroundChecker;


public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    private float maxGroundAngle = 60;
    [SerializeField]
    private LayerMask groundMask;

    public List<GroundRay> groundRays = new List<GroundRay>();

    private void Awake()
    {
        foreach (var ray in groundRays)
        {
            ray.SetMaxAngle(maxGroundAngle);
        }
    }

    private void Update()
    {
        foreach (GroundRay ray in groundRays) 
        {
            ray.ShootRay(transform, groundMask);
        }

    }
    private void FixedUpdate()
    {
        foreach (GroundRay ray in groundRays)
        {
            ray.ShootRay(transform, groundMask);
        }
    }
    public Contact GetPriorityContact()
    {
        List<GroundRay> sortedRays = SortedRays();
        foreach (var ray in sortedRays)
        {
            //find the fist ray thats grounded 
            if (ray.rayContact.onGround)
            {
                //return that ray
                return ray.rayContact;
            }
        }
        //else return the first ray with a non grounded contact
        return sortedRays[0].rayContact;
    }

    public List<GroundRay> SortedRays()
    {
        return groundRays.OrderByDescending(child => child.Priority).ToList();
    }

    private void OnDrawGizmos()
    {
        foreach(GroundRay ray in groundRays)
        {
            if (ray.debugRays)
            {
                Gizmos.color = Color.red;
                if (ray.GroundFound)
                {
                    Gizmos.color = Color.green;
                }
                Gizmos.DrawLine(transform.position + ray.GetOffset(), transform.position + ray.GetOffset() + (Vector3.down * ray.GetLength()));
            }
           
        }
    }
}
[Serializable]
public class GroundRay
{
    [SerializeField]
    public bool debugRays = true;

    [SerializeField]
    private float maxGroundAngle = 90f;

    public bool GroundFound
    {
        get
        {
            return rayContact.onGround;
        }
    }
    [SerializeField]
    private int priority = 0;
    public int Priority { get  { return priority; } }
    [SerializeField]
    private float rayLength = 2f;
    [SerializeField]
    private Vector3 offset;
    RaycastHit hit;

    public Contact rayContact;

    public void ShootRay(Transform transform, LayerMask groundMask)
    {
        if (Physics.Raycast(transform.position + offset, Vector3.down, out hit, rayLength, groundMask))
        {
            if (debugRays)
            {
                //Debug.DrawLine(transform.position + offset, hit.point, Color.green);
            }
            rayContact.gameObject = hit.collider.gameObject;
            rayContact.normal = hit.normal;
            rayContact.groundAngle = Vector3.Angle(hit.normal, Vector2.up);
            if (hit.collider.sharedMaterial != null)
            {
                rayContact.physicMaterial = hit.collider.sharedMaterial;
            }
            else
            {
                rayContact.physicMaterial = new PhysicsMaterial();
            }

            if (rayContact.groundAngle <= maxGroundAngle)
            {
                rayContact.onGround = true;
            }
            else
            {
                rayContact.onGround = false;
            }


            rayContact.rb = hit.collider.attachedRigidbody;
        }
        else
        {
            if (debugRays)
            {
                //Debug.DrawLine(transform.position + offset, transform.position + offset + (Vector3.down * rayLength), Color.red);
            }
            rayContact.ResetContact();
        }
    }
    public void SetMaxAngle(float maxAngle)
    {
        maxGroundAngle = maxAngle;
    }
    public Vector3 GetOffset()
    {
        return offset;
    }
    public float GetLength()
    {
        return rayLength;
    }
}

[Serializable]
public class Contact
{
    public GameObject gameObject = null;
    public Vector3 normal = Vector3.zero;
    public PhysicsMaterial physicMaterial;
    public float groundAngle = 0;
    public bool onGround = false;
    public Rigidbody rb = null;


    public void ResetContact()
    {
        gameObject = null;
        rb = null;
        normal = Vector3.zero;
        groundAngle = 0;
        onGround = false;
    }
}