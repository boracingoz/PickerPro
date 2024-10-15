using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ValueObjects.Data;

namespace Data.UnityObjects
{

    [CreateAssetMenu(fileName ="CD_Level", menuName ="PickerPro/CD_Level",order =0)]
    public class CD_Level : ScriptableObject
    {
        public List<LevelData> levels;
    }
}