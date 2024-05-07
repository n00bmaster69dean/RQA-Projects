using UnityEngine;
using System.Collections;

public class AIBrain : MonoBehaviour
{
    public NeuralNet neuralNet;

    public int inputNeurons;

    public int[] hiddenNeurons;

    public int outputNeurons;

    /// <summary>
    /// Create a Neural network
    /// </summary>
    public void Init()
    {
        neuralNet = new NeuralNet(inputNeurons, hiddenNeurons, outputNeurons);
    }

    /// <summary>
    /// Train the neural network with the specified input values and desiredOutput values.
    /// (The algorithm of Error Back Propagation)
    /// </summary>
    public void Train(float[] inputs, float[] desiredOutputs)
    {
        neuralNet.Train(inputs, desiredOutputs);
    }

    /// <summary>
    /// Give input values to calculate the return output values of the neural network
    /// </summary>
    public void Calculate(float[] inputs, out float[] outputs)
    {
        neuralNet.Calculate(inputs, out outputs);
    }

    /// <summary>
    /// Save each connection's weight of neural network
    /// </summary>
    public void SaveWeights()
    {
        neuralNet.SaveWeights();
    }

    /// <summary>
    /// Load each connection's weight of neural network
    /// </summary>
    public void LoadWeights()
    {
        neuralNet.LoadWeights();
    }
}
