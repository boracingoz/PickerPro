﻿using System;
using UnityEngine;

namespace Data.ValueObjects
{
    [Serializable]
    public struct InputData
    {
        public float horizontalInputSpeed;
        public Vector2 clampValue;
        public float clampSpeed;
    }
}
