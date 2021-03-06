using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniAgile.Dependency;
using UniAgile.Game;
#pragma warning disable 1998

namespace UnityProjectTemplate.Application
{
    public class GameApplication : UniAgile.Game.Application
    {
        public GameApplication(ApplicationModel      applicationModel,
                                     List<IDependencyInfo> dependencyList) : base(applicationModel, dependencyList)
        {
        }

        public override Task Loop(TimeSpan deltaTime)
        {
            ApplicationModel.NotifyChanges();
            return Task.CompletedTask;
        }

        public override void Reset()
        {
        }

        public override void Start()
        {
        }
    }
}