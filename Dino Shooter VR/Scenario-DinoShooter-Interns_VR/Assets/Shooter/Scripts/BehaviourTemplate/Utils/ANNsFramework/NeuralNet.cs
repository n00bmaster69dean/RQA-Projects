using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The class of neuralNet
/// </summary>
public class NeuralNet
{
    private NeuronLayer m_InputNeuronLayer;//The input layer of neuralNet

    private NeuronLayer[] m_HiddenNeuronLayers;//The hidden layer of neuralNet

    private NeuronLayer m_OutputNeuronLayer;//The output layer of neuralNet

   /// <summary>
    /// Creates a neural net
   /// </summary>
    /// <param name="inputNeuronLayerNeuronsCount">In layer neuron count.</param>
    /// <param name="hiddenNeuronLayerArrayNeuronsCounts">Array containing the hidden layers neuron count.</param>
    /// <param name="outputNeuronLayerNeuronsCount">Out layer neuron count.</param>
    public NeuralNet(int inputNeuronLayerNeuronsCount, int[] hiddenNeuronLayerArrayNeuronsCounts, int outputNeuronLayerNeuronsCount)
    {
        if (hiddenNeuronLayerArrayNeuronsCounts.Length == 0)
        {
            throw new Exception("The neural network must have one ro morre hidden layer...");
        }

        //Create input layer
        m_InputNeuronLayer = new NeuronLayer(inputNeuronLayerNeuronsCount);

        //Create hidden layers
        m_HiddenNeuronLayers = new NeuronLayer[hiddenNeuronLayerArrayNeuronsCounts.Length];

        for (int i = 0; i < hiddenNeuronLayerArrayNeuronsCounts.Length; i++)
        {
            m_HiddenNeuronLayers[i] = new NeuronLayer(hiddenNeuronLayerArrayNeuronsCounts[i]);
        }

        //Create output layer
        m_OutputNeuronLayer = new NeuronLayer(outputNeuronLayerNeuronsCount);

        //Connect each neuron of inputlayer to the first hidden layer
        m_InputNeuronLayer.ConnectToNextNeuronLayer(m_HiddenNeuronLayers[0]);

        //Connect each neuron of hiddenlayer to next hidden layer
        for (int i = 0; i < m_HiddenNeuronLayers.Length - 1; i++)
        {
            m_HiddenNeuronLayers[i].ConnectToNextNeuronLayer(m_HiddenNeuronLayers[i + 1]);
        }

        //Connect each neuron of final hiddenlayer to the output layer
        m_HiddenNeuronLayers[m_HiddenNeuronLayers.Length - 1].ConnectToNextNeuronLayer(m_OutputNeuronLayer);
    }

    /// <summary>
    /// Train the neural network with the specified input values and desiredOutput values.
    /// (The algorithm of Error Back Propagation)
    /// </summary>
    public void Train(float[] inputs, float[] desiredOutputs)
    {
        if (m_InputNeuronLayer.CurrentNeuronLayerNeuronsList.Count != inputs.Length || m_OutputNeuronLayer.CurrentNeuronLayerNeuronsList.Count != desiredOutputs.Length)
        {
            throw new Exception("The length of train inputs array or outputs array was wrong");
        }

        //Let each value of inputs array assign to each neuron's input
        for (int i = 0; i < m_InputNeuronLayer.CurrentNeuronLayerNeuronsList.Count; i++)
        {
            m_InputNeuronLayer.CurrentNeuronLayerNeuronsList[i].Input = inputs[i];
        }

        ///////////////////Update each neuron's input of each layer/////////////////////////////////////////////

        m_InputNeuronLayer.UpdateEveryNeuronInputs();//Update each neuron's input of input layer

        m_InputNeuronLayer.UpdateEveryNeuronOutputs();//Update each neuron's output of input layer

        for (int i = 0; i < m_HiddenNeuronLayers.Length; i++)
        {
            m_HiddenNeuronLayers[i].UpdateEveryNeuronInputs();//Update each neuron's input of hidden layer

            m_HiddenNeuronLayers[i].UpdateEveryNeuronOutputs();//Update each neuron's output of hidden layer
        }

        m_OutputNeuronLayer.UpdateEveryNeuronInputs();//Update each neuron's input of hidden layer

        m_OutputNeuronLayer.UpdateEveryNeuronOutputs();////Update each neuron's output of hidden layer
        /////////////////////////////////////////////////////////////////////////////////////////////////


        //Let each value of desiredOutputs array assign to each neuron's desiredOutput
        for (int i = 0; i < m_OutputNeuronLayer.CurrentNeuronLayerNeuronsList.Count; i++)
        {
            m_OutputNeuronLayer.CurrentNeuronLayerNeuronsList[i].DesiredOutput = desiredOutputs[i];
        }

        ///////////////////Update each neuron's error of each layer/////////////////////////////////////////////

        m_OutputNeuronLayer.UpdateEveryNeuronErrors();//Update each neuron's error of output layer

        //Update each neuron's error of hidden layers
        for (int i = 0; i < m_HiddenNeuronLayers.Length; i++)
        {
            m_HiddenNeuronLayers[i].UpdateEveryNeuronErrors();
        }

        m_InputNeuronLayer.UpdateEveryNeuronErrors();//Update each neuron's error of input layer

        /////////////////////////////////////////////////////////////////////////////////////////////////


        ///////////////////Update each dendritic connection's weight of each neuron of each layer////////////////////////////////
        m_OutputNeuronLayer.UpdateEveryNeuronWeights();//Update each dendritic connection's weight of each neuron of output layer

        for (int i = 0; i < m_HiddenNeuronLayers.Length; i++)//Update each dendritic connection's weight of each neuron of hidden layers
        {
            m_HiddenNeuronLayers[i].UpdateEveryNeuronWeights();
        }

        m_InputNeuronLayer.UpdateEveryNeuronWeights();//Update each dendritic connection's weight of each neuron of input layer
        ///////////////////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// give input values to calculate the return output values of the neural network
    /// </summary>
    public void Calculate(float[] inputs, out float[] outputs)
    {
        //Let each value of inputs array assign to each neuron's input
        for (int i = 0; i < m_InputNeuronLayer.CurrentNeuronLayerNeuronsList.Count; i++)
        {
            m_InputNeuronLayer.CurrentNeuronLayerNeuronsList[i].Input = inputs[i];
        }

        ///////////////////Update each neuron's input of each layer/////////////////////////////////////////////

        m_InputNeuronLayer.UpdateEveryNeuronInputs();//Update each neuron's input of input layer

        m_InputNeuronLayer.UpdateEveryNeuronOutputs();//Update each neuron's output of input layer

        for (int i = 0; i < m_HiddenNeuronLayers.Length; i++) 
        {
            m_HiddenNeuronLayers[i].UpdateEveryNeuronInputs();  //Update each neuron's input of hidden layer

            m_HiddenNeuronLayers[i].UpdateEveryNeuronOutputs(); //Update each neuron's output of hidden layer
        }

        m_OutputNeuronLayer.UpdateEveryNeuronInputs();//Update each neuron's input of output layer

        m_OutputNeuronLayer.UpdateEveryNeuronOutputs(); //Update each neuron's output of output layer

        //////////////////////////////////////////////////////////////////////////////////////////////////

        //Let each neuron's output assign to each value of inputs array
        outputs = new float[m_OutputNeuronLayer.CurrentNeuronLayerNeuronsList.Count];

        for (int i = 0; i < m_OutputNeuronLayer.CurrentNeuronLayerNeuronsList.Count; i++)
        {
            outputs[i] = m_OutputNeuronLayer.CurrentNeuronLayerNeuronsList[i].Output;
        }
    }

    //Serialize each dendritic connection's weight of each neuron of each layer
    public void SaveWeights()
    {
        m_InputNeuronLayer.SaveEveryNeuronWeights("InputNeuronLayer");

        for (int i = 0; i < m_HiddenNeuronLayers.Length; i++)
        {
            m_HiddenNeuronLayers[i].SaveEveryNeuronWeights("HiddenNeuronLayer");
        }

        m_OutputNeuronLayer.SaveEveryNeuronWeights("OutputNeuronLayer");
    }

    //Deserialize each dendritic connection's weight of each neuron of each layer
    public void LoadWeights()
    {
        m_InputNeuronLayer.LoadEveryNeuronWeights("InputNeuronLayer");

        for (int i = 0; i < m_HiddenNeuronLayers.Length; i++)
        {
            m_HiddenNeuronLayers[i].LoadEveryNeuronWeights("HiddenNeuronLayer");
        }

        m_OutputNeuronLayer.LoadEveryNeuronWeights("OutputNeuronLayer");
    }

}


