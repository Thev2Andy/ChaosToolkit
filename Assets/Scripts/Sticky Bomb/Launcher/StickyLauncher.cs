using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyLauncher : MonoBehaviour
{
    public GameObject StickyPrefab;

    public float LaunchForce;

    public float StickyDelay;

    public Transform StickySpawnPoint;

    public Transform StickySpawnPointPivot;

    // Private / Hidden variables.
    [HideInInspector] public GameObject StickyInstance;
    private Camera Cam;
    private float stickyTimer;

    private void Start()
    {
       Cam = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 mousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - new Vector2(StickySpawnPointPivot.position.x, StickySpawnPointPivot.position.y);
        StickySpawnPointPivot.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f));

        stickyTimer -= Time.deltaTime;
        if (stickyTimer < 0) stickyTimer = 0;
        

        if (Input.GetMouseButtonDown(1) && StickyInstance == null && stickyTimer <= 0)
        {
            StickyInstance = Instantiate(StickyPrefab, StickySpawnPoint.position, StickySpawnPoint.rotation);
            
            if (StickyInstance.GetComponent<Rigidbody2D>())
            {
                if (this.GetComponent<Rigidbody2D>()) StickyInstance.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity;
                StickyInstance.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, LaunchForce));
            }

            stickyTimer = StickyDelay;
        }else if (Input.GetMouseButtonDown(1) && StickyInstance == null && stickyTimer > 0)
        {
            GameUIController.Instance.ShowMessage("Sticky bombs on cooldown.", 2.75f);
        }
    }
}
