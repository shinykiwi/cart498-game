using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Descant.Components
{
    [Serializable]
    public class Stats
    {
        private Dictionary<string, float> dictionary = new();
        public string json;

        // Serializes the dictionary into JSON string format
        private void Serialize()
        {
            json = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
        }

        public void AddEntry(string key, float value)
        {
            Debug.Log("Adding " + key + " to dictionary. \n");
            dictionary.Add(key, value);
            Serialize();
        }

        public void RemoveEntry(string key)
        {
            Debug.Log("Removing " + key + " from dictionary. \n");
            dictionary.Remove(key);
            Serialize();
        }

        public void SetEntry(string key, float newValue)
        {
            Debug.Log("Setting " + key + " to " + newValue + ".\n");
            dictionary[key] = newValue;
            Serialize();
        }

        public void IncreaseBy(string key, float value)
        {
            Debug.Log("Increasing " + key + " by " + value + ".\n");
            dictionary[key] += value;
            Serialize();
        }

        public void DecreaseBy(string key, float value)
        {
            Debug.Log("Decreasing " + key + " by " + value + ".\n");
            dictionary[key] -= value;
            Serialize();
        }

        public void Change(OperationType type, string key, float value)
        {
            switch (type)
            {
                case OperationType.IncreaseBy:
                    IncreaseBy(key, value);
                    break;
                case OperationType.DecreaseBy:
                    DecreaseBy(key, value);
                    break;
                case OperationType.Set:
                    SetEntry(key, value);
                    break;
            }
        }

        public bool Contains(string key)
        {
            string debug = dictionary.ContainsKey(key) ? "Stats.Contains(): Success. " + key + " was found."  : 
                "Stats.Contains(): Failed. " + key + " was not found.";
            
            Debug.Log(debug);
            return dictionary.ContainsKey(key);
        }

        public float GetValue(string key)
        {
            Debug.Log("Stats.GetValue(): Returning " + key + ".");
            return dictionary[key];
        }

        public override string ToString()
        {
            string str = "Stats:\n";
            foreach (KeyValuePair<string, float> entry in dictionary)
            {
                str += entry.Key + ": " + entry.Value + "\n";
            }

            return str;

        }
    }
    
    
}