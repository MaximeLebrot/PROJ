using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScriptTemplate : MonoBehaviour
{

    /*
     * OnEventCalled will be executed when an event of the specified type is created/fired.
     * Events are stored in the script IEvent
     * If you want to create a new type of event, talk to a programmer about adding it, and when it should be used
     */

    private void OnEnable()
    {        
        //EventHandler<DIN EVENTTYP HÄR>.RegisterListener(OnEventCalled);
    }
    private void OnDisable()
    {
        //EventHandler<DIN EVENTTYP HÄR>.UnregisterListener(OnEventCalled);
    }
    private void OnEventCalled(/*DIN EVENTTYP HÄR, thisEvent*/)
    {
        //Do stuff
    }
}

