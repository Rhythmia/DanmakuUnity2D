// Copyright (c) 2015 James Liu
//	
// See the LISCENSE file for copying permission.

using UnityEngine;


namespace DanmakU {

	[System.Serializable]
	public class LinearBurstModifier : DanmakuModifier {

		public DynamicInt Depth = 1;
		public DynamicFloat DeltaVelocity = 0f;
		public DynamicFloat DeltaAngularVelocity = 0f;

		#region implemented abstract members of FireModifier
		public override void Fire (Vector2 position, DynamicFloat rotation) {
			float deltaV = DeltaVelocity.Value;
			float deltaAV = DeltaAngularVelocity.Value;
			float depth = Depth.Value;
			for(int i = 0; i < depth; i++) {
				Speed += deltaV;
				AngularSpeed += deltaAV;
				FireSingle(position, rotation);
			}

		}
		#endregion
	}
}