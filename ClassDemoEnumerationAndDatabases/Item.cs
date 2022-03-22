using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoEnumerationAndDatabases
{
    public class Item
    {
        private int _id;
        private string _name;
        private StatusType _status;
        private ColourType _colour;

        public Item(int id, string name, StatusType status, ColourType colour)
        {
            _id = id;
            _name = name;
            _status = status;
            _colour = colour;
        }

        public Item()
        {
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public StatusType Status
        {
            get => _status;
            set => _status = value;
        }

        public ColourType Colour
        {
            get => _colour;
            set => _colour = value;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Status)}: {Status}, {nameof(Colour)}: {Colour}";
        }
    }
}
