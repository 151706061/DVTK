#pragma once

namespace Wrappers
{
	public __gc class MDICOMDIRGenerator
	{
	public:
		MDICOMDIRGenerator(void);

		~MDICOMDIRGenerator(void);

		static bool WCreateDICOMDIR(System::String* fileNames[]);
	};
}
