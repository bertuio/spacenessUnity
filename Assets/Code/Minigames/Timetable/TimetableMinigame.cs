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
        private List<TimetableLine> _lines = new List<TimetableLine>();

        private void OnEnable()
        {
            _grabber.OnMoveDown += MoveDown;
            _grabber.OnMoveUp += MoveUp;
        }

        private void OnDisable()
        {
            _grabber.OnMoveDown -= MoveDown;
            _grabber.OnMoveUp -= MoveUp;
        }

        private void SwapLines(int a, int b) 
        {
            _lines[a].MoveTo(_spawner.GetPositionByIndex(b));
            _lines[b].MoveTo(_spawner.GetPositionByIndex(a));
            TimetableLine tmp = _lines[a];
            _lines[a] = _lines[b];
            _lines[b] = tmp;
            CheckWinCondition();
        }
        

        private void MoveDown(TimetableLine line) 
        {
            int index = _lines.FindIndex((v) => (v==line));

            if (index < _lines.Count - 1)
            {
                SwapLines(index, index + 1);
            }
        }

        private void MoveUp(TimetableLine line) 
        {
            int index = _lines.FindIndex((v) => (v == line));

            if (index > 0)
            {
                SwapLines(index, index - 1);
            }

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
            _lines.Clear();
        }
    }
}