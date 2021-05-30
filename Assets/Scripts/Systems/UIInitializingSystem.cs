using Components;
using EntitiesMonoBehaviour;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Systems
{
    public class UIInitializingSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private UIConfiguration _uiConfiguration;
        public void Init()
        {
            Canvas rootCanvas = CreateRootCanvas();

            CreateTimerUI(rootCanvas);
            CreateTtsUI(rootCanvas);
        }

        private void CreateTtsUI(Canvas rootCanvas)
        {
            GameObject ttsView = GameObject.Instantiate(_uiConfiguration.TapToStart, rootCanvas.transform);
            EcsEntity ttsUI = _world.NewEntity();
            ttsView.GetComponent<TapToStartEntityMonoBehaviour>().SetEcsEntity(ttsUI);
            ttsUI.Get<TTS>().TtsView = ttsView;
        }

        private void CreateTimerUI(Canvas rootCanvas)
        {
            EcsEntity timerUI = _world.NewEntity();
            Text timerUIView = GameObject.Instantiate(_uiConfiguration.TimerTextPrefab, rootCanvas.transform);
            timerUI.Get<TimerUI>().TimerUIView = timerUIView;
        }

        private Canvas CreateRootCanvas()
        {
            EcsEntity uiRootCanvas = _world.NewEntity();
            Canvas rootCanvasView = GameObject.Instantiate(_uiConfiguration.RootCanvasPrefab);
            uiRootCanvas.Get<RootCanvas>().RootCanvasView = rootCanvasView;
            return rootCanvasView;
        }
    }
}