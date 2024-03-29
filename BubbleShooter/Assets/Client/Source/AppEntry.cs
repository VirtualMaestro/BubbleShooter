﻿using Client.Source.Gameplay.Services;
using Client.Source.Hex;
using Client.Source.Physics;
using Client.Source.SOConfigs;
using StubbUnity.StubbFramework.Core;
using StubbUnity.StubbFramework.Physics;
using StubbUnity.Unity;
using UnityEngine;

namespace Client.Source
{
    public class AppEntry : EntryPoint
    {
        [SerializeField] private AppSettings appSettings;
        
        protected override void OnConstruct(IStubbContext context)
        {
            base.OnConstruct(context);
            context.MainFeature = new MainFeature();
        }
        
        protected override void OnInitialize(IStubbContext context)
        {
            context.Inject(appSettings);
            context.Inject(new HexGrid());
            context.Inject(new FactoryService(appSettings));
        }
        
        protected override IPhysicsContext CreatePhysicsContext()
        {
            var physicsContext = new PhysicsContext(World)
            {
                HeadFeature = new PhysicsFeature()
            };

            return physicsContext;
        }
    }
}