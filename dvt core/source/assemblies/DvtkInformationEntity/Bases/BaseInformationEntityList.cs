// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;

namespace Dvtk.Dicom.InformationEntity
{
	/// <summary>
	/// Summary description for BaseInformationEntityList.
	/// </summary>
	public class BaseInformationEntityList : CollectionBase
	{
		/// <summary>
		/// Gets or sets an <see cref="BaseInformationEntity"/> from the collection.
		/// </summary>
		/// <param name="index">The zero-based index of the collection member to get or set.</param>
		/// <value>The <see cref="BaseInformationEntityList"/> at the specified index.</value>
		public BaseInformationEntity this[int index]  
		{
			get  
			{
				return ((BaseInformationEntity) List[index]);
			}
			set  
			{
				List[index] = value;
			}
		}

		/// <summary>
		/// Adds an object to the end of the <see cref="BaseInformationEntityList"/>.
		/// </summary>
		/// <param name="value">The <see cref="BaseInformationEntity"/> to be added to the end of the <see cref="BaseInformationEntityList"/>.</param>
		/// <returns>The <see cref="BaseInformationEntityList"/> index at which the value has been added.</returns>
		public int Add(BaseInformationEntity value)  
		{
			return (List.Add(value));
		}

		/// <summary>
		/// Searches for the specified <see cref="BaseInformationEntity"/> and 
		/// returns the zero-based index of the first occurrence within the entire <see cref="BaseInformationEntityList"/>.
		/// </summary>
		/// <param name="value">The <see cref="BaseInformationEntity"/> to locate in the <see cref="BaseInformationEntityList"/>.</param>
		/// <returns>
		/// The zero-based index of the first occurrence of value within the entire <see cref="BaseInformationEntityList"/>, 
		/// if found; otherwise, -1.
		/// </returns>
		public int IndexOf(BaseInformationEntity value)  
		{
			return (List.IndexOf(value));
		}

		/// <summary>
		/// Inserts an <see cref="BaseInformationEntity"/> element into the <see cref="BaseInformationEntity"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">The <see cref="BaseInformationEntityList"/> to insert.</param>
		public void Insert(int index, BaseInformationEntity value)  
		{
			List.Insert(index, value);
		}

		/// <summary>
		/// Removes the first occurrence of a specific <see cref="BaseInformationEntity"/> from the <see cref="BaseInformationEntityList"/>.
		/// </summary>
		/// <param name="value">The <see cref="BaseInformationEntity"/> to remove from the <see cref="BaseInformationEntityList"/>.</param>
		public void Remove(BaseInformationEntity value)  
		{
			List.Remove(value);
		}

		/// <summary>
		/// Determines whether the <see cref="BaseInformationEntityList"/> contains a specific element.
		/// </summary>
		/// <param name="value">The <see cref="BaseInformationEntity"/> to locate in the <see cref="BaseInformationEntityList"/>.</param>
		/// <returns>
		/// <c>true</c> if the <see cref="BaseInformationEntityList"/> contains the specified value; 
		/// otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(BaseInformationEntity value)  
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
			if (!(value is BaseInformationEntity))
				throw new ArgumentException("value must be of type BaseInformationEntity.", "value");
		}

		/// <summary>
		/// Performs additional custom processes when removing an element from the collection instance.
		/// </summary>
		/// <param name="index">The zero-based index at which value can be found.</param>
		/// <param name="value">The value of the element to remove from index.</param>
		protected override void OnRemove(int index, Object value)  
		{
			if (!(value is BaseInformationEntity))
				throw new ArgumentException("value must be of type BaseInformationEntity.", "value");
		}

		/// <summary>
		/// Performs additional custom processes before setting a value in the collection instance.
		/// </summary>
		/// <param name="index">The zero-based index at which oldValue can be found.</param>
		/// <param name="oldValue">The value to replace with newValue.</param>
		/// <param name="newValue">The new value of the element at index.</param>
		protected override void OnSet(int index, Object oldValue, Object newValue)  
		{
			if (!(newValue is BaseInformationEntity))
				throw new ArgumentException("newValue must be of type BaseInformationEntity.", "newValue");
		}

		/// <summary>
		/// Performs additional custom processes when validating a value.
		/// </summary>
		/// <param name="value">The object to validate.</param>
		protected override void OnValidate(Object value)  
		{
			if (!(value is BaseInformationEntity))
				throw new ArgumentException("value must be of type BaseInformationEntity.");
		}
	
		/// <summary>
		/// Copies the elements of the <see cref="ICollection"/> to a strong-typed <c>BaseInformationEntity[]</c>, 
		/// starting at a particular <c>BaseInformationEntity[]</c> index.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional <c>BaseInformationEntity[]</c> that is the destination of the elements 
		/// copied from <see cref="ICollection"/>.
		/// The <c>BaseInformationEntity[]</c> must have zero-based indexing. 
		/// </param>
		/// <param name="index">
		/// The zero-based index in array at which copying begins.
		/// </param>
		/// <remarks>
		/// Provides the strongly typed member for <see cref="ICollection"/>.
		/// </remarks>
		public void CopyTo(BaseInformationEntity[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}
	}
}
