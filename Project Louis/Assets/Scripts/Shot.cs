using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [Header("Shot Properties")]
    public float speed;
    public float dmg;


    private Plane plane;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Limit")
        {
            //put some effect;
            plane.shotStock.Enqueue(this.gameObject);
            gameObject.SetActive(false);
        }
    }

    private void SetPlaneParent(Plane p)
    {
        plane = p;
    }
}
