using System;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for Instruction.
	/// </summary>
	public class Instruction
	{
		public Instruction(String threadId, String text)
		{
			this.threadId = threadId;
			this.text = text;
		}

		public String threadId;

		public String text;
	}
}
