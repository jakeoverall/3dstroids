using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
    public class EngineMount : MonoBehaviour
    {
        public IEnumerable<ParticleSystem> Jetstreams;
        public void Awake()
        {
            Jetstreams = GetComponentsInChildren<ParticleSystem>();
        }

    }
}
