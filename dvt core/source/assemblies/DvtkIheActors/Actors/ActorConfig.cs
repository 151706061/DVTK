// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;
using System.Xml;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for ActorConfig.
	/// </summary>
	public class ActorConfig : CollectionBase
	{
		private ActorNameEnum _actorName;

		public ActorConfig(ActorNameEnum actorName)
		{
			_actorName = actorName;
		}

		public ActorNameEnum ActorName
		{
			get
			{
				return _actorName;
			}
		}	

		/// <summary>
		/// Gets or sets an <see cref="BaseConfig"/> from the collection.
		/// </summary>
		/// <param name="index">The zero-based index of the collection member to get or set.</param>
		/// <value>The <see cref="BaseConfig"/> at the specified index.</value>
		public BaseConfig this[int index]  
		{
			get  
			{
				return ((BaseConfig) List[index]);
			}
			set  
			{
				List[index] = value;
			}
		}

		/// <summary>
		/// Adds an object to the end of the <see cref="ActorConfig"/>.
		/// </summary>
		/// <param name="value">The <see cref="BaseConfig"/> to be added to the end of the <see cref="ActorConfig"/>.</param>
		/// <returns>The <see cref="ActorConfig"/> index at which the value has been added.</returns>
		public int Add(BaseConfig value)  
		{
			return (List.Add(value));
		}

		/// <summary>
		/// Searches for the specified <see cref="BaseConfig"/> and 
		/// returns the zero-based index of the first occurrence within the entire <see cref="ActorConfig"/>.
		/// </summary>
		/// <param name="value">The <see cref="BaseConfig"/> to locate in the <see cref="ActorConfig"/>.</param>
		/// <returns>
		/// The zero-based index of the first occurrence of value within the entire <see cref="ActorConfig"/>, 
		/// if found; otherwise, -1.
		/// </returns>
		public int IndexOf(BaseConfig value)  
		{
			return (List.IndexOf(value));
		}

		/// <summary>
		/// Inserts an <see cref="BaseConfig"/> element into the <see cref="ActorConfig"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">The <see cref="BaseConfig"/> to insert.</param>
		public void Insert(int index, BaseConfig value)  
		{
			List.Insert(index, value);
		}

		/// <summary>
		/// Removes the first occurrence of a specific <see cref="BaseConfig"/> from the <see cref="ActorConfig"/>.
		/// </summary>
		/// <param name="value">The <see cref="BaseConfig"/> to remove from the <see cref="ActorConfig"/>.</param>
		public void Remove(BaseConfig value)  
		{
			List.Remove(value);
		}

		/// <summary>
		/// Determines whether the <see cref="ActorConfig"/> contains a specific element.
		/// </summary>
		/// <param name="value">The <see cref="BaseConfig"/> to locate in the <see cref="ActorConfig"/>.</param>
		/// <returns>
		/// <c>true</c> if the <see cref="ActorConfig"/> contains the specified value; 
		/// otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(BaseConfig value)  
		{
			// If value is not of type Code, this will return false.
			return (List.Contains(value));
		}

		/// <summary>
		/// Performs additional custom processes before inserting a new element into the collection instance.
		/// </summary>
		/// <param name="index">The zero-based index at which to insert value.</param>
		/// <param name="value">The new value of the element at index.</param>
		protected override void OnInsert(int index, Object value)  
		{
			if (!(value is BaseConfig))
				throw new ArgumentException("value must be of type BaseConfig.", "value");
		}

		/// <summary>
		/// Performs additional custom processes when removing an element from the collection instance.
		/// </summary>
		/// <param name="index">The zero-based index at which value can be found.</param>
		/// <param name="value">The value of the element to remove from index.</param>
		protected override void OnRemove(int index, Object value)  
		{
			if (!(value is BaseConfig))
				throw new ArgumentException("value must be of type BaseConfig.", "value");
		}

		/// <summary>
		/// Performs additional custom processes before setting a value in the collection instance.
		/// </summary>
		/// <param name="index">The zero-based index at which oldValue can be found.</param>
		/// <param name="oldValue">The value to replace with newValue.</param>
		/// <param name="newValue">The new value of the element at index.</param>
		protected override void OnSet(int index, Object oldValue, Object newValue)  
		{
			if (!(newValue is BaseConfig))
				throw new ArgumentException("newValue must be of type BaseConfig.", "newValue");
		}

		/// <summary>
		/// Performs additional custom processes when validating a value.
		/// </summary>
		/// <param name="value">The object to validate.</param>
		protected override void OnValidate(Object value)  
		{
			if (!(value is BaseConfig))
				throw new ArgumentException("value must be of type BaseConfig.");
		}
	
		/// <summary>
		/// Copies the elements of the <see cref="ICollection"/> to a strong-typed <c>BaseConfig[]</c>, 
		/// starting at a particular <c>BaseConfig[]</c> index.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional <c>BaseConfig[]</c> that is the destination of the elements 
		/// copied from <see cref="ICollection"/>.
		/// The <c>BaseConfig[]</c> must have zero-based indexing. 
		/// </param>
		/// <param name="index">
		/// The zero-based index in array at which copying begins.
		/// </param>
		/// <remarks>
		/// Provides the strongly typed member for <see cref="ICollection"/>.
		/// </remarks>
		public void CopyTo(BaseConfig[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		public void WriteXmlConfig(XmlTextWriter writer)
		{
			writer.WriteStartElement("IheActorConfiguration");
			writer.WriteElementString("ActorName", ActorNames.Name(_actorName));
			foreach (BaseConfig baseConfig in this)
			{
				baseConfig.WriteXmlConfig(writer);
			}
			writer.WriteEndElement();
		}
	}
}
