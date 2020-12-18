using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timetable {
    public class TimetableMinigame : Minigame
    {
        private TimetableVariant _variant;
        [SerializeField] private TimetableGenerator _generator;
        [SerializeField] private TimetableLinesSpawner _spawner;

        private void PrepareMinigame()
        {
            _variant = _generator.GetVariant();
            _spawner.Spawn(_variant);
        }

        public override void EndGame() 
        {
            Camera.main.GetComponent<CameraController>().ForceChasing();
            _spawner.Flush();
        }
        public override void StartGame() 
        {
            PrepareMinigame();
            Camera.main.gameObject.GetComponent<CameraController>().SimulateCamera(_minigameCamera);
        }
    }
}