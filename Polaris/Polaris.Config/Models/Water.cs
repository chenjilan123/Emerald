using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polaris.Config.Models
{
    public class Water
    {
        public Color[] Color { get; set; }
    }

    public class Color
    {
        public string Value { get; set; }
    }


    //// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    ///// <remarks/>
    //[System.SerializableAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    //public partial class Water
    //{

    //    private WaterColor[] colorField;

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlElementAttribute("Color")]
    //    public WaterColor[] Color
    //    {
    //        get
    //        {
    //            return this.colorField;
    //        }
    //        set
    //        {
    //            this.colorField = value;
    //        }
    //    }
    //}

    ///// <remarks/>
    //[System.SerializableAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //public partial class WaterColor
    //{

    //    private string valueField;

    //    private string nameField;

    //    /// <remarks/>
    //    public string Value
    //    {
    //        get
    //        {
    //            return this.valueField;
    //        }
    //        set
    //        {
    //            this.valueField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlAttributeAttribute()]
    //    public string name
    //    {
    //        get
    //        {
    //            return this.nameField;
    //        }
    //        set
    //        {
    //            this.nameField = value;
    //        }
    //    }
    //}


}
