using Commands.Levels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ValueObjects.Data;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform _levelHolder;
        [SerializeField] byte _totalLevelCount;

        private OnLevelLoaderCommand _levelLoaderCommand;
        private OnLevelDestroyerCommand _levelDestroyerCommand;

        private byte _currentLevel;
        private LevelData _levelData;

        private void Awake()
        {
            _levelData = GetlevelData();
            _currentLevel = GetActiveLevel();

            Init();
        }

        private void Init()
        {
            _levelLoaderCommand = new OnLevelLoaderCommand(_levelHolder);
            _levelDestroyerCommand = new OnLevelDestroyerCommand(_levelHolder);
        }

        private byte GetActiveLevel()
        {
            throw new NotImplementedException();
        }

        private LevelData GetlevelData()
        {
            throw new NotImplementedException();
        }
    }
}

