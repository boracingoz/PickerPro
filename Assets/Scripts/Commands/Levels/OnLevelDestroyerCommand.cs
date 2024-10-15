using UnityEngine;

namespace Commands.Levels
{
    public class OnLevelDestroyerCommand
    {
        private Transform _levelHolder;

        public OnLevelDestroyerCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }

        public void Exucute()
        {
            if (_levelHolder.childCount <= 0)
            {
                return;
            }
            
            Object.Destroy(_levelHolder.transform.GetChild(0).gameObject);
        }
    }
}
