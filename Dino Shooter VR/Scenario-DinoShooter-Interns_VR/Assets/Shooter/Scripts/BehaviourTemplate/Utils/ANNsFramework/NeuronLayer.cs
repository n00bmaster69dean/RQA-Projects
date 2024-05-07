using System;
using System.Collections.Generic;

/// <summary>
/// The class of neuron network layer
/// </summary>
public class NeuronLayer
{
    private NeuronLayer m_PreviousNeuronLayer;//The previous neuron layer

    private NeuronLayer m_NextNeuronLayer;//The next neuron layer

    private List<Neuron> m_CurrentNeuronLayerNeuronsList = new List<Neuron>();//This layer's neurons list 

    public NeuronLayer PreviousNeuronLayer
    {
        set { m_PreviousNeuronLayer = value; }
        get { return m_PreviousNeuronLayer; }
    }
    public NeuronLayer NextNeuronLayer
    {
        set { m_NextNeuronLayer = value; }
        get { return m_NextNeuronLayer; }
    }
    public List<Neuron> CurrentNeuronLayerNeuronsList
    {
        set { m_CurrentNeuronLayerNeuronsList = value; }
        get { return m_CurrentNeuronLayerNeuronsList; }
    }

    /// <summary>
    /// The constructor of this layer
    /// </summary>
    public NeuronLayer() { }
    /// <summary>
    /// The constructor of this layer with a parameter
    /// </summary>
    public NeuronLayer(int num)
    {
        for (int i = 0; i < num; i++)
        {
            m_CurrentNeuronLayerNeuronsList.Add(new Neuron());
        }
    }

    /// <summary>
    /// Connect each neuron of this layer to each neuron of next layer
    /// </summary>
    public void ConnectToNextNeuronLayer(NeuronLayer nextNeuronLayer)
    {
        m_NextNeuronLayer = nextNeuronLayer;

        m_NextNeuronLayer.PreviousNeuronLayer = this;

        for (int i = 0; i < m_CurrentNeuronLayerNeuronsList.Count; i++)
        {
            for (int j = 0; j < m_NextNeuronLayer.CurrentNeuronLayerNeuronsList.Count; j++)
            {
                m_CurrentNeuronLayerNeuronsList[i].ConnectToNextNeuron(m_NextNeuronLayer.CurrentNeuronLayerNeuronsList[j]);
            }
        }
    }

    /// <summary>
    /// Update each neuron's input
    /// </summary>
    public void UpdateEveryNeuronInputs()
    {
        for (int i = 0; i < m_CurrentNeuronLayerNeuronsList.Count; i++)
        {
            m_CurrentNeuronLayerNeuronsList[i].UpdateInput();
        }
    }

    /// <summary>
    /// Update each neuron's output
    /// </summary>
    public void UpdateEveryNeuronOutputs()
    {
        for (int i = 0; i < m_CurrentNeuronLayerNeuronsList.Count; i++)
        {
            m_CurrentNeuronLayerNeuronsList[i].UpdateOutput();
        }
    }

    /// <summary>
    /// Update each neuron's error
    /// </summary>
    public void UpdateEveryNeuronErrors()
    {
        for (int i = 0; i < m_CurrentNeuronLayerNeuronsList.Count; i++)
        {
            m_CurrentNeuronLayerNeuronsList[i].UpdateError();
        }
    }

    /// <summary>
    /// Update weight of each neuron's connection
    /// </summary>
    public void UpdateEveryNeuronWeights()
    {
        for (int i = 0; i < m_CurrentNeuronLayerNeuronsList.Count; i++)
        {
            m_CurrentNeuronLayerNeuronsList[i].UpdateNeuronConnectionsWeight();
        }
    }

    /// <summary>
    /// Serialize weight of each neuron's connection
    /// </summary>
    /// <param name="layerIndex"></param>
    public void SaveEveryNeuronWeights(string layerIndex)
    {
        for (int i = 0; i < m_CurrentNeuronLayerNeuronsList.Count; i++)
        {
            m_CurrentNeuronLayerNeuronsList[i].SaveNeuronWeights(layerIndex,i);
        }
    }

    /// <summary>
    /// Deserialize weight of each neuron's connection
    /// </summary>
    /// <param name="layerIndex"></param>
    public void LoadEveryNeuronWeights(string layerIndex)
    {
        for (int i = 0; i < m_CurrentNeuronLayerNeuronsList.Count; i++)
        {
            m_CurrentNeuronLayerNeuronsList[i].LoadNeuronWeights(layerIndex,i);
        }
    }
}
