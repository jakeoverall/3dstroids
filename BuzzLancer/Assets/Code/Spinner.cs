using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
    public class Spinner : MonoBehaviour
    {
        public bool Direction;

        public void Update()
        {
            transform.Rotate(0, Time.deltaTime * 20 * (Direction ? 1 : -1), 0, Space.Self);
        }
    }
}
