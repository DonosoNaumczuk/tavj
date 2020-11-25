using System;
using System.Collections.Generic;
using System.Linq;
using Networking;
using UnityEngine;

namespace Client
{
    public class Snapshot
    {
        private readonly Dictionary<int, (int, Vector3, Quaternion)> _transforms;

        public Snapshot(BitBuffer buffer)
        {
            _transforms = new Dictionary<int, (int, Vector3, Quaternion)>();
            for (var clientsToProcess = buffer.GetInt(); clientsToProcess > 0; clientsToProcess--)
            {
                var id = buffer.GetInt();
                var lastInputProcessed = buffer.GetInt();
                var position = new Vector3(buffer.GetFloat(), buffer.GetFloat(), buffer.GetFloat());
                var rotation = new Quaternion(buffer.GetFloat(), buffer.GetFloat(), buffer.GetFloat(), buffer.GetFloat());
                _transforms[id] = (lastInputProcessed, position, rotation);
            }
        }

        public List<int> Ids => _transforms.Keys.ToList();
    
        public bool Contains(int id)
        {
            return _transforms.ContainsKey(id);
        }
        
        public int GetLastInputProcessed(int id)
        {
            return _transforms[id].Item1;
        }

        public Vector3 GetPosition(int id)
        {
            return _transforms[id].Item2;
        }
        
        public Quaternion GetRotation(int id)
        {
            return _transforms[id].Item3;
        }
    }
}
