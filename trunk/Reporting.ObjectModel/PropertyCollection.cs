using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{

    /// <summary>
    /// A name-value property collection
    /// Needed for the report filter custom properties (View & Filter tab)
    /// </summary>
	public class PropertyCollection : Collection<Property>
	{
        public Property this[string propertyName]
		{
			get
			{
				Property property = null;

				foreach (Property item in Items)
				{
					if (item.Name == propertyName)
					{
						property = item;
						break;
					}
				}

                //if (property == null)
                //{
                //    property = new Property(propertyName, string.Empty);
                //    Items.Add(property);
                //}

				return property;
			}
		}

        public new void Add (Property p)
        {
            // remove properties with the same name
            while (this[p.Name] != null)
            {
                this.Remove(this[p.Name]);
            }

            base.Add(p);

        }

	}

    

    public class Property
    {
        private string _name;
        private string _value;

        public Property() { }

        public Property(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

}
