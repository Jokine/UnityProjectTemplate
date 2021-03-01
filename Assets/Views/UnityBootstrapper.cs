using System;
using System.Collections.Generic;
using UniAgile.Dependency;
using UniAgile.Game;
using UnityEngine;
using UnityProjectTemplate.Application;

namespace UnityProjectTemplate
{
    public class UnityBootstrapper : MonoBehaviour
    {
        [SerializeField]
        protected TextAsset BlowRunnerConfigurationJson;

        private       TimeSpan              DeltaTime;
        public static GameApplication StaticGameApplication { get; private set; }
        public        GameApplication GameApplication       { get; private set; }

        private void FixedUpdate()
        {
            GameApplication.Loop(DeltaTime);
        }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void BeforeSceneLoad()
        {
            var bootstrapper = FindObjectOfType<UnityBootstrapper>();

            if (bootstrapper == null)
            {
                Debug.LogWarning($"No {nameof(UnityBootstrapper)} found in the default scene. This is to be expected if running tests");
                return;
            }
            
            if (bootstrapper.BlowRunnerConfigurationJson == default) throw new Exception($"{nameof(BlowRunnerConfigurationJson)} has not been set");

            bootstrapper.Initialize(bootstrapper.BlowRunnerConfigurationJson.text.FromJson<GameConfiguration>());
        }

        public void Initialize(GameConfiguration blowRunnerConfiguration)
        {
            if (blowRunnerConfiguration.IsDefault()) throw new Exception($"{nameof(blowRunnerConfiguration)} has not been set");

            DeltaTime = TimeSpan.FromMilliseconds(blowRunnerConfiguration.DeltaTimeMs);

            var repositories = new IRepository[]
            {
            };

            var dependencyList = new List<IDependencyInfo>();

            var applicationModel                                = new ApplicationModel(repositories);
            GameApplication = StaticGameApplication = new GameApplication(applicationModel, dependencyList);
        }
    }
}