// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;
using Dvtk.Comparator;
using Dvtk.Results;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for ActorsTransactionLog.
	/// </summary>
	public class ActorsTransactionLog : CollectionBase
	{
		/// <summary>
		/// Gets or sets an <see cref="ActorsTransaction"/> from the collection.
		/// </summary>
		/// <param name="index">The zero-based index of the collection member to get or set.</param>
		/// <value>The <see cref="ActorsTransactionLog"/> at the specified index.</value>
		public ActorsTransaction this[int index]  
		{
			get  
			{
				return ((ActorsTransaction) List[index]);
			}
			set  
			{
				List[index] = value;
			}
		}

		/// <summary>
		/// Adds an object to the end of the <see cref="ActorsTransactionLog"/>.
		/// </summary>
		/// <param name="value">The <see cref="ActorsTransaction"/> to be added to the end of the <see cref="ActorsTransactionLog"/>.</param>
		/// <returns>The <see cref="ActorsTransactionLog"/> index at which the value has been added.</returns>
		public int Add(ActorsTransaction value)  
		{
			return (List.Add(value));
		}

		/// <summary>
		/// Searches for the specified <see cref="ActorsTransaction"/> and 
		/// returns the zero-based index of the first occurrence within the entire <see cref="ActorsTransactionLog"/>.
		/// </summary>
		/// <param name="value">The <see cref="ActorsTransaction"/> to locate in the <see cref="ActorsTransactionLog"/>.</param>
		/// <returns>
		/// The zero-based index of the first occurrence of value within the entire <see cref="ActorsTransactionLog"/>, 
		/// if found; otherwise, -1.
		/// </returns>
		public int IndexOf(ActorsTransaction value)  
		{
			return (List.IndexOf(value));
		}

		/// <summary>
		/// Inserts an <see cref="ActorsTransaction"/> element into the <see cref="ActorsTransactionLog"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">The <see cref="ActorsTransaction"/> to insert.</param>
		public void Insert(int index, ActorsTransaction value)  
		{
			List.Insert(index, value);
		}

		/// <summary>
		/// Removes the first occurrence of a specific <see cref="ActorsTransaction"/> from the <see cref="ActorsTransactionLog"/>.
		/// </summary>
		/// <param name="value">The <see cref="ActorsTransaction"/> to remove from the <see cref="ActorsTransactionLog"/>.</param>
		public void Remove(ActorsTransaction value)  
		{
			List.Remove(value);
		}

		/// <summary>
		/// Determines whether the <see cref="ActorsTransactionLog"/> contains a specific element.
		/// </summary>
		/// <param name="value">The <see cref="ActorsTransaction"/> to locate in the <see cref="ActorsTransactionLog"/>.</param>
		/// <returns>
		/// <c>true</c> if the <see cref="ActorsTransactionLog"/> contains the specified value; 
		/// otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(ActorsTransaction value)  
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
			if (!(value is ActorsTransaction))
				throw new ArgumentException("value must be of type ActorsTransaction.", "value");
		}

		/// <summary>
		/// Performs additional custom processes when removing an element from the collection instance.
		/// </summary>
		/// <param name="index">The zero-based index at which value can be found.</param>
		/// <param name="value">The value of the element to remove from index.</param>
		protected override void OnRemove(int index, Object value)  
		{
			if (!(value is ActorsTransaction))
				throw new ArgumentException("value must be of type ActorsTransaction.", "value");
		}

		/// <summary>
		/// Performs additional custom processes before setting a value in the collection instance.
		/// </summary>
		/// <param name="index">The zero-based index at which oldValue can be found.</param>
		/// <param name="oldValue">The value to replace with newValue.</param>
		/// <param name="newValue">The new value of the element at index.</param>
		protected override void OnSet(int index, Object oldValue, Object newValue)  
		{
			if (!(newValue is ActorsTransaction))
				throw new ArgumentException("newValue must be of type ActorsTransaction.", "newValue");
		}

		/// <summary>
		/// Performs additional custom processes when validating a value.
		/// </summary>
		/// <param name="value">The object to validate.</param>
		protected override void OnValidate(Object value)  
		{
			if (!(value is ActorsTransaction))
				throw new ArgumentException("value must be of type ActorsTransaction.");
		}
	
		/// <summary>
		/// Copies the elements of the <see cref="ICollection"/> to a strong-typed <c>ActorsTransaction[]</c>, 
		/// starting at a particular <c>ActorsTransaction[]</c> index.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional <c>ActorsTransaction[]</c> that is the destination of the elements 
		/// copied from <see cref="ICollection"/>.
		/// The <c>ActorsTransaction[]</c> must have zero-based indexing. 
		/// </param>
		/// <param name="index">
		/// The zero-based index in array at which copying begins.
		/// </param>
		/// <remarks>
		/// Provides the strongly typed member for <see cref="ICollection"/>.
		/// </remarks>
		public void CopyTo(ActorsTransaction[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>
		/// Get the total number of errors occuring the Transaction Log.
		/// </summary>
		/// <returns>uint - Total number of errors.</returns>
		public uint NrErrors()
		{
			uint nrErrors = 0;

			foreach (ActorsTransaction actorsTransaction in this)
			{
				nrErrors += actorsTransaction.NrErrors;
			}

			return nrErrors;
		}

		/// <summary>
		/// Get the total number of warnings occuring the Transaction Log.
		/// </summary>
		/// <returns>uint - Total number of warnings.</returns>
		public uint NrWarnings()
		{
			uint nrWarnings = 0;

			foreach (ActorsTransaction actorsTransaction in this)
			{
				nrWarnings += actorsTransaction.NrWarnings;
			}

			return nrWarnings;
		}

		/// <summary>
		/// Evaluate the transaction log results using the associated comparators.
		/// </summary>
		public void Evaluate(ResultsReporter resultsReporter)
		{
			Dvtk.Comparator.BaseComparatorCollection comparatorCollection = new Dvtk.Comparator.BaseComparatorCollection();

			for (int i = 0; i < this.Count; i++)
			{
				foreach (ActorsTransaction actorsTransaction in this)
				{
					if (actorsTransaction.TransactionNumber == i + 1)
					{
						actorsTransaction.SetComparators(comparatorCollection);
					}
				}
			}

			comparatorCollection.Compare(resultsReporter);
		}

		/// <summary>
		/// Display the Transaction Log - for debugging purposes.
		/// </summary>
		public void ConsoleDisplay()
		{
			for (int i = 0; i < this.Count; i++)
			{
				foreach (ActorsTransaction actorsTransaction in this)
				{
					if (actorsTransaction.TransactionNumber == i + 1)
					{
						actorsTransaction.ConsoleDisplay();
					}
				}
			}

			Console.WriteLine("Total {0} errors, {1} warnings", this.NrErrors(), this.NrWarnings());
		}
	}
}
