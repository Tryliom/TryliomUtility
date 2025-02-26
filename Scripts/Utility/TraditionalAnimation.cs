using System;
using System.Collections.Generic;
using UnityEngine;

namespace TryliomUtility
{
    [Serializable]
    public class TraditionalAnimation
    {
        public List<Sprite> Sprites;

        [Tooltip("Time between each sprite in seconds, 0 if using max life time / number of sprites")]
        public float TimeBetweenSprites;

        public bool OneLoop;

        private int _currentSpriteIndex;
        private float _time;
        private float _maxTime;

        public void Init(float maxLifeTime)
        {
            _maxTime = TimeBetweenSprites;

            if (TimeBetweenSprites == 0)
            {
                _maxTime = maxLifeTime / Sprites.Count;
            }
        }

        public Sprite GetNextSprite(float deltaTime = 0)
        {
            if (Sprites.Count == 0) return null;

            if (_maxTime == 0) return Sprites[0];

            _time += deltaTime;

            if (_time >= _maxTime)
            {
                if (OneLoop && _currentSpriteIndex == Sprites.Count - 1) return Sprites[_currentSpriteIndex];

                _time -= _maxTime;
                _currentSpriteIndex++;
            }

            if (_currentSpriteIndex >= Sprites.Count)
            {
                if (TimeBetweenSprites == 0) _currentSpriteIndex--;
                else _currentSpriteIndex = 0;
            }

            return Sprites[_currentSpriteIndex];
        }

        public void Reset()
        {
            _currentSpriteIndex = 0;
            _time = 0;
            _maxTime = 0;
        }

        public bool IsFinished()
        {
            return _currentSpriteIndex == Sprites.Count - 1 && _time >= _maxTime;
        }

        public float GetCompletion()
        {
            var completion = (float)_currentSpriteIndex / (Sprites.Count - 1);

            if (_maxTime is 0 || _time is 0) return completion;

            completion += _time / _maxTime / (Sprites.Count - 1);

            return completion;
        }

        public TraditionalAnimation Clone()
        {
            return new TraditionalAnimation()
            {
                Sprites = Sprites,
                TimeBetweenSprites = TimeBetweenSprites,
                OneLoop = OneLoop
            };
        }
    }
}