using U4K.BaseScripts;
using U4K.BehaviourTemplate.Attr;
using U4K.BehaviourTemplate.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace U4K.BehaviourTemplate
{
    public class ShowVariableTemplate : BasicBehaviourTemplate
    {
        [SerializeField] [FieldSortIndex(0)] public string Context1 = "";

        [SerializeField] [FieldSortIndex(1)] public GameObject Variable = null;

        [SerializeField] [FieldSortIndex(2)] public string Context2 = "";

        [SerializeField] [FieldSortIndex(3)] private TextAnchor Alignment;

        [SerializeField] [FieldSortIndex(4)] private Color Color;

        private GameObject _canvas;

        private GameObject _textGo;

        private Text _text;

        public override string GetHelpMsg()
        {
            return "The <i>Variable</i> will be shown in the middle of <i>Context1</i> and <i>Context2</i>";
        }

        private void Start()
        {
            _canvas = UIUtil.Canvas();
            _textGo = new GameObject();
            _textGo.transform.SetParent(_canvas.transform);
            var rect = _textGo.AddComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, 0);
            rect.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
            _text = _textGo.AddComponent<Text>();
            _text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        }

        private void Update()
        {
            string vText = "";
            var numberContent = Variable.GetComponent<NumberContent>();
            if (numberContent != null)
            {
                vText = numberContent.number.ToString();
            }
            else
            {
                var boolContent = Variable.GetComponent<BooleanContent>();
                if (boolContent != null)
                {
                    vText = boolContent.value.ToString();
                }
            }

            _text.alignment = Alignment;
            _text.text = Context1 + " " + vText + " " + Context2;
            _text.fontSize = 40;
            _text.color = Color;
        }

        public new static string GetName()
        {
            return "Show Variable with Context";
        }

        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.UI;
        }
    }
}