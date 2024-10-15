using Commands.Levels;
using Data.UnityObjects;
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
        private LevelData GetlevelData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level").levels[_currentLevel]; //?
        }

        private byte GetActiveLevel()
        {
            return _currentLevel;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGamesSignals.Instance.onLevelInitialize += _levelLoaderCommand.Exucute;
            CoreGamesSignals.Instance.onLevelDestroy += _levelDestroyerCommand.Exucute;
            CoreGamesSignals.Instance.onGetLevelValue += OnGetValue;
            CoreGamesSignals.Instance.onNextLevel += OnNextLevel;
            CoreGamesSignals.Instance.onRestartLevet += OnRestartLevel;
        }

        private void UnSubscribeEvents()
        {
            CoreGamesSignals.Instance.onLevelInitialize -= _levelLoaderCommand.Exucute;
            CoreGamesSignals.Instance.onLevelDestroy -= _levelDestroyerCommand.Exucute;
            CoreGamesSignals.Instance.onGetLevelValue -= OnGetValue;
            CoreGamesSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGamesSignals.Instance.onRestartLevet -= OnRestartLevel;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}

