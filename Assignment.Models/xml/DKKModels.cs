using System;
using System.Xml.Serialization;

namespace Assignment.Models.Xml
{
    [XmlRoot(ElementName = "currency")]
    public class Currency
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }

        [XmlAttribute(AttributeName = "desc")]
        public string Desc { get; set; }

        [XmlIgnore]
        public double Rate { get; set; }

        [XmlAttribute(AttributeName = "rate")]
        public string RateString
        {
            get => Rate.ToString(new System.Globalization.CultureInfo("da-DK"));
            set => Rate = double.Parse(value, new System.Globalization.CultureInfo("da-DK"));
        }
    }

    [XmlRoot(ElementName = "dailyrates")]
    public class Dailyrates
    {

        [XmlElement(ElementName = "currency")]
        public List<Currency> Currency { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public DateTime Id { get; set; }
    }

    [XmlRoot(ElementName = "exchangerates")]
    public class Exchangerates
    {

        [XmlElement(ElementName = "dailyrates")]
        public Dailyrates Dailyrates { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "author")]
        public string Author { get; set; }

        [XmlAttribute(AttributeName = "refcur")]
        public string Refcur { get; set; }

        [XmlAttribute(AttributeName = "refamt")]
        public int Refamt { get; set; }
    }
}