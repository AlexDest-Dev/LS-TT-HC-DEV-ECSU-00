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

        public void Run()
        {
            if (_beforeStartFilter.IsEmpty() == false)
            {
                foreach (var ttsIndex in _ttsFilter)
                {
                    TTS tapToStart = _ttsFilter.Get1(ttsIndex);
                    if (tapToStart.TtsView.GetComponent<TapToStartUIHandler>().IsClicked)
                    {
                        tapToStart.TtsView.SetActive(false);

                        foreach (var beforeStartIndex in _beforeStartFilter)
                        {
                            _beforeStartFilter.GetEntity(beforeStartIndex).Destroy();
                        }
                    }
                }
            }
        }
    }
}