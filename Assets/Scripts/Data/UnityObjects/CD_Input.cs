﻿using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Input", menuName = "PickerPro/CD_Input", order = 0)]
    public class CD_Input : ScriptableObject
    {
        public InputData Data;
    }
}