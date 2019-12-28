using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebindManager : MonoBehaviour
{
    KeyCode newKey; // the new key binding that will replace the old one

    Event keyEvent; // the event that checks for key press in onGui

    private bool isWaiting; // bool to check if it's waiting for a key press
    private bool validKey = false;

    private void OnGUI()
    {
        keyEvent = Event.current;

        if (keyEvent.isKey && isWaiting) //checks if player pressed any key
        {
            if(keyEvent.keyCode != KeyCode.Tab)
            {
                newKey = keyEvent.keyCode; // sets the key to the keyboard press
                isWaiting = false; // this stops it from running multiple times

                Debug.Log("set key");
            }
            else
            {
                isWaiting = false;
                Debug.Log("invalid key");
            }
        }
        else if(keyEvent.isMouse && isWaiting)
        {
            if (keyEvent.button == 0)
                newKey = KeyCode.Mouse0;
            else
                newKey = KeyCode.Mouse1;

            isWaiting = false;

            Debug.Log("set key");
        }
    }

    IEnumerator WaitForKey() // waits for key press 
    {
        while(!keyEvent.isKey)
        {
            yield return null;
        }
    }

    public void StartAssign(string keyName) // method that runs when button is pressed
    {
        newKey = KeyBinding.GetKeyCode(keyName);

        if(isWaiting == false)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    public void ChangeText(Text obj)
    {
        obj.text = "Waiting for Key Press";

        StartCoroutine(AssignText(obj));
    }

    public IEnumerator AssignText(Text obj)
    {
        yield return WaitForKey();

        if (validKey)
            obj.text = newKey.ToString();
    }

    public IEnumerator AssignKey(string keyName) // This actually does the assignment and starts the checking for key presses
    {
        isWaiting = true; // this sets it to start checking for key presses

        Debug.Log("assigning..");
        yield return WaitForKey(); // runs endlessly till key pressed

        if (newKey == default(KeyCode))
            yield return null;
        else
        {
            validKey = true;

            KeyBinding.ChangeKey(keyName, newKey);

            Debug.Log("Finished! New Key = " + newKey.ToString());
        }

        yield return null;
    }
}

public static class KeyBinding
{
    public static KeyCode jumpCode = KeyCode.Space;
    public static KeyCode attackCode = KeyCode.V;

    public static IDictionary<string, KeyCode> Keys = new Dictionary<string, KeyCode>() { { "btnJump", jumpCode }, { "btnAttack", attackCode } };

    public static void ChangeKey(string keyName, KeyCode newBinding)
    {
        Keys[keyName] = newBinding;
        Debug.Log(Keys[keyName]);
    }

    public static KeyCode GetKeyCode(string keyName)
    {
        return Keys[keyName];
    }
}
