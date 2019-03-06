using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{

    //Wants to know when another object does something interesting
    public abstract class Observer
    {
        public abstract void OnNotify();
    }

    public class Box : Observer
    {

        // the bo game object which will do something
        GameObject boxObj;

        // What will happen when this box gets an event
        BoxEvents boxEvent;



        public Box(GameObject boxObj, BoxEvents boxEvent)
        {
            this.boxObj = boxObj;
            this.boxEvent = boxEvent;

        }

        // what the box will do if the event fits it
        public override void OnNotify()
        {
            Jump(boxEvent.GetJumpForce());
        }

        // to scale this get the event and see if it corresponds to the event the observer is subscribing to

        //public override void OnNotify(Event, event)
        //{
        //    // if (evt == "pitch")

        //    Jump(boxEvent.GetJumpForce());
        //}


        //The box will always jump
        void Jump(float jumpForce)
        {
            //if the box is close to the ground
            if (boxObj.transform.position.y < 0.55f)
            {

                boxObj.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
            }
        }


    }

}