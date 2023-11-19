using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{

    
    [Header("Movement Properties")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject plane;
    [SerializeField] private Camera cam;

    [Header("Shot Properties")]
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private float shotCoolDown; 

    [HideInInspector] 
    public Queue<GameObject> shotStock = new Queue<GameObject>();
    private bool canShot = true;
    private int maxShotQuant = 10;

    // OTHERS
    private bool isPaused = false; // put this on GameManager Later

    #region "Regulars"

    private void Awake() 
    {
        if(!cam) 
            cam = Camera.main;
 
        for(int i = 0; i < maxShotQuant; i++)
        {
            GameObject newShot = Instantiate(shotPrefab);
            newShot.SendMessage("SetPlaneParent", this);
            newShot.SetActive(false);
            shotStock.Enqueue(newShot);
        }
    }

    private void Update() 
    {
        if(Input.touchCount > 0 && !isPaused)
        {
            var touch = Input.GetTouch(0);
            var touchPos = cam.ScreenToWorldPoint(touch.position);
            
            plane.transform.position = Vector3.MoveTowards(plane.transform.position, 
                new Vector3(
                    touchPos.x,
                    touchPos.y,
                    plane.transform.position.z
                ),
                moveSpeed * Time.deltaTime);
        }    
    }

    #endregion

    #region "SHOT"

    public void ShotButton()
    {
        if (canShot)
            StartCoroutine(Shot());
    }

    private IEnumerator Shot()
    {

        canShot = false;

        GameObject shot = shotStock.Dequeue();
        shot.SetActive(true); 
        shot.transform.position = transform.position;



        yield return new WaitForSeconds(shotCoolDown);
        canShot = true;
    }
    #endregion
}
