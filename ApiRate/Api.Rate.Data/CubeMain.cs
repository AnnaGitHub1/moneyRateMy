using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Api.Rate.Data
{
	[XmlRoot(ElementName = "Sender", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
	public class Sender
	{
		[XmlElement(ElementName = "name", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
		public string Name { get; set; }
	}

	[XmlRoot(ElementName = "Cube", Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
	public class Cube
	{
		[XmlArray(ElementName = "Cube")]
		public Cube[] Cubes { get; set; }

		[XmlAttribute(AttributeName = "currency")]
		public string Currency { get; set; }
		[XmlAttribute(AttributeName = "rate")]
		public decimal Rate { get; set; }
	}

	[XmlRoot(ElementName = "Envelope", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
	public class Envelope
	{
		[XmlElement(ElementName = "subject", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
		public string Subject { get; set; }
		[XmlElement(ElementName = "Sender", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
		public Sender Sender { get; set; }
		[XmlElement(ElementName = "Cube", Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
		public Cube Cube { get; set; }
		[XmlAttribute(AttributeName = "gesmes", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Gesmes { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}

}
