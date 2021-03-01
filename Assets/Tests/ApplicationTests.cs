using System.Collections;
using System.Collections.Specialized;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace UnityProjectTemplate.Application.Tests
{
    public class Integration
    {
        private UnityBootstrapper UnityBootstrapper;

        [SetUp]
        public void Setup()
        {
            UnityBootstrapper = new GameObject("UnityBoostrapper").AddComponent<UnityBootstrapper>();

            UnityBootstrapper.Initialize(new GameConfiguration()
            {
                DeltaTimeMs = 166
            });
        }
        
        [UnityTest]
        public IEnumerator Application_notifies_data_changes_each_frame()
        {
            var dataKey  = "test";
            var appModel = UnityBootstrapper.GameApplication.ApplicationModel;

            var mock = new Mock<NotifyCollectionChangedEventHandler>();
            appModel[dataKey].CollectionChanged += mock.Object;


            appModel.GetRepository<int>()[dataKey] = 69;
            
            mock.Verify(m => m.Invoke(It.IsAny<object>(), It.IsAny<NotifyCollectionChangedEventArgs>()), Times.Never);

            yield return new WaitForFixedUpdate();
            mock.Verify(m => m.Invoke(It.IsAny<object>(), It.IsAny<NotifyCollectionChangedEventArgs>()), Times.Once);
            
            yield return new WaitForFixedUpdate();
            mock.Verify(m => m.Invoke(It.IsAny<object>(), It.IsAny<NotifyCollectionChangedEventArgs>()), Times.Once);
            
            appModel.GetRepository<int>()[dataKey] = 666;
            yield return new WaitForFixedUpdate();
            mock.Verify(m => m.Invoke(It.IsAny<object>(), It.IsAny<NotifyCollectionChangedEventArgs>()), Times.Exactly(2));
        }
    }
}