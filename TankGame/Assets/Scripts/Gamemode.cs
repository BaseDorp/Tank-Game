﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour
{
    void Awake()
    {
        // Limits fps count
        Application.targetFrameRate = 60;
    }
}