using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityProjectTemplate.Application;

namespace UnityProjectTemplate.Tests.BootstrapperTests
{
    public class Unit
    {
        [UnityTest]
        public IEnumerator Bootstrapper_can_create_an_application()
        {
            // Use the Assert class to test conditions.

            var bootstrapper = new GameObject("UnityBoostrapper").AddComponent<UnityBootstrapper>();

            bootstrapper.Initialize(new GameConfiguration()
            {
                DeltaTimeMs = 166
            });
            
            Assert.NotNull(bootstrapper.GameApplication);
            Assert.NotNull(bootstrapper.GameApplication.ApplicationModel);
            Assert.NotNull(bootstrapper.GameApplication.DependencyService);
            yield break;
        }

    }
}