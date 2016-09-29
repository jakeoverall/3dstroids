using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
    public static class GameExtensions
    {
        public static T FindComponet<T>(this GameObject that) where T : Component
        {
            var componet = that.GetComponent<T>();
            if (componet != null)
                return componet;

            var proxy = that.GetComponent<ColliderProxy>();
            if (proxy == null)
                return null;

            if (proxy.ProxyFor == null)
            {
                Debug.LogWarning("Collider did not specify what it was a proxy for", that);
                return null;
            }

            return proxy.ProxyFor.GetComponent<T>();
        }
    }
}
