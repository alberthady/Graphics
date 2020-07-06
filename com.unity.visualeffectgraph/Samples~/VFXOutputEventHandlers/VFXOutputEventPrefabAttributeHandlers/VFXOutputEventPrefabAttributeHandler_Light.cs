﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if HDRP
using UnityEngine.Rendering.HighDefinition;
#endif

namespace UnityEngine.VFX.Utility
{
    [RequireComponent(typeof(Light))]
#if HDRP
    [RequireComponent(typeof(HDAdditionalLightData))]
#endif
    public class VFXOutputEventPrefabAttributeHandler_Light : VFXOutputEventPrefabAttributeHandler
    {
        public float BrightnessScale = 1.0f;

        static readonly int kColor = Shader.PropertyToID("color");

        public override void OnVFXEventAttribute(VFXEventAttribute eventAttribute, VisualEffect visualEffect)
        {
            Vector3 color = eventAttribute.GetVector3(kColor);

            float intensity = color.magnitude;
            Color c = new Color(color.x, color.y, color.z) / intensity;

#if HDRP
            var hdlight = GetComponent<HDAdditionalLightData>();

            hdlight.SetColor(c);
            hdlight.intensity = intensity * BrightnessScale;
#else
            var light = GetComponent<Light>();
            light.color = c;
            light.intensity = intensity * BrightnessScale;
#endif
        }
    }
}
