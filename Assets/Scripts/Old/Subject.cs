using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern {

    public class Subject : MonoBehaviour
    {

        // A l.ist of observers
        List<Observer> observers = new List<Observer>();

        //Send notifications if something has happened
        public void Notify() {
            for (int i = 0; i < observers.Count; i++) {

                //notify all oberservers regardless of whether or not they are interested
                observers[i].OnNotify();

                // to scale this pass an event through it
                //observers[i].OnNotify(Event, event);

            }

        }
         // add / remove observers
        public void AddObserver(Observer observer) {
            observers.Add(observer);
        }

        public void RemoveObserver(Observer observer) {
            
        }
       
    }

}