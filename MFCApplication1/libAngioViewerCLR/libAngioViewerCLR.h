// libAngioViewerCLR.h

#pragma once

using namespace System;
using namespace System::Reflection;

namespace libAngioViewerCLR {

	public ref class Viewer
	{
	public:
		static void open_viewer(String^ data_dir, String^ db_file_path) {
			libAngioViewer::Entry::open_viewer(data_dir, db_file_path);
		}
	};
}

__declspec(dllexport) void open_angio_viewer(char* data_dir, char* db_file_path)
{
	String^ strDataDir = gcnew String(data_dir);
	String^ strDBFIlePath = gcnew String(db_file_path);
	libAngioViewerCLR::Viewer::open_viewer(strDataDir, strDBFIlePath);
}