using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELO.Utilities
{
    public class SelectItem<TValue> : SelectKeyValue<TValue>
    {
        public string Icon { get; set; }
        public bool Disabled { get; set; } = false;
        public bool Selected { get; set; } = false;
        public bool Visible { get; set; } = true;
        public dynamic Group { get; set; }
        public dynamic Data { get; set; }


        public SelectItem() { }
        public SelectItem(string _text, TValue _value)
        {
            Text = _text;
            Value = _value;
        }
        public SelectItem(string _text, TValue _value, string _group)
        {
            Text = _text;
            Value = _value;
            Group = _group;
        }

        public override string ToString()
            => $"{Text} {Value}";
    }

    public class SelectItem : SelectItem<string>
    {
        public SelectItem() : base() { }
        public SelectItem(string _text, string _value) : base(_text, _value) { }
        public SelectItem(string _text, string _value, string _group) : base(_text, _value, _group) { }
    }

    public class SelectKeyValue<TValue>
    {
        public string Text { get; set; } = "";
        public TValue Value { get; set; }


        public SelectKeyValue() { }
        public SelectKeyValue(string _text, TValue _value)
        {
            Text = _text;
            Value = _value;
        }

        public override string ToString()
            => $"{Text} {Value}";
    }

    public class SelectKeyValue : SelectKeyValue<string>
    {
        public SelectKeyValue() : base() { }
        public SelectKeyValue(string _text, string _value) : base(_text, _value) { }
    }
}