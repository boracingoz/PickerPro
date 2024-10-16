using Assets.Scripts.Signals;
using Data.UnityObjects;
using Data.ValueObjects;
using Keys;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Manager
{
    public class InputManager : MonoBehaviour
    {
        private InputData _data;
        private bool _isAvailableForTouch, _isFirstTouch, _isTouching;

        private float _currentVelocity;
        private float3 _moveVector;
        private Vector2? _mousePos;



        private void Awake()
        {
            _data = GetInputData();
        }

        private InputData GetInputData()
        {
            return Resources.Load<CD_Input>("Data/CD_Input").Data;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGamesSignals.Instance.onReset += OnReset;
            InputSingals.Instance.onEnableInput += OnEnableInput;
            InputSingals.Instance.onDisableInput += OnDisableInput;
        }


        private void OnEnableInput()
        {
            _isAvailableForTouch = true;
        }

        private void OnDisableInput()
        {
            _isAvailableForTouch = false;
        }
        private void OnReset()
        {
            _isAvailableForTouch = false;
            //_isFirstTouch = false;
            _isTouching = false;
        }

        private void UnubSubscribeEvents()
        {
            CoreGamesSignals.Instance.onReset -= OnReset;
            InputSingals.Instance.onEnableInput -= OnEnableInput;
            InputSingals.Instance.onDisableInput -= OnDisableInput;
        }

        private void OnDisable()
        {
            UnubSubscribeEvents();
        }

        private void Update()
        {
            if (!_isAvailableForTouch)
            {
                return;
            }

            if (Input.GetMouseButtonUp(0) /*& !IsPointerUIElement()*/)
            {
                _isTouching = false;
                InputSingals.Instance.onInputRelase?.Invoke();
                Debug.LogWarning("Executed ----> OnInputRelased");
            }

            if (Input.GetMouseButtonDown(0) /*& !IsPointerUIElement()*/)
            {
                _isTouching = true;
                InputSingals.Instance.onInputRelase?.Invoke();
                Debug.LogWarning("Çalışıyor ----> OnInputRelased");

                if (!_isFirstTouch)
                {
                    _isTouching = true;
                    InputSingals.Instance.onInputRelase?.Invoke();
                    Debug.LogWarning("Çalışıyor ----> OnInputRelased");
                }

                _mousePos = Input.mousePosition;
            }

            if (Input.GetMouseButtonDown(0) & !IsPointerUIElement())
            {
                if (_isTouching)
                {
                    if (_mousePos != null)
                    {
                        Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePos.Value;
                        if (mouseDeltaPos.x > _data.horizontalInputSpeed)
                        {
                            _moveVector.x = _data.horizontalInputSpeed / 10f * mouseDeltaPos.x;
                        }
                        else if(mouseDeltaPos.x < _data.horizontalInputSpeed)
                        {
                            _moveVector.x = -_data.horizontalInputSpeed / 10f * mouseDeltaPos.x;
                        }
                        else
                        {
                            _moveVector.x = Mathf.SmoothDamp(_moveVector.x,0f, ref _currentVelocity, _data.clampSpeed); 
                        }

                        _mousePos = Input.mousePosition;

                        InputSingals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                        {
                            horiMove = _moveVector.x,
                            clampValues = _data.clampValue
                        });
                    }
                }
            }
        }

        private bool IsPointerUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;   
        }
    }
}