// libAngioViewerCLR.h

#pragma once

using namespace System;
using namespace System::Reflection;

namespace libAngioViewerCLR {

	public ref class Viewer
	{
	public:
		static void open_viewer(String^ data_dir) {
			libAngioViewer::Entry::open_viewer(data_dir);
		}
	};
}

__declspec(dllexport) void open_angio_viewer(char* data_dir)
{
	String^ str = gcnew String(data_dir);
	libAngioViewerCLR::Viewer::open_viewer(str);
}