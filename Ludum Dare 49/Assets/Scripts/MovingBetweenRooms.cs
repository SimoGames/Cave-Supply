using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBetweenRooms : MonoBehaviour
{
    public Animation camAnim;
    public bool IsInMid;
    public bool IsInLeft;
    public bool IsInRight;
    public bool isCamMoving;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Left")
        {
            isCamMoving = true;
            IsInMid = false;
            IsInRight = false;
            IsInLeft = true;
            camAnim.Play("MachineToPress");
            StartCoroutine(camMovingTimer());
        }
        if (collision.tag == "Right")
        {
            isCamMoving = true;
            IsInMid = false;
            IsInRight = true;
            IsInLeft = false;
            camAnim.Play("MachineToConveyor");
            StartCoroutine(camMovingTimer());
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Left")
        {
            isCamMoving = true;
            IsInMid = true;
            IsInRight = false;
            IsInLeft = false;
            camAnim.Play("PressToMachine");
            StartCoroutine(camMovingTimer());
        }
        if (collision.tag == "Right")
        {
            isCamMoving = true;
            IsInMid = true;
            IsInRight = false;
            IsInLeft = false;
            camAnim.Play("ConveyorToMachine");
            StartCoroutine(camMovingTimer());
        }
    }
    IEnumerator camMovingTimer()
    {
        yield return new WaitForSeconds(1);
        isCamMoving = false;
    }

}
