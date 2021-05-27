using Components;
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
            CreateDefeatUI(rootCanvas);
            CreateVictoryUI(rootCanvas);
        }
        
        //TODO: remove duplicating of code - make victory and defeat views as abstraction
        private void CreateVictoryUI(Canvas rootCanvas)
        {
            EcsEntity victoryScreen = _world.NewEntity();
            GameObject victoryScreenView =
                GameObject.Instantiate(_uiConfiguration.victoryScreenPrefab, rootCanvas.transform);
            victoryScreenView.SetActive(false);
            victoryScreen.Get<VictoryScreen>().VictoryScreenView = victoryScreenView;
        }

        private void CreateDefeatUI(Canvas rootCanvas)
        {
            EcsEntity defeatScreen = _world.NewEntity();
            GameObject defeatScreenView =
                GameObject.Instantiate(_uiConfiguration.defeatScreenPrefab, rootCanvas.transform);
            defeatScreenView.SetActive(false);
            defeatScreen.Get<DefeatScreen>().DefeatScreenView = defeatScreenView;
        }

        private void CreateTtsUI(Canvas rootCanvas)
        {
            EcsEntity ttsUI = _world.NewEntity();
            GameObject ttsView = GameObject.Instantiate(_uiConfiguration.tapToStart, rootCanvas.transform);
            ttsUI.Get<TTS>().TtsView = ttsView;
        }

        private void CreateTimerUI(Canvas rootCanvas)
        {
            EcsEntity timerUI = _world.NewEntity();
            Text timerUIView = GameObject.Instantiate(_uiConfiguration.timerTextPrefab, rootCanvas.transform);
            timerUI.Get<TimerUI>().TimerUIView = timerUIView;
        }

        private Canvas CreateRootCanvas()
        {
            EcsEntity uiRootCanvas = _world.NewEntity();
            Canvas rootCanvasView = GameObject.Instantiate(_uiConfiguration.rootCanvasPrefab);
            uiRootCanvas.Get<RootCanvas>().RootCanvasView = rootCanvasView;
            return rootCanvasView;
        }
    }
}