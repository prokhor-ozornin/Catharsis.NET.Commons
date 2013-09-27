﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Language,Name,Translator")]
  public class TextTranslation : EntityBase, IEquatable<TextTranslation>, ILocalizable, INameable, ITextable
  {
    private string language;
    private string name;
    private string text;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Language
    {
      get { return this.language; }
      set
      {
        Assertion.NotEmpty(value);

        this.language = value;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Name
    {
      get { return this.name; }
      set
      {
        Assertion.NotEmpty(value);

        this.name = value;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Text
    {
      get { return this.text; }
      set
      {
        Assertion.NotEmpty(value);

        this.text = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual string Translator { get; set; }

    /// <summary>
    ///   <para>Creates new translation.</para>
    /// </summary>
    public TextTranslation()
    {
    }

    /// <summary>
    ///   <para>Creates new translation.</para>
    /// </summary>
    /// <param name="language">ISO language code of translation's text content.</param>
    /// <param name="name">Title of translation.</param>
    /// <param name="text">Translation's content text.</param>
    /// <param name="translator"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public TextTranslation(string language, string name, string text, string translator = null)
    {
      this.Language = language;
      this.Name = name;
      this.Text = text;
      this.Translator = translator;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static TextTranslation Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var translation = new TextTranslation((string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), (string) xml.Element("Translator"));
      if (xml.Element("Id") != null)
      {
        translation.Id = (long) xml.Element("Id");
      }
      return translation;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(TextTranslation other)
    {
      return base.Equals(other);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return this.Name;
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="TextTranslation"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("Language", this.Language),
        new XElement("Name", this.Name),
        new XElement("Text", this.Text),
        this.Translator != null ? new XElement("Translator", this.Translator) : null);
    }
  }
}