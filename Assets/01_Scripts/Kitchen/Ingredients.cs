using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ingredients : MonoBehaviour
{
    [SerializeField]
    private PoolObjectType thisType;

    public PoolObjectType Gettype()
    {
        return thisType;
    }

}
