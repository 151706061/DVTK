// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Dvtk.Comparator;

using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;
using DvtkHighLevelInterface.Comparator;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for ActorsTransaction.
	/// </summary>
	public class ActorsTransaction
	{
		private int _transactionNumber;
		private ActorNameEnum _fromActorName;
		private ActorNameEnum _toActorName;
		private BaseTransaction _transaction = null;
		private System.String _resultsFilename;
		private uint _nrErrors = 0;
		private uint _nrWarnings = 0;

		public ActorsTransaction(int transactionNumber,
								ActorNameEnum fromActorName, 
								ActorNameEnum toActorName, 
								BaseTransaction transaction,
								System.String resultsFilename,
								uint nrErrors,
								uint nrWarnings)
		{
			_transactionNumber = transactionNumber;
			_fromActorName = fromActorName;
			_toActorName = toActorName;
			_transaction = transaction;
			_resultsFilename = resultsFilename;
			_nrErrors = nrErrors;
			_nrWarnings = nrWarnings;
		}

		public int TransactionNumber
		{
			get
			{
				return _transactionNumber;
			}
		}

		public ActorNameEnum FromActorName
		{
			get
			{
				return _fromActorName;
			}
		}

		public ActorNameEnum ToActorName
		{
			get
			{
				return _toActorName;
			}
		}

		public TransactionDirectionEnum Direction
		{
			get
			{
				return _transaction.Direction;
			}
		}

		public System.String ResultsFilename
		{
			get
			{
				return _resultsFilename;
			}
		}

		public uint NrErrors
		{
			get
			{
				return _nrErrors;
			}
		}

		public uint NrWarnings
		{
			get
			{
				return _nrWarnings;
			}
		}

		public void ConsoleDisplay()
		{
			Console.WriteLine("<<- {0} -------------------------------------------------------------->>", _transactionNumber);
			if (_transaction is DicomTransaction)
			{
				DicomTransaction dicomTransaction = (DicomTransaction)_transaction;
				switch(dicomTransaction.Direction)
				{
					case TransactionDirectionEnum.TransactionReceived:
						Console.WriteLine("DICOM Transaction received by {0}", ActorNames.Name(_fromActorName));
						Console.WriteLine("from {0}", ActorNames.Name(_toActorName));
						break;
					case TransactionDirectionEnum.TransactionSent:
						Console.WriteLine("DICOM Transaction sent from {0}", ActorNames.Name(_fromActorName));
						Console.WriteLine("to {0}", ActorNames.Name(_toActorName));
						break;
					default:
						break;
				}
				Console.WriteLine("{0} errors, {1} warnings", _nrErrors, _nrWarnings);
				for (int i = 0; i < dicomTransaction.DicomMessages.Count; i++)
				{
					Console.WriteLine("DICOM Message {0}...", i + 1);
					DicomMessage dicomMessage = (DicomMessage)dicomTransaction.DicomMessages[i];
					if (dicomMessage.CommandSet != null)
					{
						Console.WriteLine("Command Attributes: {0}", dicomMessage.CommandSet.Count);
					}
					if (dicomMessage.DataSet != null)
					{
						Console.WriteLine("Dataset Attributes: {0}", dicomMessage.DataSet.Count);
					}
				}
			}
			else
			{
				Hl7Transaction hl7Transaction = (Hl7Transaction)_transaction;
			}
			Console.WriteLine("Results Filename: \"{0}\"", _resultsFilename);

			Console.WriteLine("<<------------------------------------------------------------------>>");
		}

		public void SetComparators(Dvtk.Comparator.BaseComparatorCollection comparatorCollection)
		{
			if (_transaction is DicomTransaction)
			{
				System.String name = System.String.Empty;

				DicomTransaction dicomTransaction = (DicomTransaction)_transaction;
				switch(dicomTransaction.Direction)
				{
					case TransactionDirectionEnum.TransactionReceived:
						name = System.String.Format("Received by {0} from {1}", ActorNames.Name(_fromActorName), ActorNames.Name(_toActorName));
						break;
					case TransactionDirectionEnum.TransactionSent:
						name = System.String.Format("Sent from {0} to {1}", ActorNames.Name(_fromActorName), ActorNames.Name(_toActorName));
						break;
					default:
						break;
				}

				for (int i = 0; i < dicomTransaction.DicomMessages.Count; i++)
				{
					DicomMessage dicomMessage = (DicomMessage)dicomTransaction.DicomMessages[i];

					DvtkHighLevelInterface.Comparator.Comparator comparator = new DvtkHighLevelInterface.Comparator.Comparator(name);
					Dvtk.Comparator.DicomComparator dicomComparator = comparator.InitializeDicomComparator(dicomMessage);
					if (dicomComparator != null)
					{
						comparatorCollection.Add(dicomComparator);
					}
				}
			}
		}

		public void ConvertResult(System.String applicationDirectory, System.String resultsDirectory)
		{
			XslTransform xslt = new XslTransform();
			xslt.Load(applicationDirectory + "DVT_RESULTS.xslt");

			System.String resultsFilename = resultsDirectory + "\\Summary_" + _resultsFilename;
			XPathDocument xpathdocument = new XPathDocument(resultsFilename);
//			XmlTextWriter writer = new XmlTextWriter(resultsFilename.Replace (".xml", ".html"), System.Text.Encoding.UTF8);
//			writer.Formatting = Formatting.None;
//			xslt.Transform(xpathdocument, null, writer, null);
//			writer.Flush();
//			writer.Close();

			FileStream fileStream = new FileStream(resultsFilename.Replace (".xml", ".html"), FileMode.Create, FileAccess.ReadWrite);
			xslt.Transform(xpathdocument, null, fileStream, null);
			fileStream.Close();

			resultsFilename = resultsDirectory + "\\Detail_" + _resultsFilename;
			xpathdocument = new XPathDocument(resultsFilename);
//			writer = new XmlTextWriter(resultsFilename.Replace (".xml", ".html"), System.Text.Encoding.UTF8);
//			writer.Formatting = Formatting.None;
//			xslt.Transform(xpathdocument, null, writer, null);
//			writer.Flush();
//			writer.Close();

			fileStream = new FileStream(resultsFilename.Replace (".xml", ".html"), FileMode.Create, FileAccess.ReadWrite);
			xslt.Transform(xpathdocument, null, fileStream, null);
			fileStream.Close();
		}
	}
}
