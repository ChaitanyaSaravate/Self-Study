using System;
using System.Collections.Generic;

namespace Entities
{
    public class Building
    {
        private string _escalatorProvider;
        public string Name { get; set; }
        public string Id { get; set; }
        public uint Floors { get; set; }
        public bool IsEscalatorAvailable { get; private set; }
        public uint Capacity { get; }

        public string EscalatorProvider
        {
            get => _escalatorProvider;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                    throw new Exception($"{nameof(EscalatorProvider)} value is either null or empty.");

                _escalatorProvider = value;
                this.IsEscalatorAvailable = true;
            } 
        }

        public bool IsParkingSpaceAllotted { get; set; }
        
        public Building(string buildingName, string id, uint floors, bool isParkingSpaceAllotted)
        {
            if (string.IsNullOrWhiteSpace(buildingName))
            {
                throw new ArgumentNullException(nameof(buildingName));
            }

            if(string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Name = buildingName;
            this.Id = id;
            this.Floors = floors;
            this.IsParkingSpaceAllotted = isParkingSpaceAllotted;
        }
    }
}
