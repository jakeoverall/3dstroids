using UnityEngine;

namespace Assets.Code
{
    public static class GameResources
    {
        public static Texture Square = (Texture) Resources.Load("Square");
        public static Texture TargetReticle = (Texture) Resources.Load("TargetReticle");
        public static Texture TargetSquare = (Texture)Resources.Load("TargetSquare");
        public static Texture OffScreenIndicator = (Texture)Resources.Load("OffScreenIndicator");
        public static Texture TargetReticleFocus = (Texture)Resources.Load("TargetReticleFocus");
    }
}