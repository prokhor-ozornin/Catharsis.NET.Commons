﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Article"/>.</para>
  /// </summary>
  public sealed class ArticleTests : EntityUnitTests<Article>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Article.Annotation"/> property.</para>
    /// </summary>
    [Fact]
    public void Annotation_Property()
    {
      Assert.True(new Article { Annotation = "annotation" }.Annotation == "annotation");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Article.Category"/> property.</para>
    /// </summary>
    [Fact]
    public void Category_Property()
    {
      var category = new ArticlesCategory();
      Assert.True(ReferenceEquals(new Article { Category = category }.Category, category));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Article.Image"/> property.</para>
    /// </summary>
    [Fact]
    public void Image_Property()
    {
      var image = new Image();
      Assert.True(ReferenceEquals(new Article { Image = image }.Image, image));
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Article()"/>
    ///   <seealso cref="Article(IDictionary{string, object})"/>
    ///   <seealso cref="Article(string, string, ArticlesCategory, string, string, string, Image)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var article = new Article();
      Assert.True(article.Id == 0);
      Assert.True(article.AuthorId == null);
      Assert.True(article.DateCreated <= DateTime.UtcNow);
      Assert.True(article.Language == null);
      Assert.True(article.LastUpdated <= DateTime.UtcNow);
      Assert.True(article.Name == null);
      Assert.True(article.Text == null);
      Assert.True(article.Annotation == null);
      Assert.True(article.Category == null);
      Assert.True(article.Image == null);

      Assert.Throws<ArgumentNullException>(() => new Article(null));
      article = new Article(new Dictionary<string, object>()
        .AddNext("Id", 1)
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Annotation", "annotation")
        .AddNext("Category", new ArticlesCategory())
        .AddNext("Image", new Image()));
      Assert.True(article.Id == 1);
      Assert.True(article.AuthorId == "authorId");
      Assert.True(article.DateCreated <= DateTime.UtcNow);
      Assert.True(article.Language == "language");
      Assert.True(article.LastUpdated <= DateTime.UtcNow);
      Assert.True(article.Name == "name");
      Assert.True(article.Text == "text");
      Assert.True(article.Annotation == "annotation");
      Assert.True(article.Category != null);
      Assert.True(article.Image != null);

      Assert.Throws<ArgumentNullException>(() => new Article(null, "name"));
      Assert.Throws<ArgumentNullException>(() => new Article("language", null));
      Assert.Throws<ArgumentException>(() => new Article(string.Empty, "name"));
      Assert.Throws<ArgumentException>(() => new Article("language", string.Empty));
      article = new Article("language", "name", new ArticlesCategory(), "annotation", "text", "authorId", new Image());
      Assert.True(article.Id == 0);
      Assert.True(article.AuthorId == "authorId");
      Assert.True(article.DateCreated <= DateTime.UtcNow);
      Assert.True(article.Language == "language");
      Assert.True(article.LastUpdated <= DateTime.UtcNow);
      Assert.True(article.Name == "name");
      Assert.True(article.Text == "text");
      Assert.True(article.Annotation == "annotation");
      Assert.True(article.Category != null);
      Assert.True(article.Image != null);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Article.Equals(Article)"/></description></item>
    ///     <item><description><see cref="Article.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Category", new ArticlesCategory { Name = "Name" }, new ArticlesCategory { Name = "Name_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Article.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Category", new ArticlesCategory { Name = "Name" }, new ArticlesCategory { Name = "Name_2" });
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Article.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Article.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Article.Xml(null));

      var xml = new XElement("Article",
        new XElement("Id", 1),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"));
      var article = Article.Xml(xml);
      Assert.True(article.Id == 1);
      Assert.True(article.Annotation == null);
      Assert.True(article.AuthorId == null);
      Assert.True(article.Category == null);
      Assert.True(article.Comments.Count == 0);
      Assert.True(article.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(article.Image == null);
      Assert.True(article.Language == "language");
      Assert.True(article.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(article.Name == "name");
      Assert.True(article.Tags.Count == 0);
      Assert.True(article.Text == null);
      Assert.True(new Article("language", "name") { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Article.Xml(article.Xml()).Equals(article));

      xml = new XElement("Article",
        new XElement("Id", 1),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("Annotation", "annotation"),
        new XElement("ArticlesCategory",
          new XElement("Id", 2),
          new XElement("Language", "category.language"),
          new XElement("Name", "category.name")),
       new XElement("Image",
        new XElement("Id", 3),
        new XElement("File",
          new XElement("Id", 4),
          new XElement("ContentType", "image.file.contentType"),
          new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
          new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
          new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
          new XElement("Name", "image.file.name"),
          new XElement("OriginalName", "image.file.originalName"),
          new XElement("Size", Guid.Empty.ToByteArray().Length)),
        new XElement("Height", 1),
        new XElement("Width", 2)));
      article = Article.Xml(xml);
      Assert.True(article.Id == 1);
      Assert.True(article.Annotation == "annotation");
      Assert.True(article.AuthorId == "authorId");
      Assert.True(article.Category.Id == 2);
      Assert.True(article.Category.Language == "category.language");
      Assert.True(article.Category.Name == "category.name");
      Assert.True(article.Comments.Count == 0);
      Assert.True(article.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(article.Image.Id == 3);
      Assert.True(article.Image.File.Id == 4);
      Assert.True(article.Image.File.ContentType == "image.file.contentType");
      Assert.True(article.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(article.Image.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(article.Image.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(article.Image.File.Name == "image.file.name");
      Assert.True(article.Image.File.OriginalName == "image.file.originalName");
      Assert.True(article.Image.File.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(article.Image.Height == 1);
      Assert.True(article.Image.Width == 2);
      Assert.True(article.Language == "language");
      Assert.True(article.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(article.Name == "name");
      Assert.True(article.Tags.Count == 0);
      Assert.True(article.Text == "text");
      Assert.True(new Article("language", "name", new ArticlesCategory("category.language", "category.name") { Id = 2 }, "annotation", "text", "authorId", new Image(new File("image.file.contentType", "image.file.name", "image.file.originalName", Guid.Empty.ToByteArray()) { Id = 4, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2) { Id = 3 }) { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Article.Xml(article.Xml()).Equals(article));
    }
  }
}