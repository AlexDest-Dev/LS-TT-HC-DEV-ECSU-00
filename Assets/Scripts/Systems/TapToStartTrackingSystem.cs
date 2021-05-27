using Components;
using Leopotam.Ecs;
using UnityEngine;
using Touch = Components.Touch;

namespace Systems
{
    public class TapToStartTrackingSystem : IEcsRunSystem
    {
        private EcsFilter<GameStopped, BeforeStart> _beforeStartFilter;
        private EcsFilter<TTS> _ttsFilter;
        private EcsFilter<Touch> _touchesFilter;
    
        public void Run()
        {
            if (_beforeStartFilter.IsEmpty() == false && _touchesFilter.IsEmpty() == false)
            {
                foreach (var ttsIndex in _ttsFilter)
                {
                    _ttsFilter.Get1(ttsIndex).TtsView.SetActive(false);
                }

                foreach (var beforeStartIndex in _beforeStartFilter)
                {
                    _beforeStartFilter.GetEntity(beforeStartIndex).Destroy();
                }
            }
        }
    }
}