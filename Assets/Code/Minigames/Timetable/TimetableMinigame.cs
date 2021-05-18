using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timetable {
    public class TimetableMinigame : Minigame
    {
        private TimetableVariant _variant;
        [SerializeField] private TimetableGenerator _generator;
        [SerializeField] private TimetableLinesSpawner _spawner;
        [SerializeField] private LineGrabber _grabber;
        [SerializeField] private AudioClip[] _audioSwaped;
        private List<TimetableLine> _lines = new List<TimetableLine>();

        private void OnEnable()
        {
            _grabber.TryMoveDown += MoveDown;
            _grabber.TryMoveUp += MoveUp;
        }

        private void OnDisable()
        {
            _grabber.TryMoveDown -= MoveDown;
            _grabber.TryMoveUp -= MoveUp;
        }

        private void SwapLines(int a, int b) 
        {
            Debug.Log($"Swaping {a} and {b}");
            EmitSound(_audioSwaped[UnityEngine.Random.Range(0, _audioSwaped.Length)]);
            
            string tmpstring = _lines[a].Value;
            _lines[a].SetText(_lines[b].Value);
            _lines[b].SetText(tmpstring);

            int tmpIndex = _lines[b].Index;
            _lines[b].Index = _lines[a].Index;
            _lines[a].Index = tmpIndex;

            CheckWinCondition();
        }
        

        private TimetableLine MoveDown(TimetableLine line)
        {
            try
            {
                int index = _lines.FindIndex((v) => (v==line));

                if (index < _lines.Count - 1)
                {
                    SwapLines(index, index + 1);
                    return _lines[index+1];
                }
                return _lines[index];
            }
            catch (Exception) { return null; }
        }

        private TimetableLine MoveUp(TimetableLine line) 
        {
            try
            {
                int index = _lines.FindIndex((v) => (v == line));

                if (index > 0)
                {
                    SwapLines(index, index - 1);
                    return _lines[index - 1];
                }
                return _lines[index];
            }
            catch (Exception) { return null; }
        }

        private void PrepareMinigame()
        {
            _variant = _generator.GetVariant();
            _lines = _spawner.Spawn(_variant);
        }

        public override void InterruptGame() 
        {
            base.InterruptGame();
            Flush();
            _grabber.Deactivate();
        }

        public override void FinishGame()
        {
            base.FinishGame();
            Flush();
            _grabber.Deactivate();
        }

        public override void StartGame() 
        {
            base.StartGame();
            _grabber.Activate();
            PrepareMinigame();
        }

        private void CheckWinCondition() 
        {
            for (int i = 0; i<_lines.Count-1;i++) 
            {
                if (_lines[i].Index > _lines[i + 1].Index) 
                {
                    return;
                }
            }
            FinishGame();
        }
        private void Flush()
        {
            foreach (TimetableLine line in _lines)
            {
                Destroy(line.gameObject);
            }
            Debug.Log("clearing");
            _lines.Clear();
        }
    }
}