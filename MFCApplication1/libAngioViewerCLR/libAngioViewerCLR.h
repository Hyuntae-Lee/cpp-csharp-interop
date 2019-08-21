// libAngioViewerCLR.h

#pragma once

using namespace System;
using namespace System::Reflection;

namespace libAngioViewerCLR {

	public ref class Viewer
	{
	public:
		static void open_viewer() {
			libAngioViewer::Entry::open_viewer();
		}
	};
}

__declspec(dllexport) void open_angio_viewer()
{
	libAngioViewerCLR::Viewer::open_viewer();
}