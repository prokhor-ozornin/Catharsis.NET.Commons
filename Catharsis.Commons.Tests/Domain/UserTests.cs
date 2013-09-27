﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="User"/>.</para>
  /// </summary>
  public sealed class UserTests : EntityUnitTests<User>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="User.DateCreated"/> property.</para>
    /// </summary>
    [Fact]
    public void DateCreated_Property()
    {
      Assert.True(new User { DateCreated = DateTime.MinValue }.DateCreated == DateTime.MinValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="User.Email"/> property.</para>
    /// </summary>
    [Fact]
    public void Email_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new User { Email = null });
      Assert.Throws<ArgumentException>(() => new User { Email = string.Empty });
      Assert.True(new User { Email = "email" }.Email == "email");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="User.LastUpdated"/> property.</para>
    /// </summary>
    [Fact]
    public void LastUpdated_Property()
    {
      Assert.True(new User { LastUpdated = DateTime.MaxValue }.LastUpdated == DateTime.MaxValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="User.Name"/> property.</para>
    /// </summary>
    [Fact]
    public void Name_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new User { Name = null });
      Assert.Throws<ArgumentException>(() => new User { Name = string.Empty });
      Assert.True(new User { Name = "name" }.Name == "name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="User.Username"/> property.</para>
    /// </summary>
    [Fact]
    public void Username_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new User { Username = null });
      Assert.Throws<ArgumentException>(() => new User { Username = string.Empty });
      Assert.True(new User { Username = "username" }.Username == "username");
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="User()"/>
    ///   <seealso cref="User(string, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var user = new User();
      Assert.True(user.Id == 0);
      Assert.True(user.DateCreated <= DateTime.UtcNow);
      Assert.True(user.Email == null);
      Assert.True(user.LastUpdated <= DateTime.UtcNow);
      Assert.True(user.Name == null);
      Assert.True(user.Username == null);

      Assert.Throws<ArgumentNullException>(() => new User(null, "email", "name"));
      Assert.Throws<ArgumentNullException>(() => new User("username", null, "name"));
      Assert.Throws<ArgumentNullException>(() => new User("username", "email", null));
      Assert.Throws<ArgumentException>(() => new User(string.Empty, "email", "name"));
      Assert.Throws<ArgumentException>(() => new User("username", string.Empty, "name"));
      Assert.Throws<ArgumentException>(() => new User("username", "email", string.Empty));
      user = new User("username", "email@mail.ru", "name");
      Assert.True(user.Id == 0);
      Assert.True(user.DateCreated <= DateTime.UtcNow);
      Assert.True(user.Email == "email@mail.ru");
      Assert.True(user.LastUpdated <= DateTime.UtcNow);
      Assert.True(user.Name == "name");
      Assert.True(user.Username == "username");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="User.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new User { Name = "Name" }.ToString() == "Name");
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="User.Equals(User)"/></description></item>
    ///     <item><description><see cref="User.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Username", "Username", "Username_2");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="User.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Username", "Username", "Username_2");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="User.CompareTo(User)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new User { Username = "Username" }.CompareTo(new User { Username = "Username"}) == 0);
      Assert.True(new User { Username = "a" }.CompareTo(new User { Username = "b" }) < 0);
    }

   /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="User.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="User.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => User.Xml(null));

      var xml = new XElement("User",
        new XElement("Id", 1),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Email", "email"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Username", "username"));
      var user = User.Xml(xml);
      Assert.True(user.Id == 1);
      Assert.True(user.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(user.Email == "email");
      Assert.True(user.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(user.Name == "name");
      Assert.True(user.Username == "username");
      Assert.True(new User("username", "email", "name") { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(User.Xml(user.Xml()).Equals(user));
    }
  }
}