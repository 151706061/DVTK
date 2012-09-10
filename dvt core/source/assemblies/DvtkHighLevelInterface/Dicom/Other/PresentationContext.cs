using System;
using System.Collections;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for RequestedPresentationContext.
	/// </summary>
	public class PresentationContext
	{
		/// <summary>
		/// The parameters supplied to the constructor.
		/// </summary>
		private ArrayList constructorParameters;

		/// <summary>
		/// SOP Class name or SOP Class UID.
		/// </summary>
		private String sopClass = null;

		/// <summary>
		/// List of Transfer Syntax names and/or Transfer Syntax UIDs.
		/// </summary>
		private ArrayList transferSyntaxes = new ArrayList();

		/// <summary>
		/// Result when used in a associate accept.
		/// </summary>
		private byte result = 0;
		
		/// <summary>
		/// Indicates if the parameters of the constructor have been interpreted successfully.
		/// </summary>
		private bool interpretedSuccessfully = false;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="parameters">
		/// TODO!!!
		/// </param>
		public PresentationContext(params Object[] parameters)
		{
			this.constructorParameters = new ArrayList(parameters);

		}

		/// <summary>
		/// Get the SOP Class UID's and/or names.
		/// </summary>
		internal String SopClass
		{
			get
			{
				if (!interpretedSuccessfully)
				{
					DvtkHighLevelInterfaceException.Throw("Has not yet been interpreted (successfully).");
				}

				return this.sopClass;
			}
		}

		/// <summary>
		/// Get the Result.
		/// </summary>
		internal byte Result
		{
			get
			{
				if (!interpretedSuccessfully)
				{
					DvtkHighLevelInterfaceException.Throw("Has not yet been interpreted (successfully).");
				}

				return this.result;
			}
		}

		/// <summary>
		/// Get the transfer syntax UID's and/or names.
		/// </summary>
		internal ArrayList TransferSyntaxes
		{
			get
			{
				if (!interpretedSuccessfully)
				{
					DvtkHighLevelInterfaceException.Throw("Has not yet been interpreted (successfully).");
				}

				return this.transferSyntaxes;
			}
		}

		/// <summary>
		/// Interpret the parameters supplied to the constructor as a Presentation Context 
		/// for an Associate Request.
		/// When interpreted, fields of this object will have the interpreted values.
		/// </summary>
		/// <returns>
		/// Indicates if the parameters of the constructor could be interpreted as a Presentation Context
		/// for an Associate Request.
		/// </returns>
		internal bool InterpretAsPcForAssociateRq()
		{
			int parameterToProcessIndex = 0;

			this.interpretedSuccessfully = true;

			// Get the SOP Class name or SOP Class UID;
			if (this.constructorParameters.Count > 0)
			{
				this.sopClass = this.constructorParameters[parameterToProcessIndex] as String;

				if (this.sopClass == null)
				{
					this.interpretedSuccessfully = false;
				}
			}
			else
			{
				this.interpretedSuccessfully = false;
			}

			parameterToProcessIndex++;

			if (this.interpretedSuccessfully)
			{
				while (parameterToProcessIndex < this.constructorParameters.Count)
				{
					String transferSyntax = this.constructorParameters[parameterToProcessIndex] as String;

					if (transferSyntax == null)
					{
						this.interpretedSuccessfully = false;
						break;
					}
					else
					{
						this.transferSyntaxes.Add(transferSyntax);
						parameterToProcessIndex++;

					}
				}
			}

			if (this.interpretedSuccessfully)
			{
				// Check that at least one transfer syntax is present
				if (transferSyntaxes.Count == 0)
				{
					this.interpretedSuccessfully = false;
				}
			}

			return (this.interpretedSuccessfully);
		}

		internal bool InterpretAsPcForAssociateAc()
		{
			this.interpretedSuccessfully = true;

			if (this.constructorParameters.Count != 3)
			{
				this.interpretedSuccessfully = false;
			}
			else
			{
				this.sopClass = this.constructorParameters[0] as String;

				// SOP Class.
				if (this.sopClass == null)
				{
					this.interpretedSuccessfully = false;
				}

				// Results.
				if (this.interpretedSuccessfully)
				{
					Convert.ToByte(this.constructorParameters[1]);
				}

				// Transfer syntax.
				if (this.interpretedSuccessfully)
				{
					String transferSyntax = this.constructorParameters[2] as String;

					if (transferSyntax == null)
					{
						this.interpretedSuccessfully = false;
					}
					else
					{
						this.transferSyntaxes.Add(transferSyntax);
					}
				}
			}

			return this.interpretedSuccessfully;
 		}
	}
}
