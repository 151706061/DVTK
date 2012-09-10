// Part of DvtkData.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;
using System.IO;

using DvtkData.Validation;
using DvtkData.DvtDetailToXml;
using DvtkData.Dimse;
using DvtkData.Validation.TypeSafeCollections;

namespace DvtkData.ComparisonResults
{
	/// <summary>
	/// Summary description for AttributeComparisonResults.
	/// </summary>
	public class AttributeComparisonResults : IDvtDetailToXml, IDvtSummaryToXml
	{
		private Tag _Tag = null;
		private System.String _Name = System.String.Empty;
		private System.String _Value1 = System.String.Empty;
		private System.String _Value2 = System.String.Empty;

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="tag">Attribute Tag.</param>
		/// <param name="value1">First Compared Attribute Value.</param>
		/// <param name="value2">Second Compared Attribute Value.</param>
		public AttributeComparisonResults(Tag tag, System.String value1, System.String value2)
		{
			_Tag = tag;
			_Value1 = value1;
			_Value2 = value2;
		}

		/// <summary>
		/// Tag property - Get only
		/// </summary>
		public Tag Tag
		{
			get
			{
				return _Tag;
			}
		}

		/// <summary>
		/// Name property - Set only
		/// </summary>
		public System.String Name
		{
			set
			{
				_Name = value;
			}
		}

		/// <summary>
		/// Validation informative messages.
		/// </summary>
		public ValidationMessageCollection Messages 
		{
			get 
			{ 
				return _Messages; 
			}
		} 
		private ValidationMessageCollection _Messages
			= new ValidationMessageCollection();

		/// <summary>
		/// Serialize DVT Detail Data to Xml.
		/// </summary>
		/// <param name="streamWriter">Stream writer to serialize to.</param>
		/// <param name="level">Recursion level. 0 = Top.</param>
		/// <returns>bool - success/failure</returns>
		public bool DvtDetailToXml(StreamWriter streamWriter, int level)
		{
			if (streamWriter != null)
			{
				streamWriter.WriteLine("<AttributeComparisonResults>");
				string group = _Tag.GroupNumber.ToString("X");
				while (group.Length < 4)
				{
					group = "0" + group;
				}
				string element = _Tag.ElementNumber.ToString("X");
				while (element.Length < 4)
				{
					element = "0" + element;
				}
				streamWriter.WriteLine("<AttributeComparison Id=\"({0},{1})\" Name=\"{2}\">", group, element, DvtToXml.ConvertString(_Name));
				streamWriter.WriteLine("<Value1>{0}</Value1>", _Value1);
				streamWriter.WriteLine("<Value2>{0}</Value2>", _Value2);
				streamWriter.WriteLine("</AttributeComparison>");
				Messages.DvtDetailToXml(streamWriter, level);
				streamWriter.WriteLine("</AttributeComparisonResults>");
			}
			return true;
		}    

		/// <summary>
		/// Serialize DVT Summary Data to Xml.
		/// </summary>
		/// <param name="streamWriter">Stream writer to serialize to.</param>
		/// <param name="level">Recursion level. 0 = Top.</param>
		/// <returns>bool - success/failure</returns>
		public bool DvtSummaryToXml(StreamWriter streamWriter, int level)
		{
			if (
				(streamWriter != null) &&
				this.ContainsMessages()
				)
			{
				streamWriter.WriteLine("<AttributeComparisonResults>");
				string group = _Tag.GroupNumber.ToString("X");
				while (group.Length < 4)
				{
					group = "0" + group;
				}
				string element = _Tag.ElementNumber.ToString("X");
				while (element.Length < 4)
				{
					element = "0" + element;
				}
				streamWriter.WriteLine("<AttributeComparison Group=\"{0}\" Element=\"{1}\" Name=\"{2}\">", group, element, DvtToXml.ConvertString(_Name));
				streamWriter.WriteLine("<Value1>{0}</Value1>", _Value1);
				streamWriter.WriteLine("<Value2>{0}</Value2>", _Value2);
				streamWriter.WriteLine("</AttributeComparison>");
				Messages.DvtDetailToXml(streamWriter, level);
				streamWriter.WriteLine("</AttributeComparisonResults>");
			}
			return true;
		}    	

		/// <summary>
		/// Check if this contains any validation messages
		/// </summary>
		/// <returns>bool - contains validation messages true/false</returns>
		public bool ContainsMessages()
		{
			bool containsMessages = false;
			if (Messages.ErrorWarningCount() != 0)
			{
				containsMessages = true;
			}
			return containsMessages;
		}
	}

	/// <summary>
	/// Summary description for MessageComparisonResults.
	/// </summary>
	public class MessageComparisonResults : CollectionBase, IDvtDetailToXml, IDvtSummaryToXml
	{
		private System.String _ObjectName1 = System.String.Empty;
		private System.String _ObjectName2 = System.String.Empty;
		private DvtkData.Dimse.DimseCommand _DimseCommand1 = DvtkData.Dimse.DimseCommand.UNDEFINED;
		private DvtkData.Dimse.DimseCommand _DimseCommand2 = DvtkData.Dimse.DimseCommand.UNDEFINED;
		private System.String _IodName1 = System.String.Empty;
		private System.String _IodName2 = System.String.Empty;
		private System.String _SopClassUid1 = System.String.Empty;
		private System.String _SopClassUid2 = System.String.Empty;

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="objectName1">Object1 name</param>
		/// <param name="objectName2">Object2 name</param>
		/// <param name="dimseCommand1">Dimse Command1</param>
		/// <param name="dimseCommand2">Dimse Command2</param>
		/// <param name="sopClassUid1">Sop Class Uid1</param>
		/// <param name="sopClassUid2">Sop Class Uid2</param>
		public MessageComparisonResults(System.String objectName1,
										System.String objectName2,
										DvtkData.Dimse.DimseCommand dimseCommand1,
										DvtkData.Dimse.DimseCommand dimseCommand2,
										System.String sopClassUid1,
										System.String sopClassUid2)
		{
			_ObjectName1 = objectName1;
			_ObjectName2 = objectName2;
			_DimseCommand1 = dimseCommand1;
			_DimseCommand2 = dimseCommand2;
			_SopClassUid1 = sopClassUid1;
			_SopClassUid2 = sopClassUid2;
		}

		/// <summary>
		/// Command1 property - Get only
		/// </summary>
		public DvtkData.Dimse.DimseCommand Command1
		{
			get
			{
				return _DimseCommand1;
			}
		}

		/// <summary>
		/// Command2 property - Get only
		/// </summary>
		public DvtkData.Dimse.DimseCommand Command2
		{
			get
			{
				return _DimseCommand2;
			}
		}

		/// <summary>
		/// SopClassUid1 property - Get only
		/// </summary>
		public System.String SopClassUid1
		{
			get
			{
				return _SopClassUid1;
			}
		}

		/// <summary>
		/// SopClassUid2 property - Get only
		/// </summary>
		public System.String SopClassUid2
		{
			get
			{
				return _SopClassUid2;
			}
		}

		/// <summary>
		/// IodName1 property - Set only
		/// </summary>
		public System.String IodName1
		{
			set
			{
				_IodName1 = value;
			}
		}

		/// <summary>
		/// IodName1 property - Set only
		/// </summary>
		public System.String IodName2
		{
			set
			{
				_IodName2 = value;
			}
		}

		/// <summary>
		/// Gets or sets an <see cref="AttributeComparisonResults"/> from the collection.
		/// </summary>
		/// <param name="index">The zero-based index of the collection member to get or set.</param>
		/// <value>The <see cref="MessageComparisonResults"/> at the specified index.</value>
		public AttributeComparisonResults this[int index]  
		{
			get  
			{
				return ((AttributeComparisonResults) List[index]);
			}
			set  
			{
				List[index] = value;
			}
		}

		/// <summary>
		/// Adds an object to the end of the <see cref="MessageComparisonResults"/>.
		/// </summary>
		/// <param name="value">The <see cref="AttributeComparisonResults"/> to be added to the end of the <see cref="MessageComparisonResults"/>.</param>
		/// <returns>The <see cref="MessageComparisonResults"/> index at which the value has been added.</returns>
		public int Add(AttributeComparisonResults value)  
		{
			return (List.Add(value));
		}

		/// <summary>
		/// Searches for the specified <see cref="AttributeComparisonResults"/> and 
		/// returns the zero-based index of the first occurrence within the entire <see cref="MessageComparisonResults"/>.
		/// </summary>
		/// <param name="value">The <see cref="AttributeComparisonResults"/> to locate in the <see cref="MessageComparisonResults"/>.</param>
		/// <returns>
		/// The zero-based index of the first occurrence of value within the entire <see cref="MessageComparisonResults"/>, 
		/// if found; otherwise, -1.
		/// </returns>
		public int IndexOf(AttributeComparisonResults value)  
		{
			return (List.IndexOf(value));
		}

		/// <summary>
		/// Inserts an <see cref="AttributeComparisonResults"/> element into the <see cref="MessageComparisonResults"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">The <see cref="MessageComparisonResults"/> to insert.</param>
		public void Insert(int index, AttributeComparisonResults value)  
		{
			List.Insert(index, value);
		}

		/// <summary>
		/// Removes the first occurrence of a specific <see cref="AttributeComparisonResults"/> from the <see cref="MessageComparisonResults"/>.
		/// </summary>
		/// <param name="value">The <see cref="AttributeComparisonResults"/> to remove from the <see cref="MessageComparisonResults"/>.</param>
		public void Remove(AttributeComparisonResults value)  
		{
			List.Remove(value);
		}

		/// <summary>
		/// Determines whether the <see cref="MessageComparisonResults"/> contains a specific element.
		/// </summary>
		/// <param name="value">The <see cref="AttributeComparisonResults"/> to locate in the <see cref="MessageComparisonResults"/>.</param>
		/// <returns>
		/// <c>true</c> if the <see cref="MessageComparisonResults"/> contains the specified value; 
		/// otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(AttributeComparisonResults value)  
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
			if (!(value is AttributeComparisonResults))
				throw new ArgumentException("value must be of type AttributeComparisonResults.", "value");
		}

		/// <summary>
		/// Performs additional custom processes when removing an element from the collection instance.
		/// </summary>
		/// <param name="index">The zero-based index at which value can be found.</param>
		/// <param name="value">The value of the element to remove from index.</param>
		protected override void OnRemove(int index, Object value)  
		{
			if (!(value is AttributeComparisonResults))
				throw new ArgumentException("value must be of type AttributeComparisonResults.", "value");
		}

		/// <summary>
		/// Performs additional custom processes before setting a value in the collection instance.
		/// </summary>
		/// <param name="index">The zero-based index at which oldValue can be found.</param>
		/// <param name="oldValue">The value to replace with newValue.</param>
		/// <param name="newValue">The new value of the element at index.</param>
		protected override void OnSet(int index, Object oldValue, Object newValue)  
		{
			if (!(newValue is AttributeComparisonResults))
				throw new ArgumentException("newValue must be of type AttributeComparisonResults.", "newValue");
		}

		/// <summary>
		/// Performs additional custom processes when validating a value.
		/// </summary>
		/// <param name="value">The object to validate.</param>
		protected override void OnValidate(Object value)  
		{
			if (!(value is AttributeComparisonResults))
				throw new ArgumentException("value must be of type AttributeComparisonResults.");
		}
	
		/// <summary>
		/// Copies the elements of the <see cref="ICollection"/> to a strong-typed <c>AttributeComparisonResults[]</c>, 
		/// starting at a particular <c>AttributeComparisonResults[]</c> index.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional <c>AttributeComparisonResults[]</c> that is the destination of the elements 
		/// copied from <see cref="ICollection"/>.
		/// The <c>AttributeComparisonResults[]</c> must have zero-based indexing. 
		/// </param>
		/// <param name="index">
		/// The zero-based index in array at which copying begins.
		/// </param>
		/// <remarks>
		/// Provides the strongly typed member for <see cref="ICollection"/>.
		/// </remarks>
		public void CopyTo(AttributeComparisonResults[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>
		/// Serialize DVT Detail Data to Xml.
		/// </summary>
		/// <param name="streamWriter">Stream writer to serialize to.</param>
		/// <param name="level">Recursion level. 0 = Top.</param>
		/// <returns>bool - success/failure</returns>
		public bool DvtDetailToXml(StreamWriter streamWriter, int level)
		{
			if (streamWriter != null)
			{
				streamWriter.WriteLine("<MessageComparisonResults>");
				streamWriter.WriteLine("<Object1>{0}</Object1>", _ObjectName1);
				streamWriter.WriteLine("<Object2>{0}</Object2>", _ObjectName2);
				streamWriter.WriteLine("<Message1>{0} {1}</Message1>", _DimseCommand1.ToString(), _IodName1);
				streamWriter.WriteLine("<Message2>{0} {1}</Message2>", _DimseCommand2.ToString(), _IodName2);
				foreach (AttributeComparisonResults acr in this)
				{
					acr.DvtDetailToXml(streamWriter, level);
				}
				streamWriter.WriteLine("</MessageComparisonResults>");
			}
			return true;
		}    

		/// <summary>
		/// Serialize DVT Summary Data to Xml.
		/// </summary>
		/// <param name="streamWriter">Stream writer to serialize to.</param>
		/// <param name="level">Recursion level. 0 = Top.</param>
		/// <returns>bool - success/failure</returns>
		public bool DvtSummaryToXml(StreamWriter streamWriter, int level)
		{
			if (
				(streamWriter != null) &&
				this.ContainsMessages()
				)
			{
				streamWriter.WriteLine("<MessageComparisonResults>");
				streamWriter.WriteLine("<Object1>{0}</Object1>", _ObjectName1);
				streamWriter.WriteLine("<Object2>{0}</Object2>", _ObjectName2);
				streamWriter.WriteLine("<Message1>{0} {1}</Message1>", _DimseCommand1.ToString(), _IodName1);
				streamWriter.WriteLine("<Message2>{0} {1}</Message2>", _DimseCommand2.ToString(), _IodName2);
				foreach (AttributeComparisonResults acr in this)
				{
					acr.DvtSummaryToXml(streamWriter, level);
				}
				streamWriter.WriteLine("</MessageComparisonResults>");
			}
			return true;
		}    	

		/// <summary>
		/// Check if this contains any validation messages
		/// </summary>
		/// <returns>bool - contains validation messages true/false</returns>
		private bool ContainsMessages()
		{
			bool containsMessages = false;

			foreach (AttributeComparisonResults acr in this)
			{
				if (acr.ContainsMessages() == true)
				{
					containsMessages = true;
					break;
				}
			}
			return containsMessages;
		}
	}
}
