﻿// Copyright (c) 2015 James Liu
//	
// See the LISCENSE file for copying permission.

using UnityEngine;
using Vexe.Runtime.Types;

namespace Hourai.DanmakU.Collider {

    /// <summary>
    /// A DanmakuCollider that changes the direction of motion for all valid bullets that come into contact with it.
    /// </summary>
    [AddComponentMenu("Hourai.DanmakU/Colliders/Redirection Collider")]
    public class RedirectionCollider : DanmakuCollider {

        private DanmakuGroup affected;

        [SerializeField]
        private float angle;

        //TODO Document

        [SerializeField]
        private RotationMode rotationMode;

        [Serialize]
        public Transform Target { get; set; }

        public RotationMode RotationMode {
            get { return rotationMode; }
            set { rotationMode = value; }
        }

        public float Angle {
            get { return angle; }
            set { angle = value; }
        }

        /// <summary>
        /// Called on Component instantiation
        /// </summary>
        protected override void Awake() {
            base.Awake();
            affected = DanmakuGroup.Set();
        }

        #region implemented abstract members of DanmakuCollider

        /// <summary>
        /// Handles a Danmaku collision. Only ever called with Danmaku that pass the filter.
        /// </summary>
        /// <param name="danmaku">the danmaku that hit the collider.</param>
        /// <param name="info">additional information about the collision</param>
        protected override void DanmakuCollision(Danmaku danmaku,
                                                 RaycastHit2D info) {
            if (affected.Contains(danmaku))
                return;
            float baseAngle = angle;
            switch (rotationMode) {
                case RotationMode.Relative:
                    baseAngle += danmaku.Rotation;
                    break;
                case RotationMode.Object:
                    if (Target != null) {
                        baseAngle += DanmakuUtil.AngleBetween2D(
                                                                danmaku.Position,
                                                                Target.position);
                    } else {
                        Debug.LogWarning(
                                         "Trying to direct at an object but no Target object assinged");
                    }
                    break;
                case RotationMode.Absolute:
                    break;
            }
            danmaku.Rotation = baseAngle;
            affected.Add(danmaku);
        }

        #endregion
    }

}