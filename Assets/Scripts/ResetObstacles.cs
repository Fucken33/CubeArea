using UnityEngine;
using System.Collections;

public class ResetObstacles : MonoBehaviour
{

    public float startX;
    public float maxStartY;
    public float minStartY;
    private bool canReset = true;

    void setObstacleColor(Color newColor, Transform groupTransform)
    {
        // Get obstacle group renderer components
        Component[] renderers = groupTransform.GetComponentsInChildren(typeof(SpriteRenderer));
        foreach (Component r in renderers)              // for each component
        {
            if (r.gameObject.tag == "Obstacle")
            {
                SpriteRenderer r_cast = (SpriteRenderer)r; // cast component to renderer
                r_cast.color = newColor;                   // Set color to new color
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (canReset && other.gameObject.tag == "ObstacleGroup")
        {
            StartCoroutine(resetObstacles(other));
        }
    }

    IEnumerator resetObstacles(Collision2D other)
    {
        
        ObstacleController oc = other.gameObject.GetComponent<ObstacleController>();
        int oldId = oc.id;
        oc.nextId();
        int newId = oc.id;

        if(PlayerController.debug) Debug.Log("Setting obstacle id "+oldId+" to "+newId);

        setObstacleColor(Color.white, other.transform);

        Vector3 newPos = other.transform.position;
        newPos.x = startX;

        float startY = Random.Range(minStartY, maxStartY);
        newPos.y = startY;

        other.transform.position = newPos;

        canReset = false;
        yield return new WaitForSeconds(0.5f);
        canReset = true;
    }
}
