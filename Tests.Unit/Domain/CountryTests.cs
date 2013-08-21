﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Country"/>.</para>
  /// </summary>
  public sealed class CountryTests : EntityUnitTests<Country>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Country.Image"/> property.</para>
    /// </summary>
    [Fact]
    public void Image_Property()
    {
      var image = new Image();
      Assert.True(ReferenceEquals(new Country { Image = image }.Image, image));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Country.IsoCode"/> property.</para>
    /// </summary>
    [Fact]
    public void IsoCode_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Country { IsoCode = null });
      Assert.Throws<ArgumentException>(() => new Country { IsoCode = string.Empty });
      Assert.True(new Country { IsoCode = "isoCode" }.IsoCode == "isoCode");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Country.Name"/> property.</para>
    /// </summary>
    [Fact]
    public void Name_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Country { Name = null });
      Assert.Throws<ArgumentException>(() => new Country { Name = string.Empty });
      Assert.True(new Country { Name = "name" }.Name == "name");
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Country()"/>
    ///   <seealso cref="Country(IDictionary{string, object})"/>
    ///   <seealso cref="Country(string, string, string, Image)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var country = new Country();
      Assert.True(country.Id == null);
      Assert.True(country.Image == null);
      Assert.True(country.IsoCode == null);
      Assert.True(country.Name == null);

      Assert.Throws<ArgumentNullException>(() => new Country(null));
      country = new Country(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Image", new Image())
        .AddNext("IsoCode", "isoCode")
        .AddNext("Name", "name"));
      Assert.True(country.Id == "id");
      Assert.True(country.Image != null);
      Assert.True(country.IsoCode == "isoCode");
      Assert.True(country.Name == "name");

      Assert.Throws<ArgumentNullException>(() => new Country(null, "name", "isoCode"));
      Assert.Throws<ArgumentNullException>(() => new Country("id", null, "isoCode"));
      Assert.Throws<ArgumentNullException>(() => new Country("id", "name", null));
      country = new Country("id", "name", "isoCode", new Image());
      Assert.True(country.Id == "id");
      Assert.True(country.Image != null);
      Assert.True(country.IsoCode == "isoCode");
      Assert.True(country.Name == "name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Country.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Country { Name = "name" }.ToString() == "name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="Country"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("IsoCode", new[] { "IsoCode", "IsoCode_2" }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Country.CompareTo(Country)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Country { Name = "Name" }.CompareTo(new Country { Name = "Name" }) == 0);
      Assert.True(new Country { Name = "First" }.CompareTo(new Country { Name = "Second" }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Country.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Country.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Country.Xml(null));

      var xml = new XElement("Country",
        new XElement("Id", "id"),
        new XElement("IsoCode", "isoCode"),
        new XElement("Name", "name"));
      var country = Country.Xml(xml);
      Assert.True(country.Id == "id");
      Assert.True(country.Image == null);
      Assert.True(country.IsoCode == "isoCode");
      Assert.True(country.Name == "name");
      Assert.True(new Country("id", "name", "isoCode").Xml().ToString() == xml.ToString());
      Assert.True(Country.Xml(country.Xml()).Equals(country));

      xml = new XElement("Country",
        new XElement("Id", "id"),
        new XElement("Image",
          new XElement("Id", "image.id"),
          new XElement("File",
            new XElement("Id", "image.file.id"),
            new XElement("ContentType", "image.file.contentType"),
            new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
            new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
            new XElement("Hash", Guid.Empty.ToByteArray().EncodeSHA512().EncodeHex()),
            new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
            new XElement("Name", "image.file.name"),
            new XElement("OriginalName", "image.file.originalName"),
            new XElement("Size", Guid.Empty.ToByteArray().LongLength)),
          new XElement("Height", 1),
          new XElement("Width", 2)),
        new XElement("IsoCode", "isoCode"),
        new XElement("Name", "name"));
      country = Country.Xml(xml);
      Assert.True(country.Id == "id");
      Assert.True(country.Image.Id == "image.id");
      Assert.True(country.Image.File.Id == "image.file.id");
      Assert.True(country.Image.File.ContentType == "image.file.contentType");
      Assert.True(country.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(country.Image.File.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(country.Image.File.Hash == Guid.Empty.ToByteArray().EncodeSHA512().EncodeHex());
      Assert.True(country.Image.File.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(country.Image.File.Name == "image.file.name");
      Assert.True(country.Image.File.OriginalName == "image.file.originalName");
      Assert.True(country.Image.File.Size == Guid.Empty.ToByteArray().LongLength);
      Assert.True(country.Image.Height == 1);
      Assert.True(country.Image.Width == 2);
      Assert.True(country.IsoCode == "isoCode");
      Assert.True(country.Name == "name");
      Assert.True(new Country("id", "name", "isoCode", new Image("image.id", new File("image.file.id", "image.file.contentType", "image.file.name", "image.file.originalName", Guid.Empty.ToByteArray()) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2)).Xml().ToString() == xml.ToString());
      Assert.True(Country.Xml(country.Xml()).Equals(country));
    }
  }
}