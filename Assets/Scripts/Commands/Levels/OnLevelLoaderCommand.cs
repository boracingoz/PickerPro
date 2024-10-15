using UnityEngine;

namespace Commands.Levels
{
    public class OnLevelLoaderCommand
    {
        private Transform _levelHolder;

        public OnLevelLoaderCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }

        public void Exucute(byte levelIndex) //level indexini görmek için
        {
            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/Level {levelIndex}"), _levelHolder, true); //?
        }
    }
}
