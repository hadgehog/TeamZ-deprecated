using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public enum DoorState
    {
        Open,
        Close,
        Deactivate
    }

    public DoorState state;
}
