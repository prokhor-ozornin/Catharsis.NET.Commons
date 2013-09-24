﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Poll"/>.</para>
  /// </summary>
  public sealed class PollTests : EntityUnitTests<Poll>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Poll.MultiSelect"/> property.</para>
    /// </summary>
    [Fact]
    public void MultiSelect_Property()
    {
      Assert.True(new Poll { MultiSelect = true }.MultiSelect);
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Poll()"/>
    ///   <seealso cref="Poll(IDictionary{string, object})"/>
    ///   <seealso cref="Poll(string, string, string, bool)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var poll = new Poll();
      Assert.True(poll.Id == 0);
      Assert.True(poll.AuthorId == null);
      Assert.True(poll.Comments.Count == 0);
      Assert.True(poll.DateCreated <= DateTime.UtcNow);
      Assert.True(poll.Language == null);
      Assert.True(poll.LastUpdated <= DateTime.UtcNow);
      Assert.False(poll.MultiSelect);
      Assert.True(poll.Name == null);
      Assert.True(poll.Tags.Count == 0);
      Assert.True(poll.Text == null);

      Assert.Throws<ArgumentNullException>(() => new Poll(null));
      poll = new Poll(new Dictionary<string, object>()
        .AddNext("Id", 1)
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("MultiSelect", true)
        .AddNext("Name", "name")
        .AddNext("Text", "text"));
      Assert.True(poll.Id == 1);
      Assert.True(poll.AuthorId == "authorId");
      Assert.True(poll.Comments.Count == 0);
      Assert.True(poll.DateCreated <= DateTime.UtcNow);
      Assert.True(poll.Language == "language");
      Assert.True(poll.LastUpdated <= DateTime.UtcNow);
      Assert.True(poll.MultiSelect);
      Assert.True(poll.Name == "name");
      Assert.True(poll.Tags.Count == 0);
      Assert.True(poll.Text == "text");

      Assert.Throws<ArgumentNullException>(() => new Poll(null, "name", "text", true));
      Assert.Throws<ArgumentNullException>(() => new Poll("language", null, "text", true));
      Assert.Throws<ArgumentNullException>(() => new Poll("language", "name", null, true));
      Assert.Throws<ArgumentException>(() => new Poll(string.Empty, "name", "text", true));
      Assert.Throws<ArgumentException>(() => new Poll("language", string.Empty, "text", true));
      Assert.Throws<ArgumentException>(() => new Poll("language", "name", string.Empty, true));
      poll = new Poll("language", "name", "text", true);
      Assert.True(poll.Id == 0);
      Assert.True(poll.AuthorId == null);
      Assert.True(poll.Comments.Count == 0);
      Assert.True(poll.DateCreated <= DateTime.UtcNow);
      Assert.True(poll.Language == "language");
      Assert.True(poll.LastUpdated <= DateTime.UtcNow);
      Assert.True(poll.MultiSelect);
      Assert.True(poll.Name == "name");
      Assert.True(poll.Tags.Count == 0);
      Assert.True(poll.Text == "text");
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Poll.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Poll.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Poll.Xml(null));

      var xml = new XElement("Poll",
        new XElement("Id", 1),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("MultiSelect", true));
      var poll = Poll.Xml(xml);
      Assert.True(poll.Id == 1);
      Assert.True(poll.Answers.Count == 0);
      Assert.True(poll.AuthorId == null);
      Assert.True(poll.Comments.Count == 0);
      Assert.True(poll.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(poll.Language == "language");
      Assert.True(poll.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(poll.Name == "name");
      Assert.True(poll.Tags.Count == 0);
      Assert.True(poll.Text == "text");
      Assert.True(new Poll("language", "name", "text", true) { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Poll.Xml(poll.Xml()).Equals(poll));
    }
  }
}