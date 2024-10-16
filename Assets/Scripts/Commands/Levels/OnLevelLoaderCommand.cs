using UnityEngine;

namespace Commands.Levels
{
    public class OnLevelLoaderCommand
    {
        private Transform _levelHolder;

        internal OnLevelLoaderCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }

        public void Execute(byte levelIndex)
        {
            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/Level {levelIndex}"), _levelHolder, true);
        }

    }
}
