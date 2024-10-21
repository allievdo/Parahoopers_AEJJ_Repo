using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Renderer ren;
    CourseCheckpoints courseCheckpoints;

    void Start()
    {
        //ren.GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            courseCheckpoints.PlayerThroughCheckpoint(this, other.transform);
        }
    }

    public void SetCourseCheckpoints(CourseCheckpoints courseCheckpoints)
    {
        this.courseCheckpoints = courseCheckpoints;
    }

    public void WinColor()
    {
        ren.material.color = Color.green; //look at this later
    }
}
