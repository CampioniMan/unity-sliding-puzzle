using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private My2DGrid grid;

    public void Start()
    {
        grid.Setup();
    }
}
