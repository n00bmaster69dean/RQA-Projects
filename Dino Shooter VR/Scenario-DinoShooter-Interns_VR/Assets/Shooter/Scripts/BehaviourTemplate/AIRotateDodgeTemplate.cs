using U4K.BaseScripts;
using U4K.BehaviourTemplate.Action;
using U4K.BehaviourTemplate.Attr;
using U4K.Utils;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class AIRotateDodgeTemplate : BasicBehaviourTemplate
    {
        public override string GetHelpMsg()
        {
            return "";
        }

        [SerializeField] [FieldShownAs("Minimal distance")]
        private float minimalDistance = 0.2f;

        [SerializeField] [FieldShownAs("Maximal distance")]
        private float maximalDistance = 5.0f;
        
        [SerializeField] [FieldShownAs("Training algorithm")] private Alg alg;

        [SerializeField] [FieldShownAs("Training times")] private int trainLoops = 1000;
        
        [SerializeField] [FieldShownAs("Action when collide")] private BasicAction Action = null;

        private AIBrain AiBrain;

        private GameObject frontLeftSensor;

        private GameObject frontRightSensor;

        private GameObject leftSensor;

        private GameObject rightSensor;

        private float[] inputs;

        private float[] outputs;

        private void Start()
        {
            AiBrain = gameObject.AddComponent<AIBrain>();
            AiBrain.inputNeurons = 4;
            AiBrain.hiddenNeurons = new[] {5};
            AiBrain.outputNeurons = 1;
            AiBrain.Init();

            var frontMostLeftPosition = gameObject.transform.position;
            var frontMostRightPosition = gameObject.transform.position;
            var mostLeftPosition = gameObject.transform.position;
            var mostRightPosition = gameObject.transform.position;
            var custom = gameObject.GetComponent<CustomObjectComponent>();
            if (custom != null)
            {
                frontMostLeftPosition = custom.FrontMostLeftPosition();
                frontMostRightPosition = custom.FrontMostRigthPosition();
                mostLeftPosition = custom.LeftMostPosition();
                mostRightPosition = custom.RightMostPosition();
            }
            frontLeftSensor = new GameObject("frontLeftSensor");
            frontLeftSensor.transform.position = frontMostLeftPosition;
            frontLeftSensor.transform.forward = gameObject.transform.forward;
            frontLeftSensor.transform.parent = gameObject.transform;
            frontLeftSensor.transform.Rotate(new Vector3(0, -45, 0));

            frontRightSensor = new GameObject("frontRightSensor");
            frontRightSensor.transform.position = frontMostRightPosition;
            frontRightSensor.transform.forward = gameObject.transform.forward;
            frontRightSensor.transform.parent = gameObject.transform;
            frontRightSensor.transform.Rotate(new Vector3(0, 45, 0));
            
            leftSensor = new GameObject("leftSensor");
            leftSensor.transform.position = mostLeftPosition;
            leftSensor.transform.forward = gameObject.transform.forward;
            leftSensor.transform.parent = gameObject.transform;
            leftSensor.transform.Rotate(new Vector3(0, -90, 0));
            
            rightSensor = new GameObject("rightSensor");
            rightSensor.transform.position = mostRightPosition;
            rightSensor.transform.forward = gameObject.transform.forward;
            rightSensor.transform.parent = gameObject.transform;
            rightSensor.transform.Rotate(new Vector3(0, 90, 0));

            inputs = new float[4];
            outputs = new float[1];
        }

        private void Update()
        {
            inputs[0] = CommonUtil.GetDistance(frontLeftSensor.transform.position, frontLeftSensor.transform.forward);
            inputs[1] = CommonUtil.GetDistance(frontRightSensor.transform.position, frontRightSensor.transform.forward);
            inputs[2] = CommonUtil.GetDistance(leftSensor.transform.position, leftSensor.transform.forward);
            inputs[3] = CommonUtil.GetDistance(rightSensor.transform.position, rightSensor.transform.forward);

            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = CommonUtil.Range(minimalDistance, maximalDistance, inputs[i]);
            }

            if (Mathf.Approximately(inputs[0], 0))
            {
                for (int i = 0; i < trainLoops; i++)
                {
                    AiBrain.Train(inputs, new[] {1.0f});
                }
                if (Action != null)
                {
                    if (Action.UseDefaultActor)
                        Action.Actor = gameObject;
                    if (Action.Actor != null)
                        Action.Execute();
                }
                return;
            }
            if (Mathf.Approximately(inputs[1], 0))
            {
                for (int i = 0; i < trainLoops; i++)
                {
                    AiBrain.Train(inputs, new[] {0.0f});
                }
                if (Action != null)
                {
                    if (Action.UseDefaultActor)
                        Action.Actor = gameObject;
                    if (Action.Actor != null)
                        Action.Execute();
                }
                return;
            }

            if (Mathf.Approximately(inputs[2], 0))
            {
                for (int i = 0; i < trainLoops; i++)
                {
                    AiBrain.Train(inputs, new[] {0f});
                }

                if (Action != null)
                {
                    if (Action.UseDefaultActor)
                        Action.Actor = gameObject;
                    if (Action.Actor != null) 
                        Action.Execute();
                }

                return;
            }

            if (Mathf.Approximately(inputs[3], 0))
            {
                for (int i = 0; i < trainLoops; i++)
                {
                    AiBrain.Train(inputs, new []{1f});
                }

                if (Action != null)
                {
                    if (Action.UseDefaultActor) Action.Actor = gameObject;
                    if (Action.Actor != null) Action.Execute();
                }

                return;
            }

            AiBrain.Calculate(inputs, out outputs);

            float outputValue = outputs[0];

            if (outputValue < 0.2f)
            {
                transform.Rotate(0, (1 - CommonUtil.Range(0, 0.2f, outputValue)) * -100 * Time.deltaTime, 0);
            }
            else if (outputValue > 0.8f)
            {
                transform.Rotate(0, CommonUtil.Range(0.8f, 1, outputValue) * 100 * Time.deltaTime, 0);
            }
        }

        public new static string GetName()
        {
            return "AI Avoid Collider";
        }

        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.AI;
        }
    }

    public enum Alg
    {
        [FieldShownAs("Neural Net EBP")] NeuralNetEBP
    }
}