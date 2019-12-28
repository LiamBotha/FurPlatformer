using UnityEngine;
using System.Collections;
//-----------------------------------------------------------
//Enum defining all possible game events
//More events should be added to the list
public enum Events
{
    DEADENEMY, DEADPLAYER, DESTROYOBJECT,DEADBOSS
};
//-----------------------------------------------------------
//Listener interface to be implemented on Listener classes
public interface IListener
{
//Notification function invoked when events happen
void OnEvent(Events Event, Component Sender, Object Param = null);
}
//-----------------------------------------------------------