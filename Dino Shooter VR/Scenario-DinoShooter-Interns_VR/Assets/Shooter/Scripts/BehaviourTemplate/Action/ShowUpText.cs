using System;
using U4K.BehaviourTemplate.Utils;
using U4K.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace U4K.BehaviourTemplate.Action
{
    public class ShowUpText : BasicAction
    {
        [SerializeField]
        private String Text = null;

        [SerializeField]
        private Color Color = Color.white;
        
        [SerializeField]
        private TextAnchor Alignment; //non-nullable

        private GameObject canvas;
        private GameObject textGo;
        private Text _text;

        private void Start()
        {
            canvas = UIUtil.Canvas();
            textGo = GameObject.Find(Alignment.ToString());
            if (textGo == null)
            {
                textGo = new GameObject(Alignment.ToString());
                textGo.SetActive(false);
                textGo.transform.SetParent(canvas.transform);
                var rect = textGo.AddComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(0, 0);
                rect.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
                var t = textGo.AddComponent<Text>();
                t.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                t.fontSize = 40;
                t.alignment = Alignment;
            }
            else
            {
                textGo.SetActive(false);
            }
            _text = textGo.GetComponent<Text>();
            _text.text = Text;
            _text.color = Color;
        }

        public override void Execute()
        {
            base.Execute();
            if (_text != null)
            {
                textGo.SetActive(true);
                _done = true;
            }
        }
        
        public new static string Name
        {
            get { return "Show Up Text"; }
        }
    }
}