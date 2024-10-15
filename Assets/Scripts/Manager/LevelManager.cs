using Commands.Levels;
using Data.UnityObjects;
using Signals;
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

        private short _currentLevel;
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
            return (byte)_currentLevel;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGamesSignals.Instance.onLevelInitialize += _levelLoaderCommand.Execute;
            CoreGamesSignals.Instance.onClearActiveLevel += _levelDestroyerCommand.Exucute;
            CoreGamesSignals.Instance.onGetLevelValue += OnGetValue;
            CoreGamesSignals.Instance.onNextLevel += OnNextLevel;
            CoreGamesSignals.Instance.onRestartLevet += OnRestartLevel;
        }

        private void OnNextLevel()
        {
            _currentLevel++;
            CoreGamesSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGamesSignals.Instance.onReset?.Invoke();
            CoreGamesSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % _totalLevelCount));
        }

        private void OnRestartLevel()
        {
            CoreGamesSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGamesSignals.Instance.onReset?.Invoke();
            CoreGamesSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % _totalLevelCount));
        }

        private byte OnGetValue()
        {
            return (byte)_currentLevel;
        }

        private void UnSubscribeEvents()
        {
            CoreGamesSignals.Instance.onLevelInitialize -= _levelLoaderCommand.Execute;
            CoreGamesSignals.Instance.onClearActiveLevel -= _levelDestroyerCommand.Exucute;
            CoreGamesSignals.Instance.onGetLevelValue -= OnGetValue;
            CoreGamesSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGamesSignals.Instance.onRestartLevet -= OnRestartLevel;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        {
            CoreGamesSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % _totalLevelCount));  
        }
    }
}

