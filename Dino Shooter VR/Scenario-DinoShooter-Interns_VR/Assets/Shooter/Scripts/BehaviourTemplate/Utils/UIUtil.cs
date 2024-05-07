using U4K.Utils;
using UnityEngine;

namespace U4K.BehaviourTemplate.Utils
{
    public class UIUtil
    {
        public static GameObject Canvas()
        {
            var canvas = GameObject.Find(CommonUtil.CANVAS);
            if (canvas == null)
            {
                canvas = new GameObject(CommonUtil.CANVAS);
                Canvas c = canvas.AddComponent<Canvas>();
                c.renderMode = RenderMode.ScreenSpaceOverlay;
            }
            return canvas;
        }
    }
}